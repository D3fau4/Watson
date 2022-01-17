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
            UnityAssets file = new UnityAssets(args[0], true);

            foreach (var da in file.GetAssetsOfType(AssetsTools.NET.Extra.AssetClassID.Sprite))
            {
                var asd = file.AM.GetTypeInstance(file.Assets, da).GetBaseField();

                var name = asd.Get("m_Name").GetValue().AsString();
                Console.WriteLine(name);
            }
        }
    }
}