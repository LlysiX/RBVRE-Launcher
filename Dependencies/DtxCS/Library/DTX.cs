﻿using System;
using System.Globalization;
using System.Text;
using DtxCS.DataTypes;

namespace DtxCS
{
  /// <summary>
  /// Represents a .dta/.dtx file.
  /// </summary>
  public static class DTX
  {
    /// <summary>
    /// Parses a plaintext DTA given its bytes in a byte array. If an encoding tag is set, tries to use the correct encoding.
    /// </summary>
    /// <param name="dtaBytes"></param>
    /// <returns></returns>
    public static DataArray FromPlainTextBytes(byte[] dtaBytes)
    {
      DataArray dta;
      try
      {
        dta = FromDtaString(Encoding.GetEncoding(1252).GetString(dtaBytes));
      }
      catch (Exception)
      {
        return FromDtaString(Encoding.UTF8.GetString(dtaBytes));
      }
      bool utf8 = false;
      foreach (DataNode d in dta.Children)
      {
        if (d != null && d is DataArray && ((DataArray)d).Array("encoding") != null && ((DataArray)d).Array("encoding").Children[1].Name == "utf8")
        {
          utf8 = true;
          break;
        }
      }
      if (utf8)
      {
        dta = FromDtaString(Encoding.UTF8.GetString(dtaBytes));
      }
      return dta;
    } //FromPlainTextBytes

    /// <summary>
    /// Parses the entirety of a .dta file in plain text into a DataArray.
    /// </summary>
    /// <param name="data"></param>
    public static DataArray FromDtaString(string data)
    {
      DataArray root = new DataArray();
      ParseString(data, root);
      return root;
    }

    /// <summary>
    /// Parses the entirety of a .dta file in a stream to a DataArray.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static DataArray FromDtaStream(System.IO.Stream data)
      => FromDtaString(new UTF8Encoding(false).GetString(data.ReadBytes((int)data.Length)));

    enum ParseState
    {
      whitespace,
      in_string,
      in_literal,
      in_symbol,
      in_comment
    }

    /// <summary>
    /// Parses the string as DTA elements, adding each one to the given root array.
    /// </summary>
    /// <param name="data">string of DTA info</param>
    /// <param name="root">top-level array to add the string to</param>
    private static void ParseString(string data, DataArray root)
    {
      ParseState state = ParseState.whitespace;
      data += " "; // this ensures we parse the whole string...
      DataArray current = root;
      string tmp_literal = "";
      int line = 1;
      for (int i = 0; i < data.Length; i++)
      {
        if (data[i] == '\uFEFF') continue;
        if (data[i] == '\n') line++;
        switch (state)
        {
          case ParseState.whitespace:
            switch (data[i])
            {
              case '\'':
                tmp_literal = "";
                state = ParseState.in_symbol;
                break;
              case '"':
                tmp_literal = "";
                state = ParseState.in_string;
                break;
              case ';':
                tmp_literal = "";
                state = ParseState.in_comment;
                break;
              case ' ':
              case '\r':
              case '\n':
              case '\t':
                continue;
              case '}':
              case ')':
              case ']':
                if (data[i] != current.ClosingChar || current.Parent == null)
                {
                  throw new Exception($"Mismatched closing brace encountered at line {line}.");
                }
                current = current.Parent;
                break;
              case '(':
                current = (DataArray)current.AddNode(new DataArray());
                break;
              case '{':
                current = (DataArray)current.AddNode(new DataCommand());
                break;
              case '[':
                current = (DataArray)current.AddNode(new DataMacroDefinition());
                break;
              default:
                state = ParseState.in_literal;
                tmp_literal = new string(data[i], 1);
                continue;
            }
            break;
          case ParseState.in_string:
            switch (data[i])
            {
              case '"':
                current.AddNode(new DataAtom(tmp_literal));
                state = ParseState.whitespace;
                break;
              default:
                tmp_literal += data[i];
                continue;
            }
            break;
          case ParseState.in_literal:
            switch (data[i])
            {
              case ' ':
              case '\r':
              case '\n':
              case '\t':
                AddLiteral(current, tmp_literal);
                state = ParseState.whitespace;
                break;
              case '}':
              case ')':
              case ']':
                AddLiteral(current, tmp_literal);
                if (data[i] != current.ClosingChar)
                {
                  throw new Exception("Mismatched brace types encountered.");
                }
                current = current.Parent;
                state = ParseState.whitespace;
                break;
              case '(':
                AddLiteral(current, tmp_literal);
                current = (DataArray)current.AddNode(new DataArray());
                break;
              case '{':
                AddLiteral(current, tmp_literal);
                current = (DataArray)current.AddNode(new DataCommand());
                break;
              case '[':
                AddLiteral(current, tmp_literal);
                current = (DataArray)current.AddNode(new DataMacroDefinition());
                break;
              default:
                tmp_literal += data[i];
                continue;
            }
            break;
          case ParseState.in_symbol:
            switch (data[i])
            {
              case ' ':
              case '\r':
              case '\n':
              case '\t':
                throw new Exception("Whitespace encountered in symbol.");
              case '}':
              case ')':
              case ']':
                current.AddNode(DataSymbol.Symbol(tmp_literal));
                if (data[i] != current.ClosingChar)
                {
                  throw new Exception("Mismatched brace types encountered.");
                }
                current = current.Parent;
                state = ParseState.whitespace;
                break;
              case '\'':
                current.AddNode(DataSymbol.Symbol(tmp_literal));
                state = ParseState.whitespace;
                break;
              default:
                tmp_literal += data[i];
                continue;
            }
            break;
          case ParseState.in_comment:
            switch (data[i])
            {
              case '\r':
              case '\n':
                state = ParseState.whitespace;
                break;
              default:
                continue;
            }
            break;
        }
      }
    }

