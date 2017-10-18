namespace BitmapToASCII.Test
{
  partial class MainForm
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
      this.PictureBoxOriginal = new System.Windows.Forms.PictureBox();
      this.ButtonLoad = new System.Windows.Forms.Button();
      this.ButtonSave = new System.Windows.Forms.Button();
      this.PictureBoxFinal = new System.Windows.Forms.PictureBox();
      this.TextBoxResults = new System.Windows.Forms.TextBox();
      this.TrackBarQuality = new System.Windows.Forms.TrackBar();
      this.ButtonRender = new System.Windows.Forms.Button();
      this.CheckBoxTextOnly = new System.Windows.Forms.CheckBox();
      this.ButtonSaveText = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.ComboBoxColoring = new System.Windows.Forms.ComboBox();
      this.ButtonTest = new System.Windows.Forms.Button();
      this.ButtonVIdeo = new System.Windows.Forms.Button();
      this.TrackBarEdgeDetectThreshold = new System.Windows.Forms.TrackBar();
      this.label3 = new System.Windows.Forms.Label();
      this.CheckBoxRescale = new System.Windows.Forms.CheckBox();
      ((System.ComponentModel.ISupportInitialize)(this.PictureBoxOriginal)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.PictureBoxFinal)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.TrackBarQuality)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.TrackBarEdgeDetectThreshold)).BeginInit();
      this.SuspendLayout();
      // 
      // PictureBoxOriginal
      // 
      this.PictureBoxOriginal.BackColor = System.Drawing.Color.Silver;
      this.PictureBoxOriginal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.PictureBoxOriginal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.PictureBoxOriginal.Cursor = System.Windows.Forms.Cursors.Cross;
      this.PictureBoxOriginal.Location = new System.Drawing.Point(12, 12);
      this.PictureBoxOriginal.Name = "PictureBoxOriginal";
      this.PictureBoxOriginal.Size = new System.Drawing.Size(480, 360);
      this.PictureBoxOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.PictureBoxOriginal.TabIndex = 13;
      this.PictureBoxOriginal.TabStop = false;
      // 
      // ButtonLoad
      // 
      this.ButtonLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ButtonLoad.Location = new System.Drawing.Point(12, 378);
      this.ButtonLoad.Name = "ButtonLoad";
      this.ButtonLoad.Size = new System.Drawing.Size(150, 46);
      this.ButtonLoad.TabIndex = 15;
      this.ButtonLoad.Text = "Load Image";
      this.ButtonLoad.UseVisualStyleBackColor = true;
      this.ButtonLoad.Click += new System.EventHandler(this.ButtonLoad_Click);
      // 
      // ButtonSave
      // 
      this.ButtonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ButtonSave.Location = new System.Drawing.Point(664, 430);
      this.ButtonSave.Name = "ButtonSave";
      this.ButtonSave.Size = new System.Drawing.Size(154, 46);
      this.ButtonSave.TabIndex = 16;
      this.ButtonSave.Text = "Save Image";
      this.ButtonSave.UseVisualStyleBackColor = true;
      this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
      // 
      // PictureBoxFinal
      // 
      this.PictureBoxFinal.BackColor = System.Drawing.Color.Silver;
      this.PictureBoxFinal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.PictureBoxFinal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.PictureBoxFinal.Cursor = System.Windows.Forms.Cursors.Cross;
      this.PictureBoxFinal.Location = new System.Drawing.Point(498, 12);
      this.PictureBoxFinal.Name = "PictureBoxFinal";
      this.PictureBoxFinal.Size = new System.Drawing.Size(480, 360);
      this.PictureBoxFinal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.PictureBoxFinal.TabIndex = 17;
      this.PictureBoxFinal.TabStop = false;
      // 
      // TextBoxResults
      // 
      this.TextBoxResults.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.TextBoxResults.Location = new System.Drawing.Point(12, 482);
      this.TextBoxResults.Multiline = true;
      this.TextBoxResults.Name = "TextBoxResults";
      this.TextBoxResults.Size = new System.Drawing.Size(966, 467);
      this.TextBoxResults.TabIndex = 18;
      this.TextBoxResults.WordWrap = false;
      // 
      // TrackBarQuality
      // 
      this.TrackBarQuality.AutoSize = false;
      this.TrackBarQuality.LargeChange = 10;
      this.TrackBarQuality.Location = new System.Drawing.Point(168, 378);
      this.TrackBarQuality.Maximum = 100;
      this.TrackBarQuality.Minimum = 5;
      this.TrackBarQuality.Name = "TrackBarQuality";
      this.TrackBarQuality.Size = new System.Drawing.Size(200, 22);
      this.TrackBarQuality.SmallChange = 5;
      this.TrackBarQuality.TabIndex = 19;
      this.TrackBarQuality.TickFrequency = 5;
      this.TrackBarQuality.Value = 30;
      // 
      // ButtonRender
      // 
      this.ButtonRender.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ButtonRender.Location = new System.Drawing.Point(745, 375);
      this.ButtonRender.Name = "ButtonRender";
      this.ButtonRender.Size = new System.Drawing.Size(233, 49);
      this.ButtonRender.TabIndex = 20;
      this.ButtonRender.Text = "&Render!";
      this.ButtonRender.UseVisualStyleBackColor = true;
      this.ButtonRender.Click += new System.EventHandler(this.ButtonRender_Click);
      // 
      // CheckBoxTextOnly
      // 
      this.CheckBoxTextOnly.AutoSize = true;
      this.CheckBoxTextOnly.Location = new System.Drawing.Point(583, 402);
      this.CheckBoxTextOnly.Name = "CheckBoxTextOnly";
      this.CheckBoxTextOnly.Size = new System.Drawing.Size(118, 17);
      this.CheckBoxTextOnly.TabIndex = 21;
      this.CheckBoxTextOnly.Text = "Only Generate Text";
      this.CheckBoxTextOnly.UseVisualStyleBackColor = true;
      // 
      // ButtonSaveText
      // 
      this.ButtonSaveText.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ButtonSaveText.Location = new System.Drawing.Point(824, 430);
      this.ButtonSaveText.Name = "ButtonSaveText";
      this.ButtonSaveText.Size = new System.Drawing.Size(154, 46);
      this.ButtonSaveText.TabIndex = 22;
      this.ButtonSaveText.Text = "Save Text";
      this.ButtonSaveText.UseVisualStyleBackColor = true;
      this.ButtonSaveText.Click += new System.EventHandler(this.ButtonSaveText_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(374, 380);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(96, 13);
      this.label1.TabIndex = 23;
      this.label1.Text = "<== Quality (5-100)";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(495, 380);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(61, 13);
      this.label2.TabIndex = 24;
      this.label2.Text = "Color Mode";
      // 
      // ComboBoxColoring
      // 
      this.ComboBoxColoring.FormattingEnabled = true;
      this.ComboBoxColoring.Items.AddRange(new object[] {
            "Original",
            "8-Bit",
            "15-Bit",
            "16-Bit",
            "NO COLORING (Use ASCII _font default colors)"});
      this.ComboBoxColoring.Location = new System.Drawing.Point(498, 396);
      this.ComboBoxColoring.Name = "ComboBoxColoring";
      this.ComboBoxColoring.Size = new System.Drawing.Size(79, 21);
      this.ComboBoxColoring.TabIndex = 25;
      // 
      // ButtonTest
      // 
      this.ButtonTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ButtonTest.Location = new System.Drawing.Point(408, 432);
      this.ButtonTest.Name = "ButtonTest";
      this.ButtonTest.Size = new System.Drawing.Size(169, 46);
      this.ButtonTest.TabIndex = 26;
      this.ButtonTest.Text = "&Test Battery";
      this.ButtonTest.UseVisualStyleBackColor = true;
      this.ButtonTest.Click += new System.EventHandler(this.ButtonTest_Click);
      // 
      // ButtonVIdeo
      // 
      this.ButtonVIdeo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ButtonVIdeo.Location = new System.Drawing.Point(12, 430);
      this.ButtonVIdeo.Name = "ButtonVIdeo";
      this.ButtonVIdeo.Size = new System.Drawing.Size(150, 46);
      this.ButtonVIdeo.TabIndex = 27;
      this.ButtonVIdeo.Text = "CONVERT VIDEO";
      this.ButtonVIdeo.UseVisualStyleBackColor = true;
      this.ButtonVIdeo.Click += new System.EventHandler(this.ButtonVideo_Click);
      // 
      // TrackBarEdgeDetectThreshold
      // 
      this.TrackBarEdgeDetectThreshold.AutoSize = false;
      this.TrackBarEdgeDetectThreshold.LargeChange = 10;
      this.TrackBarEdgeDetectThreshold.Location = new System.Drawing.Point(168, 402);
      this.TrackBarEdgeDetectThreshold.Maximum = 255;
      this.TrackBarEdgeDetectThreshold.Minimum = 16;
      this.TrackBarEdgeDetectThreshold.Name = "TrackBarEdgeDetectThreshold";
      this.TrackBarEdgeDetectThreshold.Size = new System.Drawing.Size(200, 22);
      this.TrackBarEdgeDetectThreshold.SmallChange = 8;
      this.TrackBarEdgeDetectThreshold.TabIndex = 28;
      this.TrackBarEdgeDetectThreshold.TickFrequency = 16;
      this.TrackBarEdgeDetectThreshold.Value = 160;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(374, 403);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(103, 13);
      this.label3.TabIndex = 29;
      this.label3.Text = "<== Edge Threshold";
      // 
      // CheckBoxRescale
      // 
      this.CheckBoxRescale.AutoSize = true;
      this.CheckBoxRescale.Location = new System.Drawing.Point(583, 379);
      this.CheckBoxRescale.Name = "CheckBoxRescale";
      this.CheckBoxRescale.Size = new System.Drawing.Size(156, 17);
      this.CheckBoxRescale.TabIndex = 30;
      this.CheckBoxRescale.Text = "Rescale to match input size";
      this.CheckBoxRescale.UseVisualStyleBackColor = true;
      // 
      // MainForm
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.ClientSize = new System.Drawing.Size(984, 961);
      this.Controls.Add(this.CheckBoxRescale);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.TrackBarEdgeDetectThreshold);
      this.Controls.Add(this.ButtonVIdeo);
      this.Controls.Add(this.ButtonTest);
      this.Controls.Add(this.ComboBoxColoring);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.ButtonSaveText);
      this.Controls.Add(this.CheckBoxTextOnly);
      this.Controls.Add(this.ButtonRender);
      this.Controls.Add(this.TrackBarQuality);
      this.Controls.Add(this.TextBoxResults);
      this.Controls.Add(this.PictureBoxFinal);
      this.Controls.Add(this.ButtonSave);
      this.Controls.Add(this.ButtonLoad);
      this.Controls.Add(this.PictureBoxOriginal);
      this.DoubleBuffered = true;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Image to ASCII";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
      this.Load += new System.EventHandler(this.MainForm_Load);
      ((System.ComponentModel.ISupportInitialize)(this.PictureBoxOriginal)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.PictureBoxFinal)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.TrackBarQuality)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.TrackBarEdgeDetectThreshold)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox PictureBoxOriginal;
    private System.Windows.Forms.Button ButtonLoad;
    private System.Windows.Forms.Button ButtonSave;
    private System.Windows.Forms.PictureBox PictureBoxFinal;
    private System.Windows.Forms.TextBox TextBoxResults;
    private System.Windows.Forms.TrackBar TrackBarQuality;
    private System.Windows.Forms.Button ButtonRender;
    private System.Windows.Forms.CheckBox CheckBoxTextOnly;
    private System.Windows.Forms.Button ButtonSaveText;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox ComboBoxColoring;
    private System.Windows.Forms.Button ButtonTest;
    private System.Windows.Forms.Button ButtonVIdeo;
    private System.Windows.Forms.TrackBar TrackBarEdgeDetectThreshold;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.CheckBox CheckBoxRescale;
  }
}

