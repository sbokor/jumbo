namespace jumbo
{
  partial class Form1
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
      this.TextBoxDetails = new System.Windows.Forms.RichTextBox();
      this.ButtonOK = new System.Windows.Forms.Button();
      this.TextBoxInput = new System.Windows.Forms.TextBox();
      this.TextBoxOutput = new System.Windows.Forms.TextBox();
      this.LabelOutput = new System.Windows.Forms.Label();
      this.ButtonOutput = new System.Windows.Forms.Button();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.LabelInput = new System.Windows.Forms.Label();
      this.ButtonCancel = new System.Windows.Forms.Button();
      this.LabelStatus = new System.Windows.Forms.Label();
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.CheckBoxDetails = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // TextBoxDetails
      // 
      this.TextBoxDetails.AcceptsTab = true;
      this.TextBoxDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TextBoxDetails.BackColor = System.Drawing.Color.White;
      this.TextBoxDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.TextBoxDetails.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.TextBoxDetails.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.TextBoxDetails.Location = new System.Drawing.Point(12, 140);
      this.TextBoxDetails.Name = "TextBoxDetails";
      this.TextBoxDetails.ReadOnly = true;
      this.TextBoxDetails.Size = new System.Drawing.Size(528, 199);
      this.TextBoxDetails.TabIndex = 1;
      this.TextBoxDetails.Text = "";
      // 
      // ButtonOK
      // 
      this.ButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.ButtonOK.BackColor = System.Drawing.Color.LightGray;
      this.ButtonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.ButtonOK.Location = new System.Drawing.Point(460, 93);
      this.ButtonOK.Name = "ButtonOK";
      this.ButtonOK.Size = new System.Drawing.Size(80, 30);
      this.ButtonOK.TabIndex = 0;
      this.ButtonOK.Text = "OK";
      this.ButtonOK.UseVisualStyleBackColor = false;
      this.ButtonOK.Click += new System.EventHandler(this.ButtonOK_Click);
      // 
      // TextBoxInput
      // 
      this.TextBoxInput.AllowDrop = true;
      this.TextBoxInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TextBoxInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.TextBoxInput.Location = new System.Drawing.Point(117, 12);
      this.TextBoxInput.Name = "TextBoxInput";
      this.TextBoxInput.Size = new System.Drawing.Size(423, 21);
      this.TextBoxInput.TabIndex = 2;
      this.TextBoxInput.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextBoxInput_DragDrop);
      this.TextBoxInput.DragOver += new System.Windows.Forms.DragEventHandler(this.TextBoxInput_DragOver);
      // 
      // TextBoxOutput
      // 
      this.TextBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TextBoxOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.TextBoxOutput.Location = new System.Drawing.Point(117, 46);
      this.TextBoxOutput.Name = "TextBoxOutput";
      this.TextBoxOutput.Size = new System.Drawing.Size(374, 21);
      this.TextBoxOutput.TabIndex = 4;
      // 
      // LabelOutput
      // 
      this.LabelOutput.AutoSize = true;
      this.LabelOutput.Location = new System.Drawing.Point(12, 46);
      this.LabelOutput.Name = "LabelOutput";
      this.LabelOutput.Size = new System.Drawing.Size(79, 15);
      this.LabelOutput.TabIndex = 5;
      this.LabelOutput.Text = "Output (mp4)";
      // 
      // ButtonOutput
      // 
      this.ButtonOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.ButtonOutput.BackColor = System.Drawing.Color.LightGray;
      this.ButtonOutput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.ButtonOutput.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ButtonOutput.Location = new System.Drawing.Point(501, 46);
      this.ButtonOutput.Margin = new System.Windows.Forms.Padding(0);
      this.ButtonOutput.Name = "ButtonOutput";
      this.ButtonOutput.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
      this.ButtonOutput.Size = new System.Drawing.Size(39, 21);
      this.ButtonOutput.TabIndex = 6;
      this.ButtonOutput.Text = "...";
      this.ButtonOutput.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.ButtonOutput.UseVisualStyleBackColor = false;
      this.ButtonOutput.Click += new System.EventHandler(this.ButtonOutput_Click);
      // 
      // LabelInput
      // 
      this.LabelInput.AutoSize = true;
      this.LabelInput.Location = new System.Drawing.Point(12, 15);
      this.LabelInput.Name = "LabelInput";
      this.LabelInput.Size = new System.Drawing.Size(59, 15);
      this.LabelInput.TabIndex = 3;
      this.LabelInput.Text = "URL (hsl)";
      // 
      // ButtonCancel
      // 
      this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.ButtonCancel.BackColor = System.Drawing.Color.LightGray;
      this.ButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.ButtonCancel.Location = new System.Drawing.Point(360, 93);
      this.ButtonCancel.Name = "ButtonCancel";
      this.ButtonCancel.Size = new System.Drawing.Size(80, 30);
      this.ButtonCancel.TabIndex = 7;
      this.ButtonCancel.Text = "Cancel";
      this.ButtonCancel.UseVisualStyleBackColor = false;
      this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
      // 
      // LabelStatus
      // 
      this.LabelStatus.AutoSize = true;
      this.LabelStatus.Location = new System.Drawing.Point(12, 80);
      this.LabelStatus.Name = "LabelStatus";
      this.LabelStatus.Size = new System.Drawing.Size(42, 15);
      this.LabelStatus.TabIndex = 8;
      this.LabelStatus.Text = "Ready";
      // 
      // progressBar1
      // 
      this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar1.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
      this.progressBar1.Location = new System.Drawing.Point(12, 100);
      this.progressBar1.Maximum = 1000;
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(330, 23);
      this.progressBar1.Step = 1;
      this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
      this.progressBar1.TabIndex = 9;
      // 
      // CheckBoxDetails
      // 
      this.CheckBoxDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.CheckBoxDetails.AutoSize = true;
      this.CheckBoxDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.CheckBoxDetails.Location = new System.Drawing.Point(281, 78);
      this.CheckBoxDetails.Name = "CheckBoxDetails";
      this.CheckBoxDetails.Size = new System.Drawing.Size(61, 19);
      this.CheckBoxDetails.TabIndex = 10;
      this.CheckBoxDetails.Text = "Details";
      this.CheckBoxDetails.UseVisualStyleBackColor = true;
      this.CheckBoxDetails.CheckedChanged += new System.EventHandler(this.CheckBoxDetails_CheckedChanged);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.ClientSize = new System.Drawing.Size(554, 350);
      this.Controls.Add(this.CheckBoxDetails);
      this.Controls.Add(this.progressBar1);
      this.Controls.Add(this.LabelStatus);
      this.Controls.Add(this.ButtonCancel);
      this.Controls.Add(this.ButtonOutput);
      this.Controls.Add(this.LabelOutput);
      this.Controls.Add(this.TextBoxOutput);
      this.Controls.Add(this.LabelInput);
      this.Controls.Add(this.TextBoxInput);
      this.Controls.Add(this.ButtonOK);
      this.Controls.Add(this.TextBoxDetails);
      this.DoubleBuffered = true;
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.MinimumSize = new System.Drawing.Size(500, 179);
      this.Name = "Form1";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Jumbo";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
      this.ResizeBegin += new System.EventHandler(this.Form1_ResizeBegin);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.RichTextBox TextBoxDetails;
    private System.Windows.Forms.Button ButtonOK;
    private System.Windows.Forms.TextBox TextBoxInput;
    private System.Windows.Forms.TextBox TextBoxOutput;
    private System.Windows.Forms.Label LabelOutput;
    private System.Windows.Forms.Button ButtonOutput;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.Label LabelInput;
    private System.Windows.Forms.Button ButtonCancel;
    private System.Windows.Forms.Label LabelStatus;
    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.CheckBox CheckBoxDetails;
  }
}

