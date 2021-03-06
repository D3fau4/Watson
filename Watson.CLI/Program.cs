using AssetsTools.NET;
using AssetsTools.NET.Extra;
using System;
using System.Collections.Generic;
using System.Linq;
using Watson.Lib.IO;


namespace Watson.Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            UnityAssetFile file = new UnityAssetFile(args[0]);

            foreach (var da in file.GetAssetsOfType(AssetsTools.NET.Extra.AssetClassID.MonoBehaviour))
            {
                var name = MonoDeserializer.GetMonoBaseField(file.AM, file.Assets, da, $"E:\\Games\\AI The Somnium Files\\AI_TheSomniumFiles_Data\\Managed");
                var font = name.Get("m_fontInfo");
                if (font != null)
                    Console.WriteLine(name.Get("m_Name").GetValue().AsString());
            }
            
            /*foreach (AssetFileInfoEx info in file.assetFile.table.assetFileInfo)
            {
                Console.WriteLine((AssetClassID)info.curFileType);
            }*/

            file.Close();
        }
    }
}