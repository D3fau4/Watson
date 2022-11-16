﻿using Watson.Lib.Assets;
using Watson.Lib.IO;
using Watson.Lib.Utils;

namespace Watson.Program;

public class Program
{
    public static void Main(string[] args)
    {
        var m_tmpold = new TMPFont(new UnityAssetFile(args[0]), new Assembly(args[1]));
        var m_tmpnew = new TMPFont(new UnityAssetFile(args[2]), new Assembly(args[3]));

        var m = TMPFont_Importer.GetToImportList(m_tmpnew.m_FontNames, m_tmpold.m_FontNames);

        foreach (var fontsnames in m) Console.WriteLine(fontsnames);
    }
}