using AssetsTools.NET;
using AssetsTools.NET.Extra;
using Watson.Lib.IO;

namespace Watson.Lib.Utils
{
    public static class TMPFont_Importer
    {
        public static List<string> GetToImportList(Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> NewFontNames, 
            Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> OldFontNames)
        {
            List<string> ToImport = new List<string>();
            // Buscar fuentes compatibles para importar
            foreach (var font in OldFontNames)
            {
                foreach (var fontnew in NewFontNames)
                {
                    if (font.Value.Item1.Contains(fontnew.Value.Item1))
                    {
                        ToImport.Add(font.Value.Item1);
                        break;
                    }
                }
            }

            return ToImport;
        }

        public static List<AssetsReplacer> Import(Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> NewFontNames, 
            Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> OldFontNames)
        {
            List<AssetsReplacer> m = new List<AssetsReplacer>();

            foreach (var font in NewFontNames)
            {
                foreach(var fontold in OldFontNames)
                {
                    if (font.Value.Item1.Contains(fontold.Value.Item1))
                    {
                        /* Remplazar m_Script */
                        // Establece el FileID
                        font.Value.Item2["m_Script"]["m_FileID"].GetValue().Set(fontold.Value.Item2["m_Script"]["m_FileID"].GetValue().AsInt64());
                        // Establece el PathID
                        font.Value.Item2["m_Script"]["m_PathID"].GetValue().Set(fontold.Value.Item2["m_Script"]["m_PathID"].GetValue().AsInt64());

                        /* Remplazar Material */
                        // Establece el FileID
                        font.Value.Item2["material"]["m_FileID"].GetValue().Set(fontold.Value.Item2["material"]["m_FileID"].GetValue().AsInt64());
                        // Establece el PathID
                        font.Value.Item2["material"]["m_PathID"].GetValue().Set(fontold.Value.Item2["material"]["m_PathID"].GetValue().AsInt64());

                        /* Remplazar Atlas */
                        // Establece el FileID
                        font.Value.Item2["atlas"]["m_FileID"].GetValue().Set(fontold.Value.Item2["atlas"]["m_FileID"].GetValue().AsInt64());
                        // Establece el PathID
                        font.Value.Item2["atlas"]["m_PathID"].GetValue().Set(fontold.Value.Item2["atlas"]["m_PathID"].GetValue().AsInt64());
                        
                        /* TODO: IMPORTAR ATLAS */

                        var newMonoBytes = font.Value.Item2.WriteToByteArray();

                        m.Add(new AssetsReplacerFromMemory(
                            0, fontold.Value.Item3.index, (int)fontold.Value.Item3.curFileType,
                            AssetHelper.GetScriptIndex(fontold.Value.Item4.file, fontold.Value.Item3), newMonoBytes
                        ));

                        break;
                    }
                }
            }
            return m;
        }

        public static void Save(UnityAssets OldFontAssets, List<AssetsReplacer> m)
        {
            //write changes to memory
            byte[] newAssetData;
            using (var stream = new MemoryStream())
            using (var writer = new AssetsFileWriter(stream))
            {
                OldFontAssets.Assets.file.Write(writer, 0, m, 0);
                newAssetData = stream.ToArray();
            }

            if (OldFontAssets.IsBundle)
            {
                //rename this asset name from boring to cool when saving
                var bunRepl = new BundleReplacerFromMemory(OldFontAssets.Assets.name, null, true, newAssetData, 0);

                var bunWriter = new AssetsFileWriter(File.OpenWrite("coolbundle.unity3d"));
                OldFontAssets.Bundle.file.Write(bunWriter, new List<BundleReplacer>() { bunRepl });
            } 
            else
            {
                File.WriteAllBytes(OldFontAssets.AssetName, newAssetData);
            }
        }
    }
}