    private static void AddLiteral(DataArray current, string tmp_literal)
    {
      int tmp_int;
      float tmp_float;
      if (int.TryParse(tmp_literal, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out tmp_int))
      {
        current.AddNode(new DataAtom(tmp_int));
      }
      else if (float.TryParse(tmp_literal, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out tmp_float))
      {
        current.AddNode(new DataAtom(tmp_float));
      }
      else if (tmp_literal[0] == '$')
      {
        current.AddNode(DataVariable.Var(tmp_literal.Substring(1)));
      }
      else
      {
        current.AddNode(DataSymbol.Symbol(tmp_literal));
      }
    }

    /// <summary>
    /// Parses a binary format (dtb) file.
    /// </summary>
    /// <param name="dtb"></param>
    public static DataArray FromDtb(System.IO.Stream dtb)
    {
      DataArray root;
      dtb.Position = 0;
      if (dtb.ReadUInt8() != 0x01)
      {
        dtb.Position = 0;
        dtb = new CryptStream(dtb);
        if (dtb.ReadUInt8() != 0x01)
        {
          throw new Exception("DTB contained unrecognized header.");
        }
      }
      uint rootNodes = dtb.ReadUInt16LE();
      dtb.Position = 1;
      uint rootNodes2 = dtb.ReadUInt32LE();
      dtb.Position = 3;
      if (rootNodes == 0)
      {
        dtb.Position = 5;
        rootNodes = dtb.ReadUInt32LE();
        uint unk = dtb.ReadUInt16LE();
        root = parse_children(dtb, rootNodes, DataType.ARRAY, 2);
      }
      else if (rootNodes == rootNodes2)
      {
        dtb.Position = 5;
        rootNodes = dtb.ReadUInt16LE();
        uint unk = dtb.ReadUInt16LE();
        root = parse_children(dtb, rootNodes, DataType.ARRAY, 3);
      }
      else
      {
        dtb.ReadInt32LE(); // unknown, always = 1
        root = parse_children(dtb, rootNodes, DataType.ARRAY, 1);
      }

      return root;
    }

