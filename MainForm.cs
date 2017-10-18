using BitmapToASCII.Video;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace BitmapToASCII.Test
{
  public partial class MainForm : Form
  {
    private AsciiFont _font = null;
    private AsciiImage _result = null;

    /// <summary>
    /// 
    /// </summary>
    public MainForm()
    {
      InitializeComponent();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MainForm_Load(object sender, EventArgs e)
    {
      // Load in the _font we will be using for the ASCII representation
      _font = AsciiFont.LoadConsola();

      ButtonRender.Enabled = false;
      ButtonSave.Enabled = false;
      ButtonSaveText.Enabled = false;
      ButtonTest.Enabled = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      _result?.Dispose();
      _result = null;

      _font?.Dispose();
      _font = null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonLoad_Click(object sender, EventArgs e)
    {
      PictureBoxOriginal.Image?.Dispose();
      PictureBoxOriginal.Image = null;
      PictureBoxFinal.Image?.Dispose();
      PictureBoxFinal.Image = null;
      ButtonRender.Enabled = false;
      ButtonSave.Enabled = false;
      ButtonSaveText.Enabled = false;
      ButtonTest.Enabled = false;
      _result?.Dispose();
      _result = null;

      var ofd = new OpenFileDialog()
      {
        Title = "Select an image file.",
        Filter = "PNG Images(*.png)|*.png|JPEG Images(*.jpg)|*.jpg|Bitmap Images(*.bmp)|*.bmp|All Files(*.*)|*.*"
      };

      if (ofd.ShowDialog() == DialogResult.OK)
      {
        using (var streamReader = new StreamReader(ofd.FileName))
          PictureBoxOriginal.Image = (Bitmap)Image.FromStream(streamReader.BaseStream);

        ButtonRender.Enabled = true;
        ButtonTest.Enabled = true;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonRender_Click(object sender, EventArgs e)
    {
      if (PictureBoxOriginal.Image != null)
      {
        _result?.Dispose();

        var coloring = AsciiColoringMode.Original;
        if (ComboBoxColoring.SelectedItem != null)
        {
          if (ComboBoxColoring.SelectedItem.ToString().Contains("8"))
            coloring = AsciiColoringMode.DowncodeTo8Bit;
          else if (ComboBoxColoring.SelectedItem.ToString().Contains("15"))
            coloring = AsciiColoringMode.DowncodeTo15Bit;
          else if (ComboBoxColoring.SelectedItem.ToString().Contains("16"))
            coloring = AsciiColoringMode.DowncodeTo16Bit;
          else if (ComboBoxColoring.SelectedItem.ToString().ToUpper().Contains("NO"))
            coloring = AsciiColoringMode.NoColor;
        }

        _result = ((Bitmap)PictureBoxOriginal.Image).ToAscii(_font, TrackBarQuality.Value, coloring, CheckBoxTextOnly.Checked, (byte)TrackBarEdgeDetectThreshold.Value, CheckBoxRescale.Checked);

        if (PictureBoxFinal.Image != null)
        {
          PictureBoxFinal.Image.Dispose();
          PictureBoxFinal.Image = null;
        }
        PictureBoxFinal.Image = _result.Image;

        // Output the ASCII text
        var msg = "";
        if (!CheckBoxTextOnly.Checked)
          msg += string.Format("Result Dimensions: {0} x {1} px. = {2} x {3} characters, coloring: {4}, quality setting: {5}, edge threshold: {6}",
            _result.Image.Width, _result.Image.Height,
            _result.Image.Width / _font.CharWidth, _result.Image.Height / _font.CharHeight,
            coloring,
            TrackBarQuality.Value,
            TrackBarEdgeDetectThreshold.Value
          );
        else
          msg += string.Format("Result Dimensions: {0} x {1} characters, quality setting: {2}, edge threshold: {3}",
            _result.Width, _result.Height,
            TrackBarQuality.Value,
            TrackBarEdgeDetectThreshold.Value
          );

        TextBoxResults.Text = msg + Environment.NewLine +
          "--------------------------------------------------------------------------------" + Environment.NewLine +
          _result.ToString();

        ButtonSave.Enabled = true;
        ButtonSaveText.Enabled = true;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonSave_Click(object sender, EventArgs e)
    {
      if (_result?.Image != null)
      {
        var sfd = new SaveFileDialog()
        {
          Title = "Specify a file name and file path",
          Filter = "PNG Images(*.png)|*.png|JPEG Images(*.jpg)|*.jpg|Bitmap Images(*.bmp)|*.bmp|All Files(*.*)|*.*"
        };

        if (sfd.ShowDialog() == DialogResult.OK)
        {
          var fileExtension = Path.GetExtension(sfd.FileName).ToUpper();
          var imgFormat = ImageFormat.Png;

          if (fileExtension == "BMP")
            imgFormat = ImageFormat.Bmp;
          else if (fileExtension == "JPG")
            imgFormat = ImageFormat.Jpeg;

          using (var streamWriter = new StreamWriter(sfd.FileName, false))
            _result.Image.Save(streamWriter.BaseStream, imgFormat);
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonSaveText_Click(object sender, EventArgs e)
    {
      if (_result != null)
      {
        var sfd = new SaveFileDialog()
        {
          Title = "Specify a file name and file path",
          Filter = "Text Files(*.txt)|*.txt|All Files(*.*)|*.*"
        };

        if (sfd.ShowDialog() == DialogResult.OK)
          File.WriteAllText(sfd.FileName, _result.ToString());
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonTest_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("This is gonna take awhile, are you sure you want to run it? This will run the source image through the API at quality settings 25, 50, 75, and 100. For each quality setting, all color modes will be attempted. Each resulting image/text will be saved to a directory.", "Walruses Ahead", MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;

      var saveToPath = ".";
      using (var sdd = new FolderBrowserDialog() { Description = "Where do you want the test results saved?" })
        if (sdd.ShowDialog() == DialogResult.OK)
          saveToPath = sdd.SelectedPath;

      if (!Directory.Exists(saveToPath))
        Directory.CreateDirectory(saveToPath);

      for (var q = 25; q < 101; q += 25)
      {
        foreach (AsciiColoringMode c in Enum.GetValues(typeof(AsciiColoringMode)))
        {
          var fileName = Path.Combine(saveToPath, string.Format("test_{0}_{1}", q, c));

          var buf = ((Bitmap)PictureBoxOriginal.Image).ToAscii(_font, q, c);

          using (var streamWriter = new StreamWriter(fileName + ".png", false))
            buf.Image.Save(streamWriter.BaseStream, ImageFormat.Png);

          File.WriteAllText(fileName + ".txt", buf.ToString());
          buf.Dispose();
        }
      }

      MessageBox.Show("Complete!");
    }

    private void ButtonVideo_Click(object sender, EventArgs e)
    {
      var ofd = new OpenFileDialog()
      {
        Title = "Select a video file.",
        Filter = "All Files(*.*)|*.*"
      };

      var input = "";
      if (ofd.ShowDialog() == DialogResult.OK)
        input = ofd.FileName;

      if (!string.IsNullOrEmpty(input))
      {
        var coloring = AsciiColoringMode.Original;
        if (ComboBoxColoring.SelectedItem != null)
        {
          if (ComboBoxColoring.SelectedItem.ToString().Contains("8"))
            coloring = AsciiColoringMode.DowncodeTo8Bit;
          else if (ComboBoxColoring.SelectedItem.ToString().Contains("15"))
            coloring = AsciiColoringMode.DowncodeTo15Bit;
          else if (ComboBoxColoring.SelectedItem.ToString().Contains("16"))
            coloring = AsciiColoringMode.DowncodeTo16Bit;
          else if (ComboBoxColoring.SelectedItem.ToString().ToUpper().Contains("NO"))
            coloring = AsciiColoringMode.NoColor;
        }

        var sfd = new SaveFileDialog()
        {
          Title = "Save output video to",
          Filter = "AVI Files(*.avi)|*.avi"
        };
        var output = "";
        if (sfd.ShowDialog() == DialogResult.OK)
          output = sfd.FileName;

        var ret = new AsciiVideo()
        {
          Font = _font,
          Coloring = coloring,
          Quality = TrackBarQuality.Value,
          EdgeDetectThreshold = (byte)TrackBarEdgeDetectThreshold.Value,
          ScaleBackToOriginalSize = CheckBoxRescale.Checked
        }.Convert(input, output, CheckBoxTextOnly.Checked);

        MessageBox.Show("Complete!");

        if (CheckBoxTextOnly.Checked)
        {
          // "Animate" the text in the _result window, loop 5 times
          System.Threading.ThreadPool.QueueUserWorkItem((o) =>
          {
            for (var i = 0; i < 5; i++)
            {
              foreach (var frame in ret)
              {
                SetText(frame);
                System.Threading.Thread.Sleep(40);
              }
            }
          });
        }
      }
      else
        MessageBox.Show("Pick an input file first!");
    }

    private void SetText(string val)
    {
      if (InvokeRequired)
      {
        BeginInvoke((Action)(() => { SetText(val); }));
        return;
      }

      TextBoxResults.Text = val;
    }
  }
}
