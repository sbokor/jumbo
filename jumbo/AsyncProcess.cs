using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace jumbo
{
  //
  public class ProcStatus
  {
    public Process proc = null;
    public AutoResetEvent exit_event = new AutoResetEvent(false);
    public bool running = false;
    public int exit_code = 0;
  }


  //
  public class AsyncProcess
  {
    // WinAPI imports

    // Ctrl+C signal
    internal const int CTRL_C_EVENT = 0;

    [DllImport("kernel32.dll")]
    internal static extern bool GenerateConsoleCtrlEvent(uint dwCtrlEvent, uint dwProcessGroupId);

    [DllImport("kernel32.dll", SetLastError = false)]
    internal static extern bool AttachConsole(uint dwProcessId);

    [DllImport("kernel32.dll", SetLastError = false, ExactSpelling = true)]
    internal static extern bool FreeConsole();

    [DllImport("kernel32.dll")]
    static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate HandlerRoutine, bool Add);

    // Delegate type to be used as the Handler Routine for SCCH
    delegate Boolean ConsoleCtrlDelegate(uint CtrlType);

    [DllImport("kernel32.dll")]
    static extern uint SetErrorMode(uint uMode);

    [Flags]
    public enum ErrorModes : uint
    {
      SYSTEM_DEFAULT = 0x0,
      SEM_FAILCRITICALERRORS = 0x0001,
      SEM_NOALIGNMENTFAULTEXCEPT = 0x0004,
      SEM_NOGPFAULTERRORBOX = 0x0002,
      SEM_NOOPENFILEERRORBOX = 0x8000
    }


    // Delegates
    public delegate void CallbackEventHandler(string arg);
    public event CallbackEventHandler OutputCallback = null;
    public event CallbackEventHandler ErrorCallback = null;


    // Class variables
    protected ProcStatus status = null;
    protected string cmd = String.Empty;
    protected string opt = String.Empty;
    int timeout = Timeout.Infinite;


    // ctor
    public AsyncProcess(string Command,
                        string Options = "",
                        CallbackEventHandler OnOutput = null,
                        CallbackEventHandler OnError = null,
                        int Timeout = Timeout.Infinite)
    {
      status = new ProcStatus();
      cmd = Command;
      opt = Options;
      if (OnOutput != null) OutputCallback += OnOutput;
      if (OnError != null) ErrorCallback += OnError;
      timeout = Timeout;
    }


    public bool IsRunning
    {
      get { return status.running; }
    }



    // Based on: https://gist.github.com/georg-jung/3a8703946075d56423e418ea76212745
    public async Task<int> Run()
    {      
      // Sanity check
      if (status == null) {
        return -1;
      }

      using ( status.proc = new Process() )
      {
        status.proc.StartInfo.FileName = this.cmd;
        status.proc.StartInfo.Arguments = this.opt;
        status.proc.StartInfo.UseShellExecute = false;       
        status.proc.StartInfo.ErrorDialog = false;

        //status.proc.StartInfo.RedirectStandardInput = true;
        status.proc.StartInfo.RedirectStandardOutput = true;
        status.proc.StartInfo.RedirectStandardError = true;
        status.proc.StartInfo.CreateNoWindow = true; 
        status.proc.EnableRaisingEvents = true;

        TaskCompletionSource<bool> out_done = new TaskCompletionSource<bool>();
        TaskCompletionSource<bool> err_done = new TaskCompletionSource<bool>();

        status.proc.Exited += (s, e) => { 
          status.running = false;
        };

        status.proc.Disposed += (s, e) => {
          status.running = false;
        };

        status.proc.OutputDataReceived += (s, e) => {
          if (e.Data == null) out_done.SetResult(true);
          else if (OutputCallback != null) OutputCallback(e.Data);
        };

        status.proc.ErrorDataReceived += (s, e) => {
          if (e.Data == null) err_done.SetResult(true);
          else if (ErrorCallback != null) ErrorCallback(e.Data);          
        };

        lock(this)
        {
          using (ChangeErrorMode mode = new ChangeErrorMode(ErrorModes.SEM_FAILCRITICALERRORS | ErrorModes.SEM_NOGPFAULTERRORBOX))
          {
            status.exit_event.Reset();
            if (!status.proc.Start()) {
              status.running = false;
              status.exit_event.Set(); // ?
              return -1;
            }
            status.running = true;
          }
        }

        // Reads the output stream first and then waits because deadlocks are possible
        status.proc.BeginOutputReadLine();
        status.proc.BeginErrorReadLine();

        // Creates task to wait for process exit using timeout
        Task<bool> timeout_done = Task.Run(() => status.proc.WaitForExit(this.timeout));

        // Create task to wait for process exit and closing all output streams
        Task<bool[]> process_done = Task.WhenAll(timeout_done, out_done.Task, err_done.Task);

        // Waits process completion and then checks it was not completed by timeout
        if (await Task.WhenAny(Task.Delay(timeout), process_done) == process_done && timeout_done.Result) {
          status.exit_code = status.proc.ExitCode;
        }
        else {
          try { status.proc.Kill(); }
          catch {}
        }
        
      }  // using process

      status.exit_event.Set();
      return status.exit_code;
    }



    //
    public bool Abort()
    {
      try {
        return ThreadPool.QueueUserWorkItem(new WaitCallback(AbortMethod), status);
      }
      catch (Exception) { }
      return false;
    }



    //
    protected static void AbortMethod(object data) 
    {
      ProcStatus status = (ProcStatus)data;
      if (status == null || status.proc == null) {
        return;
      }

      FreeConsole();
      AttachConsole((uint)status.proc.Id);
      SetConsoleCtrlHandler(null, true);
      GenerateConsoleCtrlEvent(CTRL_C_EVENT, 0);

      status.exit_event.WaitOne(2000);

      // Die wicked-witch
      if (status.running) {
        try { status.proc.Kill(); }
        catch { }
      }

      FreeConsole();
      SetConsoleCtrlHandler(null, false);
    }



    //
    protected struct ChangeErrorMode : IDisposable
    {
      private uint old_mode;

      public ChangeErrorMode(ErrorModes mode) {
        old_mode = SetErrorMode((uint)mode);
      }

      void IDisposable.Dispose() { 
        SetErrorMode(old_mode); 
      }
    }



  }  // class
}    // namespace

