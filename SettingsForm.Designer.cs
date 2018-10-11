namespace BitmapToASCII.Screensaver
{
    partial class SettingsForm
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
            if (disposing && (components != null))
            {
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
      this.TextBoxImagePath = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.okButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
      this.ButtonBrowse = new System.Windows.Forms.Button();
      this.NumericUpDownRotateEvery = new System.Windows.Forms.NumericUpDown();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownRotateEvery)).BeginInit();
      this.SuspendLayout();
      // 
      // TextBoxImagePath
      // 
      this.TextBoxImagePath.Location = new System.Drawing.Point(89, 94);
      this.TextBoxImagePath.Name = "TextBoxImagePath";
      this.TextBoxImagePath.ReadOnly = true;
      this.TextBoxImagePath.Size = new System.Drawing.Size(254, 20);
      this.TextBoxImagePath.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.ForeColor = System.Drawing.Color.Red;
      this.label1.Location = new System.Drawing.Point(15, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(364, 25);
      this.label1.TabIndex = 1;
      this.label1.Text = "Image to ASCII Screensaver Settings";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(21, 47);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(165, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Jarren Long, Books N\' Bytes, Inc.";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 98);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(71, 13);
      this.label3.TabIndex = 3;
      this.label3.Text = "Image Folder:";
      // 
      // okButton
      // 
      this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.okButton.Location = new System.Drawing.Point(157, 352);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 4;
      this.okButton.Text = "OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.CausesValidation = false;
      this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelButton.Location = new System.Drawing.Point(268, 352);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 5;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // ButtonBrowse
      // 
      this.ButtonBrowse.Location = new System.Drawing.Point(349, 93);
      this.ButtonBrowse.Name = "ButtonBrowse";
      this.ButtonBrowse.Size = new System.Drawing.Size(75, 23);
      this.ButtonBrowse.TabIndex = 6;
      this.ButtonBrowse.Text = "&Browse ...";
      this.ButtonBrowse.UseVisualStyleBackColor = true;
      this.ButtonBrowse.Click += new System.EventHandler(this.ButtonBrowse_Click);
      // 
      // numericUpDown1
      // 
      this.NumericUpDownRotateEvery.Location = new System.Drawing.Point(130, 120);
      this.NumericUpDownRotateEvery.Name = "numericUpDown1";
      this.NumericUpDownRotateEvery.Size = new System.Drawing.Size(56, 20);
      this.NumericUpDownRotateEvery.TabIndex = 7;
      this.NumericUpDownRotateEvery.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(18, 124);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(106, 13);
      this.label4.TabIndex = 8;
      this.label4.Text = "Rotate Images Every";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(192, 124);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(47, 13);
      this.label5.TabIndex = 9;
      this.label5.Text = "seconds";
      // 
      // SettingsForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(478, 387);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.NumericUpDownRotateEvery);
      this.Controls.Add(this.ButtonBrowse);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.TextBoxImagePath);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.Name = "SettingsForm";
      this.Text = "Screensaver Settings";
      ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownRotateEvery)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBoxImagePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.Button ButtonBrowse;
    private System.Windows.Forms.NumericUpDown NumericUpDownRotateEvery;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
  }
}