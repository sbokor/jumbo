using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace jumbo
{
  public class AsyncProcess
  {
    // WinAPI imports

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

    // Ctrl+C signal
    internal const int CTRL_C_EVENT = 0;


    //
    public struct ProcResult
    {
      public int? ExitCode;
      public string Output;
      public string Error;
    }


    //
    protected class ProcStatus
    {
      public Process proc = null;
      public AutoResetEvent exit_event = new AutoResetEvent(false);
      public bool running = false;
    }
    protected static ProcStatus status = new ProcStatus();


    // Delegates
    public delegate void CallbackEventHandler(string arg);
    public event CallbackEventHandler OutputCallback;
    public event CallbackEventHandler ErrorCallback;


    //public Process ShellProcess {
    //  get {
    //    return process;
    //  }
    //}

    //public int Id {
    //  get {
    //    if (process == null) return 0;
    //    return process.Id;
    //  }
    //}

    public bool IsRunning {
      get {
        //if (status.proc == null) return false;
        //try {Process.GetProcessById(status.proc.Id);}
        //catch (InvalidOperationException) { return false; }
        //catch (ArgumentException){return false;}
        //return true;

        return status.running;
     }
   }






    //
    public async Task<int> Run(string command, string arguments, int timeout)
    {      
      ProcResult result = new ProcResult();
      int exit_code = 0;

      using ( status.proc = new Process() )
      {
        status.proc.StartInfo.FileName = command;
        status.proc.StartInfo.Arguments = arguments;
        status.proc.StartInfo.UseShellExecute = false;       
        status.proc.StartInfo.ErrorDialog = false;

        //status.proc.StartInfo.RedirectStandardInput = true;
        status.proc.StartInfo.RedirectStandardOutput = true;
        status.proc.StartInfo.RedirectStandardError = true;
        status.proc.StartInfo.CreateNoWindow = true;

        status.proc.EnableRaisingEvents = true;

        status.proc.Exited += (s, e) => {
          status.running = false;
        };

        status.proc.Disposed += (s, e) => {
          status.running = false;
        };

        //StringBuilder out_buff = new StringBuilder();
        TaskCompletionSource<bool> out_done = new TaskCompletionSource<bool>();

        status.proc.OutputDataReceived += (s, e) => {
          if (e.Data == null) {
            out_done.SetResult(true);
          }
          else {
            //out_buff.Append(e.Data);
            //out_buff.Append("\r\n");
            OutputCallback(e.Data);
          }
        };

        string err = String.Empty;
        //StringBuilder err_buff = new StringBuilder();
        TaskCompletionSource<bool> err_done = new TaskCompletionSource<bool>();

        status.proc.ErrorDataReceived += (s, e) => {
          if (e.Data == null) {
            err_done.SetResult(true);
          }
          else {
            //err_buff.Append(e.Data);
            //err_buff.Append("\r\n");
            ErrorCallback(e.Data);
            //
            err = (string)e.Data.Clone();            
          }
        };

        lock(this)
        {
          using (ChangeErrorMode mode = new ChangeErrorMode(ErrorModes.SEM_FAILCRITICALERRORS | ErrorModes.SEM_NOGPFAULTERRORBOX))
          {
            status.exit_event.Reset();

            if (!status.proc.Start()) {
              status.running = false;
              result.ExitCode = status.proc.ExitCode;
              //return result;
              return -1;  //
            }
            else {
              status.running = true;
            }

            //if (!status.proc.Start()) {
            //  status.running = false;
            //  //status.exit_event.Set();
            //  return -1;
            //}
            //status.running = true;
          }
        }

        // Reads the output stream first and then waits because deadlocks are possible
        status.proc.BeginOutputReadLine();
        status.proc.BeginErrorReadLine();

        // Creates task to wait for process exit using timeout
        Task<bool> timeout_done = Task.Run(() => status.proc.WaitForExit(timeout));

        // Create task to wait for process exit and closing all output streams
        Task<bool[]> process_done = Task.WhenAll(timeout_done, out_done.Task, err_done.Task);

        // Waits process completion and then checks it was not completed by timeout
        if (await Task.WhenAny(Task.Delay(timeout), process_done) == process_done && timeout_done.Result)
        {
          ////status.proc.CancelOutputRead();
          ////status.proc.CancelErrorRead();

          result.ExitCode = status.proc.ExitCode;
          ////result.Output = out_buff.ToString();
          ////result.Error = err_buff.ToString();
          result.Error = err;
          exit_code =  status.proc.ExitCode;
        }

        else {
          try { status.proc.Kill(); }
          catch {}
        }
        

        // NEW
        //bool ok = await Task.WhenAny(Task.Delay(timeout), process_done) == process_done && timeout_done.Result;
        //if (!ok) {
        //  try { status.proc.Kill(); }
        //  catch {}
        //}

      }  // using process

      status.exit_event.Set(); // <--
      //return result;
      return exit_code;
    }



    //
    public bool Abort()
    {
      bool ok = ThreadPool.QueueUserWorkItem(new WaitCallback(AbortMethod));
      return ok;
    }



    //
    protected static void AbortMethod(object data) 
    {
      if (status.proc == null) {
        return;
      }
      bool ok = false;

      FreeConsole();
      AttachConsole((uint)status.proc.Id);
      SetConsoleCtrlHandler(null, true);

      if (!GenerateConsoleCtrlEvent(CTRL_C_EVENT, 0)) {
        ok = false;
      }

      status.exit_event.WaitOne(3000);

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

