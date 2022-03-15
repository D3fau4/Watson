using AssetsTools.NET;
using AssetsTools.NET.Extra;
using Watson.Lib.IO;

namespace Watson.Lib.Utils
{
    public static class Helpers
    {
        public static void Save(UnityAssets OldFontAssets, List<AssetsReplacer> m, AssetBundleCompressionType Compression = AssetBundleCompressionType.NONE)
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

                var bunWriter = new AssetsFileWriter(File.OpenWrite("TMP.unity3d"));
                OldFontAssets.Bundle.file.Write(bunWriter, new List<BundleReplacer>() { bunRepl });
                bunWriter.Close();

                if (Compression != AssetBundleCompressionType.NONE)
                {
                    var am = new AssetsManager();
                    var bun = am.LoadBundleFile("TMP.unity3d");
                    using (var stream = File.OpenWrite(OldFontAssets.AssetName))
                    using (var writer = new AssetsFileWriter(stream))
                    {
                        // hacer esto seleccionable
                        bun.file.Pack(bun.file.reader, writer, Compression);
                        am.UnloadAll(true);
                    }
                }
            }
            else
            {
                File.WriteAllBytes(OldFontAssets.AssetName, newAssetData);
            }
        }

        public static List<string> GetToImportList(Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> NewAssets,
            Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> OldAssets)
        {
            List<string> ToImport = new List<string>();
            // Buscar fuentes compatibles para importar
            foreach (var asset in NewAssets)
            {
                foreach (var oldasset in OldAssets)
                {
                    // Puede que cambie en otras versiones
                    if (asset.Value.Item1.Contains(oldasset.Value.Item1))
                    {
                        ToImport.Add(asset.Value.Item1);
                        break;
                    }
                }
            }

            return ToImport;
        }
    }
}
