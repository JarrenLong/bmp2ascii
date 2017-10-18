using AForge.Video.FFMPEG;
using AForge.Video.VFW;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace BitmapToASCII.Video
{
  public class AsciiVideo
  {
    public AsciiFont Font { get; set; }
    public AsciiColoringMode Coloring { get; set; }
    public int Quality { get; set; }
    public byte EdgeDetectThreshold { get; set; }
    public bool ScaleBackToOriginalSize { get; set; }

    public List<string> Convert(string srcVideo, string destVideo = "output-test.avi", bool textOnly = false)
    {
      //AVIWriter avi = null;
      var ret = new List<string>();

      if (Directory.Exists("temp"))
        Directory.Delete("temp", true);

      Directory.CreateDirectory("temp");

      // Read in the source video and dump all of the frames to a working directory
      using (var vfr = new VideoFileReader())
      {
        vfr.Open(srcVideo);
        for (var i = 0; i < vfr.FrameCount; i++)
          vfr.ReadVideoFrame().Save(".\\temp\\" + i + ".bmp");
        vfr.Close();
      }

      var files = Directory.GetFiles("temp");
      var c = "";
      switch (Coloring)
      {
        case AsciiColoringMode.NoColor: c = "0"; break;
        case AsciiColoringMode.DowncodeTo8Bit: c = "8"; break;
        case AsciiColoringMode.DowncodeTo15Bit: c = "15"; break;
        case AsciiColoringMode.DowncodeTo16Bit: c = "16"; break;
      }

      // Kick off the CLI converter, dump the result to the same file the source was in
      System.Threading.Tasks.Parallel.ForEach(files,
        new System.Threading.Tasks.ParallelOptions() { MaxDegreeOfParallelism = (int)Math.Ceiling((Environment.ProcessorCount * 0.75) * 1.0) },
        (f) =>
        {
          var p = new Process();
          p.StartInfo.RedirectStandardOutput = true;
          p.StartInfo.UseShellExecute = false;
          p.StartInfo.CreateNoWindow = true;
          p.StartInfo.FileName = "bmp2ascii.exe";
          p.StartInfo.Arguments = string.Format("-i {0} -o {0} -ot {0}.txt -f {1} -q {2} -e {3} {4} {5} {6}", f, Font.Name, Quality, EdgeDetectThreshold, string.IsNullOrEmpty(c) ? "" : "-c " + c, textOnly ? "-t" : "", ScaleBackToOriginalSize ? "-r" : "").Trim();
          p.Start();
          p.WaitForExit();
        });

      // Stitch all of the converted frames back into an AVI
      files = Directory.GetFiles("temp").Where(x => !x.Contains("txt")).ToArray();
      var orderedFrames = new Dictionary<int, string>();
      foreach (var f in files)
      {
        var id = int.Parse(new FileInfo(f).Name.Replace(".bmp", "").Replace("_converted", ""));
        orderedFrames.Add(id, f);
      }
      // Make sure the temp files are all in the right order
      files = orderedFrames.OrderBy(x => x.Key).Select(x => x.Value).ToArray();

      // Finally, stuff it all back into an AVI
      using (var avi = new AVIWriter())
      {
        using (var tmp = Image.FromFile(files[0]))
          avi.Open(destVideo, tmp.Width, tmp.Height);

        foreach (var f in files)
        {
          using (var tmp = Image.FromFile(f))
            avi.AddFrame((Bitmap)tmp);
          File.Delete(f);
        }
      }

      //// Cleanup the working directory
      //Directory.Delete("temp", true);

      return ret;
    }
  }
}
