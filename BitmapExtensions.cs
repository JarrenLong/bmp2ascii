using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;

namespace BitmapToASCII
{
  /// <summary>
  /// 
  /// </summary>
  public static class BitmapExtensions
  {
    /// <summary>
    /// Scales an image to the specified dimensions
    /// </summary>
    /// <param name="src"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    private static Bitmap Scale(this Bitmap src, int width, int height)
    {
      var destRect = new Rectangle(0, 0, width, height);
      var destImage = new Bitmap(width, height);

      destImage.SetResolution(src.HorizontalResolution, src.VerticalResolution);

      using (var graphics = Graphics.FromImage(destImage))
      {
        graphics.CompositingMode = CompositingMode.SourceCopy;
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

        using (var wrapMode = new ImageAttributes())
        {
          wrapMode.SetWrapMode(WrapMode.TileFlipXY);
          graphics.DrawImage(src, destRect, 0, 0, src.Width, src.Height, GraphicsUnit.Pixel, wrapMode);
        }
      }

      return destImage;
    }

    /// <summary>
    /// <a href="http://en.wikipedia.org/wiki/Pearson_correlation">Pearson correlation metric</a>
    /// </summary>
    /// <param name="p"></param>
    /// <param name="q"></param>
    /// <returns>-1 to +1</returns>
    private static double PearsonSimilarityScore(ref byte[] p, ref byte[] q)
    {
      double pSum = 0, qSum = 0, pSumSq = 0, qSumSq = 0, productSum = 0;
      var n = p.Length;

      if (n != q.Length)
        throw new ArgumentException("Input vectors must be of the same dimension.");

      for (var x = 0; x < n; x++)
      {
        pSum += p[x];
        qSum += q[x];
        pSumSq += p[x] * p[x];
        qSumSq += q[x] * q[x];
        productSum += p[x] * q[x];
      }

      var numerator = productSum - ((pSum * qSum) / n);
      var denominator = Math.Sqrt((pSumSq - (pSum * pSum) / n) * (qSumSq - (qSum * qSum) / n));

      return (denominator == 0) ? 0 : numerator / denominator;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static byte[] GetPixels(this Bitmap a)
    {
      var ad = a.LockBits(new Rectangle(0, 0, a.Width, a.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
      var ret = new byte[ad.Stride * ad.Height];
      Marshal.Copy(ad.Scan0, ret, 0, ret.Length);
      a.UnlockBits(ad);
      ad = null;
      return ret;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="w"></param>
    /// <param name="h"></param>
    /// <param name="fmt"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static Bitmap BitmapFromPixels(int w, int h, PixelFormat fmt, ref byte[] data)
    {
      // Smash the bytes into a bitmap and return the result
      var output = new Bitmap(w, h);
      var resultData = output.LockBits(new Rectangle(0, 0, output.Width, output.Height), ImageLockMode.WriteOnly, fmt);
      Marshal.Copy(data, 0, resultData.Scan0, data.Length);
      output.UnlockBits(resultData);
      resultData = null;
      return output;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="src"></param>
    /// <param name="region"></param>
    /// <returns></returns>
    private static Bitmap GetRegion(this Bitmap src, Rectangle region)
    {
      return src.Clone(region, src.PixelFormat);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="src"></param>
    /// <param name="destBitmap"></param>
    /// <param name="destRegion"></param>
    private static void CopyToRegion(this Bitmap src, ref Bitmap destBitmap, Rectangle destRegion)
    {
      using (var g = Graphics.FromImage(destBitmap))
        g.DrawImage(src, destRegion, new Rectangle(0, 0, src.Width, src.Height), GraphicsUnit.Pixel);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    private static Color To8Bit(this Color c)
    {
      var r = (int)(8 * ((double)c.R / 255));
      var g = (int)(8 * ((double)c.G / 255));
      var b = (int)(4 * ((double)c.B / 255));

      return Color.FromArgb(255,
        r == 8 ? 255 : 32 * r,
        g == 8 ? 255 : 32 * g,
        b == 4 ? 255 : 64 * b
        );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    private static Color To15Bit(this Color c)
    {
      return Color.FromArgb(255,
        51 * (int)(5 * ((double)c.R / 255)),
        51 * (int)(5 * ((double)c.G / 255)),
        51 * (int)(5 * ((double)c.B / 255))
        );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    private static Color To16Bit(this Color c)
    {
      return Color.FromArgb(255,
        51 * (int)(5 * ((double)c.R / 255)),
        (int)(42.5 * (int)(6 * ((double)c.G / 255))),
        51 * (int)(5 * ((double)c.B / 255))
        );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="src"></param>
    /// <param name="font"></param>
    /// <param name="quality"></param>
    /// <param name="coloring"></param>
    /// <param name="textOnly"></param>
    /// <param name="edgeDetectThreshold"></param>
    /// <param name="scaleBackToOriginalSize"></param>
    /// <returns></returns>
    public static AsciiImage ToAscii(this Bitmap src, AsciiFont font, int quality, AsciiColoringMode coloring = AsciiColoringMode.Original, bool textOnly = false, byte edgeDetectThreshold = 160, bool scaleBackToOriginalSize = false)
    {
      return src.ToAscii(font,
        (((int)(quality * font.CharHeight * font.AspectRatio)) / font.CharWidth) * font.CharWidth,
        quality * font.CharHeight,
        coloring, textOnly, edgeDetectThreshold, scaleBackToOriginalSize);
    }

    /// <summary>
    /// Pass each frame of an AVI into here, and save. Stitch all frames back together in the final AVI to make ASCII video.
    /// </summary>
    /// <param name="src"></param>
    /// <returns></returns>
    public static AsciiImage ToAscii(this Bitmap src, AsciiFont font, int width = 320, int height = 240, AsciiColoringMode coloring = AsciiColoringMode.Original, bool textOnly = false, byte edgeDetectThreshold = 160, bool scaleBackToOriginalSize = false)
    {
      // Quicky check: Width and height need to be evenly divisible by the size of the ASCII characters bitmaps
      if (((double)width) / font.CharWidth != width / font.CharWidth || ((double)height) / font.CharHeight != height / font.CharHeight)
        throw new Exception("Image size will not work properly for processing");


      var results = new AsciiImage()
      {
        Width = width / font.CharWidth,
        Height = height / font.CharHeight
      };
      results.Text = new char[results.Width * results.Height];

      // Step 1: Scale the image to the specified size
      var sized = src;
      if (sized.Width != width || sized.Height != height)
        sized = sized.Scale(width, height);

      // Step 2: Perform an edge detection on the scaled image, returns the results as a grayscale image of the same dimensions
      var edges = sized.Sobel(edgeDetectThreshold);

      // Create a workspace for the final ASCII image
      results.Image = new Bitmap(width, height, sized.PixelFormat);

      // Step 3: Split the image into a 32x15 grid. Since the image is already scaled down to 320x240, this will give us
      // 480 10x16 px images, same size as the font bitmaps. If they weren't the same size, the comparisons would fail!
      for (var y = 0; y < results.Height; y++)
      {
        for (var x = 0; x < results.Width; x++)
        {
          System.Threading.Tasks.Parallel.Invoke(new System.Threading.Tasks.ParallelOptions() { MaxDegreeOfParallelism = (int)Math.Ceiling((Environment.ProcessorCount * 0.75) * 1.0) },
            () =>
            {
              ProcessCell(ref results, ref font, x, y, ref sized, ref edges, coloring, textOnly);
            });
        }
      }

      if (scaleBackToOriginalSize)
        results.Image = results.Image.Scale(src.Width, src.Height);

      // 5. Render ASCII version of image to new file
      return results;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="results"></param>
    /// <param name="font"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="sized"></param>
    /// <param name="edges"></param>
    /// <param name="coloring"></param>
    /// <param name="textOnly"></param>
    private static void ProcessCell(ref AsciiImage results, ref AsciiFont font, int x, int y, ref Bitmap sized,
      ref Bitmap edges, AsciiColoringMode coloring = AsciiColoringMode.Original, bool textOnly = false)
    {
      // Step 4: foreach cell in the grid, find the closest matching ASCII character, represented by a bitmap
      // Using a Pearson Correlation calculation here to find an appropriate match based on having a high similarity score
      var cell = new Rectangle(x * font.CharWidth, y * font.CharHeight, font.CharWidth, font.CharHeight);
      var edgeCell = edges.GetRegion(cell);

      // Step 4: foreach cell in the grid, find the closest matching ASCII character, represented by a bitmap
      // Using a Pearson Correlation calculation here to find an appropriate match based on having a high similarity score
      var edgeCellPixels = edgeCell.GetPixels();

      // Step 4A. Compare to ASCII bitmap representations, find best matching printable character and store character
      byte[] acPixels = null;
      var scores = new List<KeyValuePair<char, double>>();
      foreach (var ac in font.Charset)
      {
        acPixels = ac.Pixels;
        scores.Add(new KeyValuePair<char, double>(ac.Character,
          PearsonSimilarityScore(ref edgeCellPixels, ref acPixels)));
        acPixels = null;
      }
      edgeCellPixels = null;

      if (textOnly)
      {
        edgeCell.Dispose();
        edgeCell = null;
      }

      // Grab our ASCII bitmap with the highest similarity score and copy it to the proper cell in the resulting bitmap
      var highScoreToUse = scores.OrderByDescending(kvp => kvp.Value).FirstOrDefault().Key;
      var toUse = font.Charset.FirstOrDefault(c => c.Character == highScoreToUse);
      results.Text[y * results.Width + x] = toUse.Character;

      if (textOnly)
        return;

      Bitmap origCell = null, bgCell = null, fgCell = null;
      Color bgColor = Color.Black,
        fgColor = Color.White,
        blk = Color.FromArgb(255, 0, 0, 0),
        wht = Color.FromArgb(255, 255, 255, 255),
        transparent = Color.FromArgb(0, 0, 0, 0);

      if (coloring == AsciiColoringMode.NoColor)
      {
        // Copy the ASCII bitmap to the proper cell in the result image
        // Just using fgCell here to hold a pointer to the resulting image
        fgCell = results.Image;
        toUse.Image.CopyToRegion(ref fgCell, cell);
        fgCell = null;
      }
      else
      {
        using (origCell = sized.GetRegion(cell))
        {
          using (edgeCell)
          {
            // Step 4B. Find average color of portion of original image that is NOT overlapped by ASCII character ("background color"), convert to 8-bit color
            bgColor = origCell.ApplyMask(edgeCell, false).GetMostCommonColor(new[] { blk });
            // Step 4C. Find average color of portion of original image that IS overlapped by ASCII character ("foreground/font color"), convert to 8-bit color
            fgColor = origCell.ApplyMask(edgeCell, true).GetMostCommonColor(new[] { wht });
          }
        }

        // Convert the colors to 8-bit awesomeness
        switch (coloring)
        {
          case AsciiColoringMode.DowncodeTo8Bit:
            bgColor = bgColor.To8Bit();
            fgColor = fgColor.To8Bit();
            break;
          case AsciiColoringMode.DowncodeTo15Bit:
            bgColor = bgColor.To15Bit();
            fgColor = fgColor.To15Bit();
            break;
          case AsciiColoringMode.DowncodeTo16Bit:
            bgColor = bgColor.To16Bit();
            fgColor = fgColor.To16Bit();
            break;
          case AsciiColoringMode.Original:
          default:
            break;
        }

        // Replace the BG/FG colors in the ascii cell with the proper colors
        using (bgCell = (Bitmap)toUse.Image.Clone())
        {
          bgCell.ReplaceColor(wht, transparent);
          bgCell.ReplaceColor(blk, bgColor);

          using (fgCell = (Bitmap)toUse.Image.Clone())
          {
            fgCell.ReplaceColor(blk, transparent);
            fgCell.ReplaceColor(wht, fgColor);

            // Step 4D. Render ASCII bitmap with specified font/BG color to same cell in a new bitmap
            //Overlay the foreground on top of the background to create the final cell image
            //bgCell = bgCell.AddOverlay(fgCell);
            bgCell.AddOverlay(fgCell);
          }

          // Copy the ASCII bitmap to the proper cell in the result image
          // Just using fgCell here to hold a pointer to the resulting image
          fgCell = results.Image;
          bgCell.CopyToRegion(ref fgCell, cell);
          fgCell = null;
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="src"></param>
    /// <returns></returns>
    public static Bitmap MakeBlackAndWhite(this Bitmap src)
    {
      var s = src.GetPixels();

      for (var x = 0; x < s.Length; x += 4)
      {
        // If it's not black, make it white
        if (!(s[x] == 0 && s[x + 1] == 0 && s[x + 2] == 0))
        {
          s[x] = 255;
          s[x + 1] = 255;
          s[x + 2] = 255;
        }
        s[x + 3] = 255;
      }

      // Smash the bytes back into a bitmap and return the result
      var ret = BitmapFromPixels(src.Width, src.Height, src.PixelFormat, ref s);
      s = null;
      return ret;
    }

    private delegate void PixelOperationCallback(ref byte[] buf);

    private static void PixelOperation(this Bitmap image, PixelOperationCallback op = null)
    {
      if (op == null)
        return;

      var imageData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);
      var imageBytes = new byte[imageData.Stride * image.Height];
      Marshal.Copy(imageData.Scan0, imageBytes, 0, imageBytes.Length);

      op(ref imageBytes);

      Marshal.Copy(imageBytes, 0, imageData.Scan0, imageBytes.Length);
      image.UnlockBits(imageData);

      imageData = null;
      imageBytes = null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <param name="mask"></param>
    /// <param name="invertMask"></param>
    /// <returns></returns>
    private static Bitmap ApplyMask(this Bitmap input, Bitmap mask, bool invertMask = false)
    {
      var srcBytes = input.GetPixels();
      var maskBytes = mask.GetPixels();

      ApplyMask(ref srcBytes, ref maskBytes, invertMask);

      // Smash the bytes back into a bitmap and return the result
      var ret = BitmapFromPixels(input.Width, input.Height, input.PixelFormat, ref srcBytes);
      srcBytes = null;
      maskBytes = null;
      return ret;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="arr"></param>
    /// <returns></returns>
    private static int[] PackBytes(byte[] arr)
    {
      var ret = new int[(arr.Length / 4) + 1];

      for (int i = 0, j = 0; i < arr.Length; i += 4, j++)
        ret[j] = arr[i] << 24 | arr[i + 1] << 16 | arr[i + 2] << 8 | arr[i + 3];
      return ret;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="srcBytes"></param>
    /// <param name="maskBytes"></param>
    /// <param name="invertMask"></param>
    private static void ApplyMask(ref byte[] srcBytes, ref byte[] maskBytes, bool invertMask = false)
    {
      var iMask = (byte)(invertMask ? 255 : 0);

      for (var x = 0; x < srcBytes.Length; x += 4)
      {
        if (maskBytes[x] == iMask)
        {
          srcBytes[x + 3] = iMask; // alpha
        }
        else
        {
          srcBytes[x] = 0; // blue
          srcBytes[x + 1] = 0; // green
          srcBytes[x + 2] = 0; // red
          srcBytes[x + 3] = iMask; // alpha
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bg"></param>
    /// <param name="fg"></param>
    private static void AddOverlay(this Bitmap bg, Bitmap fg)
    {
      using (var g = Graphics.FromImage(bg))
        g.DrawImage(fg, new Rectangle(0, 0, fg.Width, fg.Height));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bitmap"></param>
    /// <returns></returns>
    private static IEnumerable<Color> GetPixelColors(Bitmap bitmap)
    {
      var s = bitmap.GetPixels();

      // Chopping out the alpha value so these will be more sortable
      for (var x = 0; x < s.Length; x += 4)
        yield return Color.FromArgb(255, s[x + 2], s[x + 1], s[x]);

      s = null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="src"></param>
    /// <param name="ignore"></param>
    /// <returns></returns>
    private static Color GetMostCommonColor(this Bitmap src, Color[] ignore)
    {
      Color tmp = GetPixelColors(src)
        .Where(color => !ignore.Contains(color))
        .GroupBy(color => color)
        .OrderByDescending(grp => grp.Count())
        .Select(grp => grp.Key)
        .FirstOrDefault();
      return Color.FromArgb(255, tmp.R, tmp.G, tmp.B);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="src"></param>
    /// <param name="ignore"></param>
    /// <returns></returns>
    private static Color GetMostCommonColor2(int[] c, Color[] ignore)
    {
      var ig = new List<int>();
      foreach (var i in ignore)
        ig.Add(i.B << 24 | i.G << 16 | i.R << 8 | i.A);

      var tmp = c
        .Where(color => !ig.Contains(color))
        .GroupBy(color => color)
        .OrderByDescending(grp => grp.Count())
        .Select(grp => grp.Key)
        .FirstOrDefault();
      return Color.FromArgb((byte)(tmp & 0xFF), (byte)((tmp >> 8) & 0xFF), (byte)((tmp >> 16) & 0xFF), (byte)(tmp >> 24 & 0xFF));
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="src"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    private static void ReplaceColor(this Bitmap src, Color from, Color to)
    {
      src.PixelOperation((ref byte[] buf) =>
      {
        byte fr = from.R, fg = from.G, fb = from.B;
        byte tr = to.R, tg = to.G, tb = to.B, ta = to.A;

        for (int x = 0; x < buf.Length; x += 4)
        {
          if (buf[x] == fb && buf[x + 1] == fg && buf[x + 2] == fr)
          {
            buf[x] = tb;
            buf[x + 1] = tg;
            buf[x + 2] = tr;
            buf[x + 3] = ta;
          }
        }
      });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sourceBitmap"></param>
    /// <returns></returns>
    private static Bitmap Sobel(this Bitmap sourceBitmap, byte intensityThreshold = 160, bool grayscale = true)
    {
      byte[] sp = sourceBitmap.GetPixels();
      int len = sp.Length;
      byte[] dest = new byte[len];
      double bx = 0, gx = 0, rx = 0,
        by = 0, gy = 0, ry = 0,
        bt = 0, gt = 0, rt = 0;
      int width = sourceBitmap.Width, height = sourceBitmap.Height,
        filterOffset = 1, calcOffset = 0, byteOffset = 0, stride = len / height, fxy = 0;

      // Convert the image to grayscale
      if (grayscale)
        for (var k = 0; k < len; k += 4)
          sp[k] = sp[k + 1] = sp[k + 2] = (byte)(sp[k] * 0.11f + sp[k + 1] * 0.59f + sp[k + 2] * 0.3f);

      // Edge detection on a per-pixel basis (Sobel algorithm)
      for (var offsetY = filterOffset; offsetY < height - filterOffset; offsetY++)
      {
        for (var offsetX = filterOffset; offsetX < width - filterOffset; offsetX++)
        {
          bx = gx = rx = 0;
          by = gy = ry = 0;
          bt = gt = rt = 0.0;
          byteOffset = offsetY * stride + offsetX * 4;

          for (var filterY = -filterOffset; filterY <= filterOffset; filterY++)
          {
            for (var filterX = -filterOffset; filterX <= filterOffset; filterX++)
            {
              calcOffset = byteOffset + (filterX * 4) + (filterY * stride);
              fxy = (filterX + filterOffset) * 3 + filterY + filterOffset;

              bx += sp[calcOffset] * Sobel3X3Horizontal[fxy];
              gx += sp[calcOffset + 1] * Sobel3X3Horizontal[fxy];
              rx += sp[calcOffset + 2] * Sobel3X3Horizontal[fxy];

              by += sp[calcOffset] * Sobel3X3Vertical[fxy];
              gy += sp[calcOffset + 1] * Sobel3X3Vertical[fxy];
              ry += sp[calcOffset + 2] * Sobel3X3Vertical[fxy];
            }
          }

          bt = Math.Sqrt((bx * bx) + (by * by));
          gt = Math.Sqrt((gx * gx) + (gy * gy));
          rt = Math.Sqrt((rx * rx) + (ry * ry));

          // Not needed now, using intensityThreshold to clamp values in the destination byte array
          //// Clamp the totals to 0-255 for convenient casting to bytes
          //if (bt > 255)
          //  bt = 255;
          //else if (bt < 0)
          //  bt = 0;

          //if (gt > 255)
          //  gt = 255;
          //else if (gt < 0)
          //  gt = 0;

          //if (rt > 255)
          //  rt = 255;
          //else if (rt < 0)
          //  rt = 0;

          // I'm clamping these down so that they'll only represent one of 8 basic colors. BUT...since we're already in grayscale, this will either be black or white.
          dest[byteOffset] = (byte)(bt > intensityThreshold ? 255 : 0);
          dest[byteOffset + 1] = (byte)(gt > intensityThreshold ? 255 : 0);
          dest[byteOffset + 2] = (byte)(rt > intensityThreshold ? 255 : 0);
          dest[byteOffset + 3] = 255; // Fully opaque pixel
        }
      }

      var ret = BitmapFromPixels(width, height, sourceBitmap.PixelFormat, ref dest);
      sp = null;
      dest = null;
      return ret;
    }

    /// <summary>
    /// 
    /// </summary>
    private static readonly double[] Sobel3X3Horizontal =
    {
       -1,  0,  1,
       -2,  0,  2,
       -1,  0,  1
    };

    /// <summary>
    /// 
    /// </summary>
    private static readonly double[] Sobel3X3Vertical =
    {
        1,  2,  1,
        0,  0,  0,
       -1, -2, -1,
    };
  }

  public class BitmapRenderPipeline
  {
    public BitmapRenderPipeline() { }

    public delegate void RenderOperation(ref int srcWidth, ref int srcHeight, ref PixelFormat srcPixelFormat, ref byte[] srcPixels);
    public List<RenderOperation> Operations = new List<RenderOperation>();

    public Bitmap Render(Bitmap source)
    {
      int w = source.Width, h = source.Height;
      var fmt = source.PixelFormat;
      var data = source.GetPixels();

      // Make sure we have something to work with
      if (w == 0 || h == 0 || data == null)
        return source;

      // Run through the list of operations. WARNING: These CAN modify the width/height/format/pixel data of the source bitmap!
      foreach (var op in Operations)
        op(ref w, ref h, ref fmt, ref data);

      // Convert the transformed data back into a bitmap
      var result = BitmapExtensions.BitmapFromPixels(w, h, fmt, ref data);
      data = null;
      return result;
    }
  }
}
