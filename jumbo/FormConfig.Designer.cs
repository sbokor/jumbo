namespace jumbo
{
  partial class FormConfig
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.LabelFFMPEG = new System.Windows.Forms.Label();
      this.TextBoxFFMPEG = new System.Windows.Forms.TextBox();
      this.ButtonFFMPEG = new System.Windows.Forms.Button();
      this.ButtonOutput = new System.Windows.Forms.Button();
      this.TextBoxOutput = new System.Windows.Forms.TextBox();
      this.LabelOutput = new System.Windows.Forms.Label();
      this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
      this.CheckBoxOverwrite = new System.Windows.Forms.CheckBox();
      this.ButtonOK = new System.Windows.Forms.Button();
      this.ButtonCancel = new System.Windows.Forms.Button();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.SuspendLayout();
      // 
      // LabelFFMPEG
      // 
      this.LabelFFMPEG.AutoSize = true;
      this.LabelFFMPEG.Location = new System.Drawing.Point(8, 14);
      this.LabelFFMPEG.Name = "LabelFFMPEG";
      this.LabelFFMPEG.Size = new System.Drawing.Size(86, 15);
      this.LabelFFMPEG.TabIndex = 4;
      this.LabelFFMPEG.Text = "Path to ffmpeg";
      // 
      // TextBoxFFMPEG
      // 
      this.TextBoxFFMPEG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TextBoxFFMPEG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.TextBoxFFMPEG.Location = new System.Drawing.Point(8, 32);
      this.TextBoxFFMPEG.Name = "TextBoxFFMPEG";
      this.TextBoxFFMPEG.Size = new System.Drawing.Size(312, 21);
      this.TextBoxFFMPEG.TabIndex = 5;
      // 
      // ButtonFFMPEG
      // 
      this.ButtonFFMPEG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.ButtonFFMPEG.BackColor = System.Drawing.Color.LightGray;
      this.ButtonFFMPEG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.ButtonFFMPEG.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ButtonFFMPEG.Location = new System.Drawing.Point(328, 32);
      this.ButtonFFMPEG.Margin = new System.Windows.Forms.Padding(0);
      this.ButtonFFMPEG.Name = "ButtonFFMPEG";
      this.ButtonFFMPEG.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
      this.ButtonFFMPEG.Size = new System.Drawing.Size(39, 21);
      this.ButtonFFMPEG.TabIndex = 7;
      this.ButtonFFMPEG.Text = "...";
      this.ButtonFFMPEG.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.ButtonFFMPEG.UseVisualStyleBackColor = false;
      this.ButtonFFMPEG.Click += new System.EventHandler(this.ButtonFFMPEG_Click);
      // 
      // ButtonOutput
      // 
      this.ButtonOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.ButtonOutput.BackColor = System.Drawing.Color.LightGray;
      this.ButtonOutput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.ButtonOutput.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ButtonOutput.Location = new System.Drawing.Point(328, 80);
      this.ButtonOutput.Margin = new System.Windows.Forms.Padding(0);
      this.ButtonOutput.Name = "ButtonOutput";
      this.ButtonOutput.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
      this.ButtonOutput.Size = new System.Drawing.Size(39, 21);
      this.ButtonOutput.TabIndex = 8;
      this.ButtonOutput.Text = "...";
      this.ButtonOutput.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.ButtonOutput.UseVisualStyleBackColor = false;
      this.ButtonOutput.Click += new System.EventHandler(this.ButtonOutput_Click);
      // 
      // TextBoxOutput
      // 
      this.TextBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TextBoxOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.TextBoxOutput.Location = new System.Drawing.Point(8, 80);
      this.TextBoxOutput.Name = "TextBoxOutput";
      this.TextBoxOutput.Size = new System.Drawing.Size(312, 21);
      this.TextBoxOutput.TabIndex = 9;
      // 
      // LabelOutput
      // 
      this.LabelOutput.AutoSize = true;
      this.LabelOutput.Location = new System.Drawing.Point(8, 62);
      this.LabelOutput.Name = "LabelOutput";
      this.LabelOutput.Size = new System.Drawing.Size(100, 15);
      this.LabelOutput.TabIndex = 10;
      this.LabelOutput.Text = "Def. output folder";
      // 
      // CheckBoxOverwrite
      // 
      this.CheckBoxOverwrite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.CheckBoxOverwrite.AutoSize = true;
      this.CheckBoxOverwrite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.CheckBoxOverwrite.Location = new System.Drawing.Point(8, 112);
      this.CheckBoxOverwrite.Name = "CheckBoxOverwrite";
      this.CheckBoxOverwrite.Size = new System.Drawing.Size(138, 19);
      this.CheckBoxOverwrite.TabIndex = 11;
      this.CheckBoxOverwrite.Text = "Overwrite existing file";
      this.CheckBoxOverwrite.UseVisualStyleBackColor = true;
      // 
      // ButtonOK
      // 
      this.ButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.ButtonOK.BackColor = System.Drawing.Color.LightGray;
      this.ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.ButtonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.ButtonOK.Location = new System.Drawing.Point(288, 120);
      this.ButtonOK.Name = "ButtonOK";
      this.ButtonOK.Size = new System.Drawing.Size(80, 32);
      this.ButtonOK.TabIndex = 12;
      this.ButtonOK.Text = "OK";
      this.ButtonOK.UseVisualStyleBackColor = false;
      this.ButtonOK.Click += new System.EventHandler(this.ButtonOK_Click);
      // 
      // ButtonCancel
      // 
      this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.ButtonCancel.BackColor = System.Drawing.Color.LightGray;
      this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.ButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.ButtonCancel.Location = new System.Drawing.Point(192, 120);
      this.ButtonCancel.Name = "ButtonCancel";
      this.ButtonCancel.Size = new System.Drawing.Size(80, 32);
      this.ButtonCancel.TabIndex = 13;
      this.ButtonCancel.Text = "Cancel";
      this.ButtonCancel.UseVisualStyleBackColor = false;
      this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.FileName = "openFileDialog1";
      // 
      // FormConfig
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.ClientSize = new System.Drawing.Size(379, 168);
      this.Controls.Add(this.ButtonCancel);
      this.Controls.Add(this.ButtonOK);
      this.Controls.Add(this.CheckBoxOverwrite);
      this.Controls.Add(this.LabelOutput);
      this.Controls.Add(this.TextBoxOutput);
      this.Controls.Add(this.ButtonOutput);
      this.Controls.Add(this.ButtonFFMPEG);
      this.Controls.Add(this.TextBoxFFMPEG);
      this.Controls.Add(this.LabelFFMPEG);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Name = "FormConfig";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Configuration";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormConfig_FormClosing);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label LabelFFMPEG;
    private System.Windows.Forms.TextBox TextBoxFFMPEG;
    private System.Windows.Forms.Button ButtonFFMPEG;
    private System.Windows.Forms.Button ButtonOutput;
    private System.Windows.Forms.TextBox TextBoxOutput;
    private System.Windows.Forms.Label LabelOutput;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.CheckBox CheckBoxOverwrite;
    private System.Windows.Forms.Button ButtonOK;
    private System.Windows.Forms.Button ButtonCancel;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
  }
}