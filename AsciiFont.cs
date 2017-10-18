using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace BitmapToASCII
{
  /// <summary>
  /// 
  /// </summary>
  [Serializable]
  public class AsciiFont : IDisposable
  {
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int CharWidth { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int CharHeight { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public double AspectRatio { get { return (double)CharHeight / (double)CharWidth; } }
    /// <summary>
    /// 
    /// </summary>
    public string BitmapsPath { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<AsciiChar> Charset { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public AsciiFont()
    {
      Name = "";
      Charset = new List<AsciiChar>();
      CharWidth = 10;
      CharHeight = 16;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
      Name = null;
      CharWidth = 0;
      CharHeight = 0;
      if (Charset != null)
      {
        foreach (var c in Charset)
          c.Dispose();
        Charset.Clear();
      }
      Charset = null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fontName"></param>
    /// <returns></returns>
    public static AsciiFont Load(string fontName)
    {
      try
      {
        var ser = new XmlSerializer(typeof(AsciiFont));
        using (var sr = new StreamReader(string.Format(".{0}fonts{0}{1}.xml", Path.DirectorySeparatorChar, fontName)))
          return (AsciiFont)ser.Deserialize(sr);
      }
      catch { }

      return null;
    }

    /// <summary>
    /// Saves the current object to the XML configuration file
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <remarks></remarks>
    public static void Save(AsciiFont obj)
    {
      try
      {
        var ser = new XmlSerializer(typeof(AsciiFont));
        using (var fs = new FileStream(string.Format(".{0}fonts{0}{1}.xml", Path.DirectorySeparatorChar, obj.Name), FileMode.Create))
          ser.Serialize(fs, obj);
      }
      catch { }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static AsciiFont LoadConsola()
    {
      var f = Load("Consola");
      if (f == null)
        f = new AsciiFont() { Name = "Consola", BitmapsPath = "consola", CharWidth = 10, CharHeight = 16 };

      var path = string.Format(".{0}fonts{0}{1}", Path.DirectorySeparatorChar, f.BitmapsPath);
      if (f.Charset.Count > 0)
      {
        foreach (var c in f.Charset)
          c.FromBitmap(path);
      }
      else
      {
        f.Charset.AddRange(new AsciiChar[]
        {
          AsciiChar.FromBitmap(path, "A"),
          AsciiChar.FromBitmap(path, "B"),
          AsciiChar.FromBitmap(path, "C"),
          AsciiChar.FromBitmap(path, "D"),
          AsciiChar.FromBitmap(path, "E"),
          AsciiChar.FromBitmap(path, "F"),
          AsciiChar.FromBitmap(path, "G"),
          AsciiChar.FromBitmap(path, "H"),
          AsciiChar.FromBitmap(path, "I"),
          AsciiChar.FromBitmap(path, "J"),
          AsciiChar.FromBitmap(path, "K"),
          AsciiChar.FromBitmap(path, "L"),
          AsciiChar.FromBitmap(path, "M"),
          AsciiChar.FromBitmap(path, "N"),
          AsciiChar.FromBitmap(path, "O"),
          AsciiChar.FromBitmap(path, "P"),
          AsciiChar.FromBitmap(path, "Q"),
          AsciiChar.FromBitmap(path, "R"),
          AsciiChar.FromBitmap(path, "S"),
          AsciiChar.FromBitmap(path, "T"),
          AsciiChar.FromBitmap(path, "U"),
          AsciiChar.FromBitmap(path, "V"),
          AsciiChar.FromBitmap(path, "W"),
          AsciiChar.FromBitmap(path, "X"),
          AsciiChar.FromBitmap(path, "Y"),
          AsciiChar.FromBitmap(path, "Z"),
          AsciiChar.FromBitmap(path, "0"),
          AsciiChar.FromBitmap(path, "1"),
          AsciiChar.FromBitmap(path, "2"),
          AsciiChar.FromBitmap(path, "3"),
          AsciiChar.FromBitmap(path, "4"),
          AsciiChar.FromBitmap(path, "5"),
          AsciiChar.FromBitmap(path, "6"),
          AsciiChar.FromBitmap(path, "7"),
          AsciiChar.FromBitmap(path, "8"),
          AsciiChar.FromBitmap(path, "9"),
          AsciiChar.FromBitmap(path, "tick",'`'),
          AsciiChar.FromBitmap(path, "tilde",'~'),
          AsciiChar.FromBitmap(path, "exclamation",'!'),
          AsciiChar.FromBitmap(path, "at",'@'),
          AsciiChar.FromBitmap(path, "pound",'#'),
          AsciiChar.FromBitmap(path, "dollar",'$'),
          AsciiChar.FromBitmap(path, "percent",'%'),
          AsciiChar.FromBitmap(path, "chevron",'^'),
          AsciiChar.FromBitmap(path, "ampersand",'&'),
          AsciiChar.FromBitmap(path, "asterisk",'*'),
          AsciiChar.FromBitmap(path, "lparen",'('),
          AsciiChar.FromBitmap(path, "rparen",')'),
          AsciiChar.FromBitmap(path, "minus",'-'),
          AsciiChar.FromBitmap(path, "underscore",'_'),
          AsciiChar.FromBitmap(path, "plus",'+'),
          AsciiChar.FromBitmap(path, "equals",'='),
          AsciiChar.FromBitmap(path, "lbracket",'['),
          AsciiChar.FromBitmap(path, "lcurly",'{'),
          AsciiChar.FromBitmap(path, "rbracket",']'),
          AsciiChar.FromBitmap(path, "rcurly",'}'),
          AsciiChar.FromBitmap(path, "slash",'\\'),
          AsciiChar.FromBitmap(path, "pipe",'|'),
          AsciiChar.FromBitmap(path, "colon",':'),
          AsciiChar.FromBitmap(path, "semicolon",';'),
          AsciiChar.FromBitmap(path, "singlequote",'\''),
          AsciiChar.FromBitmap(path, "doublequote",'"'),
          AsciiChar.FromBitmap(path, "comma",','),
          AsciiChar.FromBitmap(path, "lt",'<'),
          AsciiChar.FromBitmap(path, "period",'.'),
          AsciiChar.FromBitmap(path, "gt",'>'),
          AsciiChar.FromBitmap(path, "backslash",'/'),
          AsciiChar.FromBitmap(path, "question",'?'),
          AsciiChar.FromBitmap(path, "space",' ')
        });

        Save(f);
      }

      return f;
    }

    public static AsciiFont LoadTwoChar()
    {
      var f = Load("TwoChar");
      if (f == null)
        f = new AsciiFont() { Name = "TwoChar", BitmapsPath = "consola", CharWidth = 10, CharHeight = 16 };

      var path = string.Format(".{0}fonts{0}{1}", Path.DirectorySeparatorChar, f.BitmapsPath);
      if (f.Charset.Count > 0)
      {
        foreach (var c in f.Charset)
          c.FromBitmap(path);
      }
      else
      {
        f.Charset.AddRange(new AsciiChar[]
        {
          AsciiChar.FromBitmap(path, "pound",'#'),
          AsciiChar.FromBitmap(path, "space",' ')
        });

        Save(f);
      }

      return f;
    }

    public static AsciiFont LoadOnlySymbols()
    {
      var f = Load("OnlySymbols");
      if (f == null)
        f = new AsciiFont() { Name = "OnlySymbols", BitmapsPath = "consola", CharWidth = 10, CharHeight = 16 };

      var path = string.Format(".{0}fonts{0}{1}", Path.DirectorySeparatorChar, f.BitmapsPath);
      if (f.Charset.Count > 0)
      {
        foreach (var c in f.Charset)
          c.FromBitmap(path);
      }
      else
      {
        f.Charset.AddRange(new AsciiChar[]
        {
          AsciiChar.FromBitmap(path, "tick",'`'),
          AsciiChar.FromBitmap(path, "tilde",'~'),
          AsciiChar.FromBitmap(path, "exclamation",'!'),
          AsciiChar.FromBitmap(path, "at",'@'),
          AsciiChar.FromBitmap(path, "pound",'#'),
          AsciiChar.FromBitmap(path, "dollar",'$'),
          AsciiChar.FromBitmap(path, "percent",'%'),
          AsciiChar.FromBitmap(path, "chevron",'^'),
          AsciiChar.FromBitmap(path, "ampersand",'&'),
          AsciiChar.FromBitmap(path, "asterisk",'*'),
          AsciiChar.FromBitmap(path, "lparen",'('),
          AsciiChar.FromBitmap(path, "rparen",')'),
          AsciiChar.FromBitmap(path, "minus",'-'),
          AsciiChar.FromBitmap(path, "underscore",'_'),
          AsciiChar.FromBitmap(path, "plus",'+'),
          AsciiChar.FromBitmap(path, "equals",'='),
          AsciiChar.FromBitmap(path, "lbracket",'['),
          AsciiChar.FromBitmap(path, "lcurly",'{'),
          AsciiChar.FromBitmap(path, "rbracket",']'),
          AsciiChar.FromBitmap(path, "rcurly",'}'),
          AsciiChar.FromBitmap(path, "slash",'\\'),
          AsciiChar.FromBitmap(path, "pipe",'|'),
          AsciiChar.FromBitmap(path, "colon",':'),
          AsciiChar.FromBitmap(path, "semicolon",';'),
          AsciiChar.FromBitmap(path, "singlequote",'\''),
          AsciiChar.FromBitmap(path, "doublequote",'"'),
          AsciiChar.FromBitmap(path, "comma",','),
          AsciiChar.FromBitmap(path, "lt",'<'),
          AsciiChar.FromBitmap(path, "period",'.'),
          AsciiChar.FromBitmap(path, "gt",'>'),
          AsciiChar.FromBitmap(path, "backslash",'/'),
          AsciiChar.FromBitmap(path, "question",'?'),
          AsciiChar.FromBitmap(path, "space",' ')
        });

        Save(f);
      }

      return f;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [Serializable]
  public class AsciiChar : IDisposable
  {
    /// <summary>
    /// 
    /// </summary>
    [XmlAttribute]
    public string Name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [XmlAttribute]
    public char Character { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [XmlIgnore]
    public byte[] Pixels { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [XmlIgnore]
    public Bitmap Image { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public AsciiChar()
    {
      Character = ' ';
      Pixels = new byte[] { };
      Image = null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public static AsciiChar FromBitmap(string path, string name, char c = ' ')
    {
      var a = new AsciiChar()
      {
        Name = name,
        Character = name.Length == 1 ? name[0] : c
      };

      // Read in the bitmap from the font directory, pull out all of it's pixel data and stuff it in the bitmask
      using (var sr = new StreamReader(Path.Combine(path, name + ".bmp")))
        a.Image = ((Bitmap)System.Drawing.Image.FromStream(sr.BaseStream)).MakeBlackAndWhite();

      a.Pixels = a.Image.GetPixels();
      return a;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public void FromBitmap(string path)
    {
      // Read in the bitmap from the font directory, pull out all of it's pixel data and stuff it in the bitmask
      using (var sr = new StreamReader(Path.Combine(path, Name + ".bmp")))
        Image = ((Bitmap)System.Drawing.Image.FromStream(sr.BaseStream)).MakeBlackAndWhite();

      Pixels = Image.GetPixels();
    }

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
      Name = null;
      Character = (char)0;
      Pixels = null;
      Image?.Dispose();
      Image = null;
    }
  }

  /// <summary>
  /// Stores the results of a Bitmap->ASCII conversion
  /// </summary>
  public class AsciiImage : IDisposable
  {
    /// <summary>
    /// The width of the generated character array
    /// </summary>
    public int Width { get; set; }
    /// <summary>
    /// The height of the generated character array
    /// </summary>
    public int Height { get; set; }
    /// <summary>
    /// Stores the final ASCII image generated from the source image
    /// </summary>
    public Bitmap Image { get; set; }
    /// <summary>
    /// Stores the final ASCII characters that were picked from the source image
    /// </summary>
    public char[] Text { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public AsciiImage()
    {
      Width = Height = 0;
      Image = null;
      Text = null;
    }

    /// <summary>
    /// Get the ASCII text
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      var buf = "";
      if (Text != null)
      {
        for (var i = 0; i < Width * Height; i++)
        {
          buf += Text[i];
          if (i % Width == 0)
            buf += Environment.NewLine;
        }
      }
      return buf;
    }

    /// <summary>
    /// Clean up
    /// </summary>
    public void Dispose()
    {
      Width = 0;
      Height = 0;
      Text = null;

      Image?.Dispose();
      Image = null;
    }
  }

  /// <summary>
  /// Defines different coloring schemes that can be applied to the resulting ASCII image
  /// </summary>
  public enum AsciiColoringMode : byte
  {
    /// <summary>
    /// Just use the ASCII bitmap itself, don't even try to color it
    /// </summary>
    NoColor = 0,
    /// <summary>
    /// Sample colors unmodified from the original image
    /// </summary>
    Original = 1,
    /// <summary>
    /// Sample colors from the original image, then downcode to an "8-bit" color
    /// </summary>
    DowncodeTo8Bit = 2,
    /// <summary>
    /// Sample colors from the original image, then downcode to an "15-bit" color
    /// </summary>
    DowncodeTo15Bit = 4,
    /// <summary>
    /// Sample colors from the original image, then downcode to an "16-bit" color
    /// </summary>
    DowncodeTo16Bit = 8
  }
}
