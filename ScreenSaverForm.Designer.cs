namespace BitmapToASCII.Screensaver
{
  partial class ScreenSaverForm
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
      this.components = new System.ComponentModel.Container();
      this.TimerCycleColor = new System.Windows.Forms.Timer(this.components);
      this.TimerCycleImages = new System.Windows.Forms.Timer(this.components);
      this.SuspendLayout();
      // 
      // TimerCycleColor
      // 
      this.TimerCycleColor.Enabled = true;
      this.TimerCycleColor.Interval = 30;
      this.TimerCycleColor.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // TimerCycleImages
      // 
      this.TimerCycleImages.Enabled = true;
      this.TimerCycleImages.Interval = 30000;
      this.TimerCycleImages.Tick += new System.EventHandler(this.TimerCycleImages_Tick);
      // 
      // ScreenSaverForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Black;
      this.ClientSize = new System.Drawing.Size(640, 480);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "ScreenSaverForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Form1";
      this.Load += new System.EventHandler(this.ScreenSaverForm_Load);
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.ScreenSaverForm_Paint);
      this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ScreenSaverForm_KeyPress);
      this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ScreenSaverForm_MouseClick);
      this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ScreenSaverForm_MouseMove);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Timer TimerCycleColor;
    private System.Windows.Forms.Timer TimerCycleImages;
  }
}

