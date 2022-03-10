﻿using LibForge.Extensions;
using LibForge.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LibForge.Ark
{
  /// <summary>
  /// Version 10 archive file class.
  /// </summary>
  public class Archive
  {
    public FileEntry[] fileEntries;
    public uint[] arkSizes;
    public int[] hashTable;
    public string[] arkNames;
    public byte[] guid;
    public string[][] strings;
    public Archive(string hdrFilename)
    {
      using (var f = File.OpenRead(hdrFilename))
      {
        LoadHdr(f);
      }
    }

    public void WriteArkorder(TextWriter sw)
    {
      long[] arkOffsets = new long[arkSizes.Length];
      long totalOffset = 0;
      for (int i = 0; i < arkSizes.Length; i++)
      {
        arkOffsets[i] = totalOffset;
        totalOffset += arkSizes[i];
      }
      int nextArk = 1;
      foreach(var entry in fileEntries.OrderBy(x => x.Offset))
      {
        if (arkSizes.Length > nextArk && entry.Offset == arkOffsets[nextArk])
        {
          sw.WriteLine("{split_ark}");
          nextArk++;
        }
        sw.WriteLine("('{0}' {1})", entry.Path, entry.Flags);
      }
    }

    private void LoadHdr(Stream file)
    {
      var cryptStream = new EncryptedReadStream(file, 0);
      var r = new BinReader(cryptStream);
      int version = r.Int();
      if (version == -11)
        cryptStream.xor = 0xFF;
      else if (version != 10)
        throw new Exception("Only version 10 arks are supported");
      int unk = r.Check(r.Int(), 1);
      guid = cryptStream.ReadBytes(16);
      int numArks = r.Int();
      arkSizes = r.Arr(r.UInt);
      arkNames = r.Arr(r.String);
      strings = r.Arr(() => r.Arr(r.String));
      fileEntries = r.Arr(() => { var e = new FileEntry(); e.Load(cryptStream); return e; });
      hashTable = r.Arr(r.Int);
    }
  }
}
