using System;
using System.Drawing;
using System.Windows.Forms;


namespace jumbo
{
  public partial class FormConfig : Form
  {
    // Error from AppConfig
    bool error = false;


    // ctor
    public FormConfig()
    {
      InitializeComponent();
      Application.Idle += Application_Idle;
      PopulateUI();
    }


    // Enable/disable controls
    private void Application_Idle(object sender, EventArgs e)
    {
      if (TextBoxFFMPEG.Text.Trim() != String.Empty) {
        ButtonOK.Enabled = true;
        ButtonOK.ForeColor = Color.Black;
        ButtonOK.BackColor = Color.LightGray;
      }
      else {
        ButtonOK.Enabled = false;
        ButtonOK.ForeColor = Color.White;
        ButtonOK.BackColor = Color.FromArgb(235,235,235);
      }
    }


    // FFMPEG button handler
    private void ButtonFFMPEG_Click(object sender, EventArgs e)
    {
      openFileDialog1.FileName = "ffmpeg.exe";
      if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
      TextBoxFFMPEG.Text = openFileDialog1.FileName;
    }


    // Output button handler
    private void ButtonOutput_Click(object sender, EventArgs e)
    {
      if (folderBrowserDialog1.ShowDialog() != DialogResult.OK) return;
      TextBoxOutput.Text = folderBrowserDialog1.SelectedPath;
    }


    // OK button handler
    private void ButtonOK_Click(object sender, EventArgs e)
    {
      error = false;
      try {
        AppConfig.PathFFMPEG = TextBoxFFMPEG.Text.Trim();
        AppConfig.OutputDirectory = TextBoxOutput.Text.Trim();
        AppConfig.OverwriteOutput = CheckBoxOverwrite.Checked;
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "Error");
        error = true;
        return;
      }

      this.Close();
    }


    // Cancel button handler
    private void ButtonCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }


    // Populate UI from config file
    protected void PopulateUI()
    {
      try {
        TextBoxFFMPEG.Text = AppConfig.PathFFMPEG;
        TextBoxOutput.Text = AppConfig.OutputDirectory;
        CheckBoxOverwrite.Checked = AppConfig.OverwriteOutput;
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "Error");
      }
    }


    // Don't close if there was error, or config file is invalid
    private void FormConfig_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.DialogResult == DialogResult.Cancel) return;
      if (error || !AppConfig.Valid) e.Cancel = true;
    }


  }  // class
}    // namespace

