using DtxCS;
using DtxCS.DataTypes;
using System;
using System.IO;
using System.Text;

namespace DTXTool
{
  internal class DTXTool
  {
    static void PrintUsage()
    {
      Console.WriteLine();
      Console.WriteLine("usage:");
      Console.WriteLine("- dta2b [source .dta] [desination .dtb] [version] (--encrypt)");
      Console.WriteLine("  converts a text DTA file to a binary DTB/*_DTA_* file of specified version (1, 2 or 3), and optionally encrypt");
      Console.WriteLine("- dtb2a [source .dtb] [desination .dta]");
      Console.WriteLine("  converts a binary DTB/*_DTA_* file to a text DTA file");
      Console.WriteLine("- dtb2b [source .dtb] [desination .dtb] [version] (--encrypt)");
      Console.WriteLine("  performs an operation on binary DTB/*_DTA_* files, e.g. version change or (de|en)cryption");
    }

    static void Main(string[] args)
    {
      int version = 1;
      bool encrypt = false;
      if (args.Length == 0)
      {
        PrintUsage();
        return;
      }
      switch (args[0])
      {
        case "dta2b":
          if (args.Length != 4 && args.Length != 5)
          {
            PrintUsage();
            break;
          }
          version = int.Parse(args[3]);
          if (args.Length == 5 && (args[4] == "--encrypt" || args[4] == "-e")) encrypt = true;
          using (FileStream fi = new FileStream(args[1], FileMode.Open))
          using (FileStream fo = new FileStream(args[2], FileMode.OpenOrCreate))
          {
            DataArray a = DTX.FromDtaStream(fi);
            // set output to suitable size
            fo.SetLength(fi.Length * 3);
            fi.Close();
            long size = DTX.ToDtb(a, fo, version, encrypt);
            fo.SetLength(size + (encrypt ? 4 : 0)); // size + 4 extra bytes for key
            fo.Close();
          }
          break;
        case "dtb2a":
          if (args.Length != 3)
          {
            PrintUsage();
            break;
          }
          using (FileStream fi = new FileStream(args[1], FileMode.Open))
          using (FileStream fo = new FileStream(args[2], FileMode.OpenOrCreate))
          {
            bool encrypted = false;
            version = DTX.DtbVersion(fi, ref encrypted);
            Console.WriteLine($"DTB Version {version} {(encrypted ? "(Encrypted)" : "")}");
            fi.Position = 0;
            DataArray a = DTX.FromDtb(fi);
            fi.Close();
            var sb = new StringBuilder();
            foreach (var x in a.Children)
            {
              sb.AppendLine(x.ToString(0));
            }
            byte[] dta = Encoding.UTF8.GetBytes(sb.ToString());
            fo.Write(dta, 0, dta.Length);
            fo.Close();
          }
          break;
        case "dtb2b":
          if (args.Length != 4 && args.Length != 5)
          {
            PrintUsage();
            break;
          }
          int targetVersion = int.Parse(args[3]);
          if (args.Length == 5 && (args[4] == "--encrypt" || args[4] == "-e")) encrypt = true;
          using (FileStream fi = new FileStream(args[1], FileMode.Open))
          using (FileStream fo = new FileStream(args[2], FileMode.OpenOrCreate))
          {
            bool encrypted = false;
            version = DTX.DtbVersion(fi, ref encrypted);
            Console.WriteLine($"DTB Version {version} {(encrypted ? "(Encrypted)" : "")}");
            fi.Position = 0;
            DataArray a = DTX.FromDtb(fi);
            fi.Close();
            if (targetVersion == 0) targetVersion = version;
            DTX.ToDtb(a, fo, targetVersion, encrypt);
            fo.Close();
          }
          break;
        default:
          PrintUsage();
          break;
      }
    }
  }
}
