using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace BitmapToASCII.CLI
{
  class Program
  {
    static void Main(string[] args)
    {
      string src = "", dest = "", destTxt = "";
      byte quality = 60, edgeThreshold = 120;
      var coloring = AsciiColoringMode.Original;
      bool textOnly = false, rescale = false;
      AsciiFont font = null;

      CLI_Processor.Register(new[] { "-i", "--src" }, (a) => { src = a[1]; });
      CLI_Processor.Register(new[] { "-o", "--dest" }, (a) => { dest = a[1]; });
      CLI_Processor.Register(new[] { "-ot", "--desttxt" }, (a) => { destTxt = a[1]; });
      CLI_Processor.Register(new[] { "-f", "--font" }, (a) =>
     {
       switch (a[1].ToLower())
       {
         case "twochar":
           font = AsciiFont.LoadTwoChar();
           break;
         case "onlysymbols":
           font = AsciiFont.LoadOnlySymbols();
           break;
         //case "consola":
         default:
           font = AsciiFont.LoadConsola();
           break;
       }
     });
      CLI_Processor.Register(new[] { "-q", "--quality" }, (a) => { quality = Byte.Parse(a[1]); });
      CLI_Processor.Register(new[] { "-e", "--edge" }, (a) => { edgeThreshold = Byte.Parse(a[1]); });
      CLI_Processor.Register(new[] { "-c", "--coloring" }, (a) =>
     {
       switch (a[1].Trim())
       {
         case "0":
           coloring = AsciiColoringMode.NoColor;
           break;
         case "8":
           coloring = AsciiColoringMode.DowncodeTo8Bit;
           break;
         case "15":
           coloring = AsciiColoringMode.DowncodeTo15Bit;
           break;
         case "16":
           coloring = AsciiColoringMode.DowncodeTo16Bit;
           break;
         default:
           coloring = AsciiColoringMode.Original;
           break;
       }
     });
      CLI_Processor.Register(new[] { "-t", "--textonly" }, (a) => { textOnly = true; });
      CLI_Processor.Register(new[] { "-r", "--rescale" }, (a) => { rescale = true; });
      CLI_Processor.Register(new[] { "-h", "--help" }, ShowHelp);
      // Process command line arguments
      CLI_Processor.Parse(args);


      if (string.IsNullOrEmpty(src) || string.IsNullOrEmpty(dest))
        ShowHelp(null);
      else
      {
        // Load the font to use if none was specified
        if (font == null)
          font = AsciiFont.LoadConsola();

        // Perform the conversion
        Convert(src, dest, destTxt, font, quality, edgeThreshold, coloring, textOnly, rescale);
      }
    }

    private static void ShowHelp(string[] args)
    {
      Console.WriteLine("Usage: bmp2ascii [options]");
      Console.WriteLine("  [options]: ");
      Console.WriteLine("  -i, --src        (Required): Path to the input image.");
      Console.WriteLine("  -o, --dest       (Required): Path to the output image/text file.");
      Console.WriteLine("  -f, --font       The font to use. Can be 'twochar' or 'consola', default is consola.");
      Console.WriteLine("  -q, --quality    The quality setting to use, default is 60. Range 0-100");
      Console.WriteLine("  -e, --edge       The edge detection level to use, default is 120. Range: 0-255");
      Console.WriteLine("  -c, --coloring   Coloring mode to use. Can be:");
      Console.WriteLine("    0    -  No coloring at all");
      Console.WriteLine("    8    -  Downcode to 8-bit color");
      Console.WriteLine("    15   -  Downcode to 15-bit color");
      Console.WriteLine("    16   -  Downcode to 16-bit color");
      Console.WriteLine("    Default is 'original'");
      Console.WriteLine("  -t, --textonly   Only save the ASCII text");
      Console.WriteLine("  -r, --rescale    Scale the final image back to the same size as the input image");
      Console.WriteLine();
      Console.WriteLine("Example: bmp2ascii -i test.png -o test-ascii.png");
      Console.WriteLine("  Use the default settings, convert test.png to ASCII image and save result to test-ascii.png");
      Console.WriteLine("Example: bmp2ascii -i test.png -o test-ascii.png -f twochar -q 100 -e 32 -c 8");
      Console.WriteLine("  Convert test.png using twochar font, max quality and a low edge detection setting, downcode to 8-bit color.");
      Console.WriteLine("Example: bmp2ascii -i test.png -o test-ascii.txt -q 80 -e 50");
      Console.WriteLine("  Convert test.png with a higher quality, low edge threshold, but only save the ASCII output.");
    }

    private static void Convert(string s, string d, string dt, AsciiFont f, int q, byte e, AsciiColoringMode c, bool textOnly = false, bool rescale = false)
    {
      // Load the source image
      Bitmap src;
      using (var streamReader = new StreamReader(s))
        src = (Bitmap)Image.FromStream(streamReader.BaseStream);

      // Do the image conversion
      var result = src.ToAscii(f, q, c, textOnly, e, rescale);

      // Save the resulting image
      if (!textOnly)
      {
        var fileExtension = Path.GetExtension(d).ToUpper();
        var imgFormat = ImageFormat.Png;

        if (fileExtension == "BMP")
          imgFormat = ImageFormat.Bmp;
        else if (fileExtension == "JPG")
          imgFormat = ImageFormat.Jpeg;

        using (var streamWriter = new StreamWriter(d, false))
          result.Image.Save(streamWriter.BaseStream, imgFormat);

        if (!string.IsNullOrEmpty(dt))
          File.WriteAllText(dt, result.ToString());
      }
      else
        File.WriteAllText(d, result.ToString());
    }
  }

  #region CLI_Processor
  public static class CLI_Processor
  {
    private static List<CLI_Command> CommandHandlers = new List<CLI_Command>();
    /// <summary>
    /// Register a single command to trigger the specified callback
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="evt"></param>
    /// <remarks></remarks>
    public static void Register(string cmd, CommandCallback evt)
    {
      var c = CommandHandlers.FirstOrDefault(x => x.Command.Equals(cmd));
      // If the command was already registered, update it to use the specified callback
      if (c != null)
        c.Callback = evt;
      else
        // Register the new command
        CommandHandlers.Add(new CLI_Command(cmd) { Callback = evt });
    }

    /// <summary>
    /// Register multiple commands to trigger the same specified callback
    /// </summary>
    /// <param name="cmds"></param>
    /// <param name="evt"></param>
    /// <remarks></remarks>
    public static void Register(string[] cmds, CommandCallback evt)
    {
      if (cmds != null)
        foreach (var c in cmds)
          Register(c, evt);
    }

    /// <summary>
    /// Removes all registered commands
    /// </summary>
    /// <remarks></remarks>
    public static void Reset()
    {
      CommandHandlers.Clear();
    }

    /// <summary>
    /// Check to see if the given command is registered. If true, returns the command, else null.
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns></returns>
    /// <remarks></remarks>
    private static CLI_Command Lookup(string cmd)
    {
      return CommandHandlers.FirstOrDefault(x => x.Command.Equals(cmd));
    }

    /// <summary>
    /// Parses the input command line arguments and fires callbacks as needed
    /// </summary>
    /// <param name="args"></param>
    /// <remarks></remarks>
    public static void Parse(string[] args)
    {
      if (args != null && args.Any())
        for (var i = 0; i <= args.Length - 1; i++)
          Lookup(args[i])?.Callback?.Invoke(args.Skip(i).Take(args.Length - i).ToArray());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    /// <returns></returns>
    /// <remarks></remarks>
    public static bool CaseSensitive { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    /// <remarks></remarks>
    public delegate void CommandCallback(string[] args);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <remarks></remarks>
  public class CLI_Command
  {
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>

    public CLI_Command()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="c"></param>
    /// <remarks></remarks>
    public CLI_Command(string c)
    {
      Command = c;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    /// <returns></returns>
    /// <remarks></remarks>
    public string Command { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public CLI_Processor.CommandCallback Callback;
  }
  #endregion
}
