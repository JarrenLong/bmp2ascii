using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BitmapToASCII.Screensaver
{
  public partial class ScreenSaverForm : Form
  {
    #region Win32 API functions

    [DllImport("user32.dll")]
    static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

    [DllImport("user32.dll")]
    static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

    #endregion


    private Point mouseLocation;
    private bool previewMode = false;
    private Random rand = new Random();

    public ScreenSaverForm()
    {
      SetStyle(ControlStyles.UserPaint, true);
      SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      DoubleBuffered = true;

      InitializeComponent();
    }

    public ScreenSaverForm(Rectangle Bounds)
    {
      SetStyle(ControlStyles.UserPaint, true);
      SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      DoubleBuffered = true;

      InitializeComponent();
      this.Bounds = Bounds;
    }

    public ScreenSaverForm(IntPtr PreviewWndHandle)
    {
      SetStyle(ControlStyles.UserPaint, true);
      SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      DoubleBuffered = true;

      InitializeComponent();

      // Set the preview window as the parent of this window
      SetParent(this.Handle, PreviewWndHandle);

      // Make this a child window so it will close when the parent dialog closes
      SetWindowLong(this.Handle, -16, new IntPtr(GetWindowLong(this.Handle, -16) | 0x40000000));

      // Place our window inside the parent
      Rectangle ParentRect;
      GetClientRect(PreviewWndHandle, out ParentRect);
      Size = ParentRect.Size;
      Location = new Point(0, 0);

      previewMode = true;
    }

    private AsciiFont font = null;
    private string srcFile = "";
    private void InitGenerator()
    {
      font = AsciiFont.LoadConsola();
      srcFile = "..\\..\\testFiles\\smb-logo.png";
    }

    private void ScreenSaverForm_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (!previewMode)
        Application.Exit();
    }

    private void ScreenSaverForm_Load(object sender, EventArgs e)
    {
      LoadSettings();
      InitGenerator();

      Cursor.Hide();
      TopMost = true;

      timer1.Interval = 10 /* 100 FPS  (approx) */;
      timer1.Start();
    }

    private void ScreenSaverForm_MouseClick(object sender, MouseEventArgs e)
    {
      if (!previewMode)
        Application.Exit();
    }

    private void ScreenSaverForm_MouseMove(object sender, MouseEventArgs e)
    {
      if (!previewMode)
      {
        if (!mouseLocation.IsEmpty)
        {
          // Terminate if mouse is moved a significant distance
          if (Math.Abs(mouseLocation.X - e.X) > 5 ||
              Math.Abs(mouseLocation.Y - e.Y) > 5)
            Application.Exit();
        }

        // Update current mouse location
        mouseLocation = e.Location;
      }
    }

    private int r = 0, g = 0, b = 0;
    private int rStep = 0, gStep = 0, bStep = 0;
    private bool rUp = true, gUp = true, bUp = true;

    private static void NextColor(ref int curColor, int step, ref bool up)
    {
      if (up && curColor + step > 255)
      {
        curColor = 255;
        up = false;
      }
      else if (!up && curColor - step < 0)
      {
        curColor = 0;
        up = true;
      }
      else
      {
        if (up)
          curColor += step;
        else
          curColor -= step;
      }
    }

    private void ScreenSaverForm_Paint(object sender, PaintEventArgs e)
    {
      e.Graphics.Clear(BackColor);

      // Figure out what size font to use to get everything to fit on screen
      Font font;
      SizeF dims;
      float sizeInPixels = 20.1f;
      do
      {
        sizeInPixels -= 0.1f;
        font = new Font("Consolas", sizeInPixels, FontStyle.Regular, GraphicsUnit.Pixel, 0, false);
        dims = e.Graphics.MeasureString(ascii, font);
      } while (dims.Width > Width || dims.Height > Height);

      NextColor(ref r, rStep, ref rUp);
      NextColor(ref g, gStep, ref gUp);
      NextColor(ref b, bStep, ref bUp);

      Color textColor = Color.FromArgb(255, r, g, b);
      Brush brushColor = new SolidBrush(textColor);

      var box = new Rectangle(
        (int)((Width - dims.Width - 2) / 2), // X
        (int)((Height - dims.Height - 2) / 2), // Y
        (int)(dims.Width + 2), // Width
        (int)(dims.Height + 2)); // Height
      // Draw the ASCII art
      e.Graphics.DrawString(ascii, font, brushColor, box);

      string fmt = "{0}: {1}, step: {2}, direction: {3}";
      e.Graphics.DrawString(string.Format(fmt, "Red", r, rStep, rUp ? "Up" : "Down"), font, brushColor, new PointF(0, 0));
      e.Graphics.DrawString(string.Format(fmt, "Green", g, gStep, gUp ? "Up" : "Down"), font, brushColor, new PointF(0, 24));
      e.Graphics.DrawString(string.Format(fmt, "Blue", b, bStep, bUp ? "Up" : "Down"), font, brushColor, new PointF(0, 48));
    }

    string ascii = "";
    string lastImg = "";

    private void timer1_Tick(object sender, EventArgs e)
    {
      try
      {
        if (lastImg != srcFile)
        {
          lastImg = srcFile;

          // Start with a light gray color
          r = 192;
          g = 192;
          b = 192;

          ascii = "Loading ...";
          Invalidate();

          // Load the source image
          Bitmap src;
          using (var streamReader = new StreamReader(srcFile))
            src = (Bitmap)Image.FromStream(streamReader.BaseStream);

          // Do the image -> ASCII conversion
          ascii = src.ToAscii(font, 60, AsciiColoringMode.NoColor, true, 120, true).ToString();

          // Pick random starting colors, steps, and directions
          Random rnd = new Random();
          r = rnd.Next(0, 255);
          g = rnd.Next(0, 255);
          b = rnd.Next(0, 255);

          rUp = rnd.Next(0, 1000) % 2 == 0;
          gUp = rnd.Next(0, 1000) % 2 == 1;
          bUp = rnd.Next(0, 1000) > 500;

          rStep = rnd.Next(2, 24);
          gStep = rnd.Next(2, 24);
          bStep = rnd.Next(2, 24);
        }
      }
      catch
      {
        ascii = "Could not open image! :/";
      }

      Invalidate();
    }

    private void LoadSettings()
    {
      //// Use the string from the Registry if it exists
      //RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Demo_ScreenSaver");
      //if (key == null)
      //  textLabel.Text = "C# Screen Saver";
      //else
      //  textLabel.Text = (string)key.GetValue("text");
    }
  }
}
