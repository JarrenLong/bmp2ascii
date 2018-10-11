using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace BitmapToASCII.Screensaver
{
  public partial class SettingsForm : Form
  {
    public SettingsForm()
    {
      InitializeComponent();
      LoadSettings();
    }

    /// <summary>
    /// Load display text from the Registry
    /// </summary>
    private void LoadSettings()
    {
      RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\AsciiScreensaver");
      if (key == null)
      {
        TextBoxImagePath.Text = "C:\\Users\\Public\\Public Pictures\\";
        NumericUpDownRotateEvery.Value = 30;
      }
      else
      {
        TextBoxImagePath.Text = (string)key.GetValue("ImageFolderPath");
        NumericUpDownRotateEvery.Value = int.Parse((string)key.GetValue("RotateEvery"));
      }
    }

    /// <summary>
    /// Save text into the Registry.
    /// </summary>
    private void SaveSettings()
    {
      // Create or get existing subkey
      RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\AsciiScreensaver");

      key.SetValue("ImageFolderPath", TextBoxImagePath.Text);
      key.SetValue("RotateEvery", NumericUpDownRotateEvery.Value.ToString());
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      SaveSettings();
      Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void ButtonBrowse_Click(object sender, EventArgs e)
    {
      if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
        TextBoxImagePath.Text = folderBrowserDialog1.SelectedPath;
    }
  }
}