    /// <summary>
    /// Parses a binary format (dtb) file and returns the version and encryption state.
    /// </summary>
    /// <param name="dtb"></param>
    /// <param name="encrypted"></param>
    public static int DtbVersion(System.IO.Stream dtb, ref bool encrypted)
    {
      dtb.Position = 0;
      encrypted = false;
      if (dtb.ReadUInt8() != 0x01)
      {
        dtb.Position = 0;
        dtb = new CryptStream(dtb);
        if (dtb.ReadUInt8() != 0x01)
          throw new Exception("DTB contained unrecognized header.");
        encrypted = true;
      }
      uint rootNodes = dtb.ReadUInt16LE();
      dtb.Position = 1;
      uint rootNodes2 = dtb.ReadUInt32LE();
      dtb.Position = 3;
      if (rootNodes == 0)
        return 2;
      else if (rootNodes == rootNodes2)
        return 3;
      else
        return 1;
    }

    /// <summary>
    /// Converts a DataArray to a binary format (DTB) file.
    /// </summary>
    public static long ToDtb(DataArray arr, System.IO.Stream dtb, int version = 3, bool crypt = false)
    {
      if (crypt) dtb = new CryptStream(dtb);
      dtb.Position = 0;
      dtb.WriteUInt8(0x01);
      if (version == 2)
      {
        dtb.WriteInt32LE(0); //unknown
        dtb.WriteInt32LE(arr.Count); //rootNodes
        dtb.WriteInt16LE(0); //unknown
      }
      else if (version == 3)
      {
        dtb.WriteInt32LE(1); //unknown
        dtb.WriteInt16LE((short)arr.Count); //rootNodes
        dtb.WriteInt16LE(1); //unknown
      }
      else
      {
        dtb.WriteInt16LE((short)arr.Count); //rootNodes
        dtb.WriteInt32LE(0); //unknown
      }
      write_children(arr, dtb, arr.Count, version);
      return dtb.Length;
    }

    static void write_children(DataArray dta, System.IO.Stream dtb, int numChildren, int version = 1)
    {
      for (int i = 0; i < numChildren; i++)
      {
        DataType t = dta.Children[i].Type;
        dtb.WriteInt32LE((int)t);
        switch (t)
        {
          case DataType.INT:
            dtb.WriteInt32LE(((DataAtom)dta.Children[i]).Int);
            break;
          case DataType.FLOAT:
            dtb.WriteFloat(((DataAtom)dta.Children[i]).Float);
            break;
          case DataType.VARIABLE:
            dtb.WriteLengthUTF8(((DataVariable)dta.Children[i]).Name.Substring(1));
            break;
          case DataType.SYMBOL:
            dtb.WriteLengthUTF8(((DataSymbol)dta.Children[i]).Name);
            break;
          case DataType.ARRAY:
          case DataType.COMMAND:
          case DataType.MACRO:
            if (version == 2)
            {
              dtb.WriteInt32LE(0); //unknown
              dtb.WriteInt32LE(((DataArray)dta.Children[i]).Count); //rootNodes
              dtb.WriteInt16LE(0); //unknown
            }
            else if (version == 3)
            {
              dtb.WriteInt32LE(1); //unknown
              dtb.WriteInt16LE((short)((DataArray)dta.Children[i]).Count); //rootNodes
              dtb.WriteInt16LE(1); //unknown
            }
            else
            {
              dtb.WriteInt16LE((short)((DataArray)dta.Children[i]).Count); //rootNodes
              dtb.WriteInt32LE(0); //unknown
            }
            write_children((DataArray)(dta.Children[i]), dtb, ((DataArray)dta.Children[i]).Count, version);
            break;
          case DataType.STRING:
            dtb.WriteLengthUTF8(((DataAtom)dta.Children[i]).String);
            break;
          case DataType.EMPTY:
            dtb.WriteUInt32LE(0);
            break;
          case DataType.IFDEF:
          case DataType.IFNDEF:
          case DataType.ELSE:
          case DataType.ENDIF:
          case DataType.INCLUDE:
          case DataType.MERGE:
          case DataType.AUTORUN:
          case DataType.UNDEF:
            // directives
            if (((DataDirective)dta.Children[i]).Constant != null)
              dtb.WriteLengthUTF8(((DataDirective)dta.Children[i]).Constant);
            else
              dtb.WriteUInt32LE(0);
            break;
          case DataType.DEFINE:
            dtb.WriteLengthUTF8(((DataDefine)dta.Children[i]).Constant);
            numChildren--;
            write_children((DataArray)((DataDefine)dta.Children[i]).Definition, dtb, 1, version);
            break;
          default:
            Console.WriteLine($"unimplemented {t} in write_children");
            break;
        }
      }
    }

