using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace jumbo
{
  public partial class Form1 : Form
  {
    public class AppStatus
    {
      protected string _status = "Ready";  

      public AsyncProcess proc = null;
      public AutoResetEvent exit_event = new AutoResetEvent(false);     
      public int duration = 0;         // total length in microsec
      public double percent = 0;       // % completed
      public string eta = "";          // ETA hh:mm:ss 

      public delegate void StatusEventHandler();
      public event StatusEventHandler StatusChanged;

      // Ready, Analyzing, Downloading, Done, Aborted
      public string status {
        set {
          _status = value;
         if (StatusChanged != null) StatusChanged();
        }
        get {
          return _status;
        }
      }
    }


    //
    protected AppStatus app = null;


    // ctor
    public Form1()
    {
      InitializeComponent();
      Application.Idle += Application_Idle;

      TextBoxDetails.Tag = TextBoxDetails.Height;
      TextBoxDetails.Visible = false;
      this.Height -= (int)TextBoxDetails.Tag;

      app = new AppStatus();
      app.StatusChanged += OnStatus;
    }

    
    // Enable/disable controls
    private void Application_Idle(object sender, EventArgs e)
    {
      // OK button
      if (string.IsNullOrWhiteSpace(TextBoxInput.Text) ||
          string.IsNullOrWhiteSpace(TextBoxOutput.Text) ||
          (app.proc != null && app.proc.IsRunning))
      {
        ButtonOK.Enabled = false;
        ButtonOK.ForeColor = Color.White;
        ButtonOK.BackColor = Color.FromArgb(235,235,235);
      }
      else {
        ButtonOK.Enabled = true;
        ButtonOK.ForeColor = Color.Black;
        ButtonOK.BackColor = Color.LightGray;
      }

      // Output selection button
      if (app.proc != null && app.proc.IsRunning) {
        ButtonOutput.Enabled = false;
        ButtonOutput.ForeColor = Color.White;
        ButtonOutput.BackColor = Color.FromArgb(235,235,235);
      }
      else {
        ButtonOutput.Enabled = true;
        ButtonOutput.ForeColor = Color.Black;
        ButtonOutput.BackColor = Color.LightGray;
      }

      // Cancel button
      if (app.proc != null && app.proc.IsRunning) {
        ButtonCancel.Enabled = true;
        ButtonCancel.ForeColor = Color.Black;
        ButtonCancel.BackColor = Color.LightGray;
      }
      else {
        ButtonCancel.Enabled = false;
        ButtonCancel.ForeColor = Color.White;
        ButtonCancel.BackColor = Color.FromArgb(235,235,235);
      }

      // Text boxes
      if (app.proc != null && app.proc.IsRunning) {
        TextBoxInput.ReadOnly = true;
        TextBoxInput.BackColor = Color.FromArgb(235,235,235);
        TextBoxOutput.ReadOnly = true;
        TextBoxOutput.BackColor = Color.FromArgb(235,235,235);
      }
      else {
        TextBoxInput.ReadOnly = false;
        TextBoxInput.BackColor = Color.White;
        TextBoxOutput.ReadOnly = false;
        TextBoxOutput.BackColor = Color.White;
      }
    }



    private async void ButtonOK_Click(object sender, EventArgs e)
    {
      // Init
      ShowProgress("");
      TextBoxDetails.Text = "";
      app.exit_event.Reset();

      string cmd = @"C:\bin\ffmpeg\bin\ffmpeg.exe";
      string opt = "";
      int timeout = -1;  //ms

      opt += $"-hide_banner -stats -progress pipe:1 ";
      opt += $"-i {TextBoxInput.Text} ";    
      opt += $"-c copy -bsf:a aac_adtstoasc ";
      opt += $"{TextBoxOutput.Text} ";

      //opt += $"-ss 00:00:10 -vframes 2 -vf scale=\"400:-1\" -q:v 1 {textBox2.Text}_%01d.jpg ";
      //opt += $"-y ";

      app.proc = new AsyncProcess();
      app.proc.OutputCallback += OnOutput;
      app.proc.ErrorCallback += OnError;

      Task<int> t = app.proc.Run(cmd, opt, timeout);
      int ret = await t;

      if (app.exit_event.WaitOne(100)) {
        Application.Exit();
      }

      app.status = "Done";

      //if (ret != 0) {
      //}

    }



    // Application events

    // Status changed
    protected void OnStatus()
    {
      this.Invoke(new Action(() => {
        LabelStatus.Text = $"{app.status}";
        if (app.status == "Downloading") LabelStatus.Text += $" ... {app.eta}";
      }));
    }


    // Standard Output changed
    protected void OnOutput(string data)
    {
      string val = (string)data.Clone();
      if (val.Contains("out_time_us=")) {
        ShowProgress(val);
      }
    }


    // Standard Error changed
    protected void OnError(string data)
    {
      string val = (string)data.Clone();

      if (val.Contains("Duration:")) {
        GetDuration(val);
      }

      if (app.status != "Downloading") {
        app.status = "Analyzing";
      }

      this.Invoke(new Action(() => {
        TextBoxDetails.Text += val + "\r\n";
        TextBoxDetails.SelectionStart = TextBoxDetails.Text.Length;
        TextBoxDetails.ScrollToCaret();
      }));
    }


    // Output file selected
    private void ButtonOutput_Click(object sender, EventArgs e)
    {
      if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
      TextBoxOutput.Text = saveFileDialog1.FileName;
    }


    // Cancel button clicked
    private void ButtonCancel_Click(object sender, EventArgs e)
    {
      app.proc.Abort();
    }



    protected void GetDuration(string line)
    {
      line = line.Trim();
      Regex rx = new Regex(@"^Duration: ([0-9]{2}):([0-9]{2}):([0-9]{2})\.([0-9]+)");
      Match match = rx.Match(line);
      if (match.Groups.Count != 5) return;

      int.TryParse(match.Groups[1].Value, out int h);
      int.TryParse(match.Groups[2].Value, out int m);
      int.TryParse(match.Groups[3].Value, out int s);

      TimeSpan t = new TimeSpan(h, m, s);
      app.duration = (int)t.TotalMilliseconds * 1000;
    }



    protected void GetProgress(string line)
    {
      if (app.duration == 0) return;

      line = line.Trim();
      Regex rx = new Regex(@"out_time_us=([0-9]+)$");
      Match match = rx.Match(line);
      if (match.Groups.Count != 2) return;

      double.TryParse(match.Groups[1].Value, out double us);
      double percent = us/(app.duration/100.00);
      app.percent = Math.Round(percent, 1);

      TimeSpan t = TimeSpan.FromMilliseconds((app.duration - us)/1000);
      app.eta = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
    }



    protected void ShowProgress(string line)
    {
      line = line.Trim();

      if (line == "") {
        this.Invoke(new Action(() => {
          progressBar1.Value = 0;
          progressBar1.Refresh();
          progressBar1.CreateGraphics().DrawString("0%", new Font("Arial", (float)8.25, FontStyle.Bold), 
            Brushes.Black, new PointF(progressBar1.Width / 2 - 10, progressBar1.Height / 2 - 7));
        }));
        return;
      }

      app.status = "Downloading";
      GetProgress(line);

      this.Invoke(new Action(() => {
        progressBar1.Value = (int)(app.percent * 10);
        progressBar1.Refresh();
        progressBar1.CreateGraphics().DrawString(app.percent.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Bold), 
          Brushes.Black, new PointF(progressBar1.Width / 2 - 10, progressBar1.Height / 2 - 7));
      }));
    }



    // Form events

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      app.exit_event.Reset();
      e.Cancel = true;

      if (app.proc == null || !app.proc.IsRunning) {
        e.Cancel = false;
        Application.Exit();
        return;
      }

      string msg = "Abort the download and exit?";
      DialogResult res = MessageBox.Show(msg, "Warning", MessageBoxButtons.YesNo);
      if (res == DialogResult.No) {
        return;
      }

      app.proc.Abort();
      app.exit_event.Set();  //
    }



    private void CheckBoxDetails_CheckedChanged(object sender, EventArgs e)
    {
      this.MaximumSize = new Size(int.MaxValue, int.MaxValue);

      // Show
      if (CheckBoxDetails.Checked) {       
        if (TextBoxDetails.Tag == null) return; // error
        if ((int)TextBoxDetails.Tag == 0) TextBoxDetails.Tag = 199;
        TextBoxDetails.Visible = true;
        this.Height += (int)TextBoxDetails.Tag;
        TextBoxDetails.Focus();
        TextBoxDetails.ScrollToCaret();
      }

      // Hide
      else {
        TextBoxDetails.Tag = TextBoxDetails.Height;
        TextBoxDetails.Visible = false;
        this.Height -= (int)TextBoxDetails.Tag;
      }
    }



    private void TextBoxInput_DragDrop(object sender, DragEventArgs e)
    {
      string url = (string)e.Data.GetData(DataFormats.Text);
      TextBoxInput.Text = url;
    }


    private void TextBoxInput_DragOver(object sender, DragEventArgs e)
    {
      e.Effect = DragDropEffects.None;
      if (!e.Data.GetDataPresent(DataFormats.Text)) return;
      e.Effect = DragDropEffects.Copy;
    }



    private void Form1_ResizeBegin(object sender, EventArgs e)
    {
      this.MaximumSize = new Size(int.MaxValue, int.MaxValue);
      if (CheckBoxDetails.Checked) return;
      this.MaximumSize = new Size(int.MaxValue, this.Height);
    }



  }  // class
}    // namespace

