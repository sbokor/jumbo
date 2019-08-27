using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace jumbo
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
      Application.Idle += Application_Idle;

      richTextBox1.Tag = richTextBox1.Height;
      richTextBox1.Visible = false;
      this.Height -= (int)richTextBox1.Tag;
    }

    //
    protected AsyncProcess proc = null;
    protected AutoResetEvent exit_event = new AutoResetEvent(false);
    protected string status = "";
    protected int duration = 0;  // us


    
    //
    private void Application_Idle(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(textBox1.Text) ||
          string.IsNullOrWhiteSpace(textBox2.Text) ||
          (proc != null && proc.IsRunning))
      {
        button1.Enabled = false;
        button1.ForeColor = Color.White;
        button1.BackColor = Color.FromArgb(235,235,235);
      }
      else {
        button1.Enabled = true;
        button1.ForeColor = Color.Black;
        button1.BackColor = Color.LightGray;
      }


      if (proc != null && proc.IsRunning) {
        button3.Enabled = true;
        button3.ForeColor = Color.Black;
        button3.BackColor = Color.LightGray;
      }
      else {
        button3.Enabled = false;
        button3.ForeColor = Color.White;
        button3.BackColor = Color.FromArgb(235,235,235);
      }
    }



    private async void button1_Click(object sender, EventArgs e)
    {
      status = "";
      label3.Text = "Ready";
      ShowProgress("");
      richTextBox1.Text = "";

      exit_event.Reset();  //


      string cmd = @"C:\bin\ffmpeg\bin\ffmpeg.exe";
      string opt = "";
      int timeout = -1;  //ms

      opt += $"-hide_banner -stats -progress pipe:1 ";  // -v quiet -stats -loglevel info 
      opt += $"-i {textBox1.Text} ";
      //opt += $"-map p:6 ";      
      opt += $"-c copy -bsf:a aac_adtstoasc ";
      opt += $"{textBox2.Text} ";
      //opt += $"-ss 00:00:10 -vframes 2 -vf scale=\"400:-1\" -q:v 1 {textBox2.Text}_%01d.jpg ";
      //opt += $"-y ";

      proc = new AsyncProcess();
      proc.OutputCallback += new AsyncProcess.CallbackEventHandler(OnOutput);
      proc.ErrorCallback += new AsyncProcess.CallbackEventHandler(OnError);

      //Task<AsyncProcess.ProcResult> t = proc.Run(cmd, opt, timeout);
      //AsyncProcess.ProcResult ret = await t;

      Task<int> t = proc.Run(cmd, opt, timeout);
      int ret = await t;

      if (exit_event.WaitOne(100)) {
        Application.Exit();
      }

      label3.Text = "Done";

      //if (ret != 0) {
      //}

    }



    protected void OnOutput(string data)
    {
      string val = (string)data.Clone();

      if (val.Contains("out_time_us=")) {
        ShowProgress(val);
      }

      //this.Invoke(new Action(() => {
      //  richTextBox1.Text += val + "\r\n";
      //  richTextBox1.SelectionStart = richTextBox1.Text.Length;
      //  richTextBox1.ScrollToCaret();
      //}));
    }



    protected void OnError(string data)
    {
      string val = (string)data.Clone();

      if (val.Contains("Duration:")) {
        this.duration = GetDuration(val);
      }
      else if (status != "DONLOADING") {
        this.Invoke(new Action(() => {
           label3.Text = "Analyzing ...";
        }));
      }

      this.Invoke(new Action(() => {
        richTextBox1.Text += val + "\r\n";
        richTextBox1.SelectionStart = richTextBox1.Text.Length;
        richTextBox1.ScrollToCaret();
      }));
    }



    private void button2_Click(object sender, EventArgs e)
    {
      if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
        textBox2.Text = saveFileDialog1.FileName;
      }
    }



    private void button3_Click(object sender, EventArgs e)
    {
      proc.Abort();
    }



    protected int GetDuration(string line)
    {
      line = line.Trim();
      Regex rx = new Regex(@"^Duration: ([0-9]{2}):([0-9]{2}):([0-9]{2})\.([0-9]+)");
      Match match = rx.Match(line);

      if (match.Groups.Count != 5) return 0;

      int.TryParse(match.Groups[1].Value, out int h);
      int.TryParse(match.Groups[2].Value, out int m);
      int.TryParse(match.Groups[3].Value, out int s);

      int us = ((h * 3600) + (m * 60) + s) * 1000000;

      return us;
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

      Regex rx = new Regex(@"out_time_us=([0-9]+)$");
      Match match = rx.Match(line);

      if (match.Groups.Count != 2) return;

      double.TryParse(match.Groups[1].Value, out double us);

      double percent = us/(this.duration/100.00);
      percent = Math.Round(percent, 1);

      this.Invoke(new Action(() => {
        status = "DONLOADING";
        label3.Text = $"Downloading ...";
        progressBar1.Value = (int)(percent * 10);
        progressBar1.Refresh();
        progressBar1.CreateGraphics().DrawString(percent.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Bold), 
          Brushes.Black, new PointF(progressBar1.Width / 2 - 10, progressBar1.Height / 2 - 7));
      }));
    }



    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      exit_event.Reset();
      e.Cancel = true;

      if (proc == null || !proc.IsRunning) {
        e.Cancel = false;
        Application.Exit();
        return;
      }

      string msg = "Abort the download and exit?";
      DialogResult res = MessageBox.Show(msg, "Warning", MessageBoxButtons.YesNo);
      if (res == DialogResult.No) {
        return;
      }

      proc.Abort();
      exit_event.Set();  //
    }



    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
      // Show
      if (checkBox1.Checked) {
        if (richTextBox1.Tag == null) return; // error
        richTextBox1.Visible = true;
        this.Height += (int)richTextBox1.Tag;
        richTextBox1.Focus();
        richTextBox1.ScrollToCaret();
      }

      // Hide
      else {
        if (richTextBox1.Tag == null) richTextBox1.Tag = richTextBox1.Height;
        richTextBox1.Visible = false;
        this.Height -= (int)richTextBox1.Tag;
      }
    }



    private void textBox1_DragDrop(object sender, DragEventArgs e)
    {
      //textBox1.BackColor = Color.White;
      string url = (string)e.Data.GetData(DataFormats.Text);
      textBox1.Text = url;
    }

    private void textBox1_DragEnter(object sender, DragEventArgs e)
    {
      //textBox1.BackColor = Color.LightGray;
    }

    private void textBox1_DragOver(object sender, DragEventArgs e)
    {
      e.Effect = DragDropEffects.None;
      if (e.Data.GetDataPresent(DataFormats.Text)) {
        e.Effect = DragDropEffects.Copy;
      }
    }





  }
}