    static DataArray parse_children(System.IO.Stream s, uint numChildren, DataType type = DataType.ARRAY, int version = 1)
    {
      DataArray ret = type == DataType.MACRO ? new DataMacroDefinition()
                            : type == DataType.COMMAND ? new DataCommand()
                            : new DataArray();
      while (numChildren-- > 0)
      {
        DataType t = (DataType)s.ReadInt32LE();
        switch (t)
        {
          case DataType.INT:
            ret.AddNode(new DataAtom(s.ReadInt32LE()));
            break;
          case DataType.FLOAT:
            ret.AddNode(new DataAtom(s.ReadFloat()));
            break;
          case DataType.VARIABLE:
            ret.AddNode(DataVariable.Var(s.ReadLengthUTF8()));
            break;
          case DataType.SYMBOL:
            ret.AddNode(DataSymbol.Symbol(s.ReadLengthUTF8()));
            break;
          case DataType.ARRAY:
          case DataType.COMMAND:
          case DataType.MACRO:
            if (version == 2)
            {
              s.Position += 4;
              uint nC = s.ReadUInt32LE();
              ushort unk = s.ReadUInt16LE();
              ret.AddNode(parse_children(s, nC, t, version));
            }
            else if (version == 3)
            {
              s.Position += 4;
              ushort nC = s.ReadUInt16LE();
              s.Position += 2;
              ret.AddNode(parse_children(s, nC, t, version));
            }
            else
            {
              ushort nC = s.ReadUInt16LE(); // numChildren
              s.Position += 4; // id
              ret.AddNode(parse_children(s, nC, t, version));
            }
            break;
          case DataType.STRING:
            ret.AddNode(new DataAtom(s.ReadLengthUTF8()));
            break;
          case DataType.EMPTY:
            s.Position += 4;
            break;
          case DataType.DEFINE:
            var constant = s.ReadLengthUTF8();
            numChildren--;
            var definition = parse_children(s, 1, DataType.ARRAY, version).Array(0);
            ret.AddNode(new DataDefine(constant, definition));
            break;
          case DataType.IFDEF:
            ret.AddNode(new DataIfDef(s.ReadLengthUTF8()));
            break;
          case DataType.IFNDEF:
            ret.AddNode(new DataIfNDef(s.ReadLengthUTF8()));
            break;
          case DataType.ELSE:
            s.Position += 4;
            ret.AddNode(new DataElse());
            break;
          case DataType.ENDIF:
            s.Position += 4;
            ret.AddNode(new DataEndIf());
            break;
          case DataType.INCLUDE:
            ret.AddNode(new DataInclude(s.ReadLengthUTF8()));
            break;
          case DataType.MERGE:
            ret.AddNode(new DataMerge(s.ReadLengthUTF8()));
            break;
          case DataType.AUTORUN:
            s.Position += 4;
            ret.AddNode(new DataAutorun());
            break;
          case DataType.UNDEF:
            ret.AddNode(new DataUndef(s.ReadLengthUTF8()));
            break;
          default:
            throw new Exception("Unhandled DTB DataType " + Enum.GetName(typeof(DataType), t));
        }
      }
      return ret;
    }
  }
}
