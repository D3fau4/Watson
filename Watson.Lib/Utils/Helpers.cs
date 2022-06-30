using AssetsTools.NET;
using AssetsTools.NET.Extra;
using Watson.Lib.IO;

namespace Watson.Lib.Utils.Helpers;

public static class AssetHelper
{
    public static void Close(UnityAssetFile assetFile)
    {
        assetFile.Close();
    }

    public static void Save(UnityAssetFile assetFile, List<AssetsReplacer> m,
        AssetBundleCompressionType Compression = AssetBundleCompressionType.NONE)
    {
        //write changes to memory
        byte[] newAssetData;
        using (var stream = new MemoryStream())
        using (var writer = new AssetsFileWriter(stream))
        {
            assetFile.Assets.file.Write(writer, 0, m);
            newAssetData = stream.ToArray();
        }

        if (assetFile.IsBundle)
        {
            //rename this asset name from boring to cool when saving
            var bunRepl = new BundleReplacerFromMemory(assetFile.Assets.name, null, true, newAssetData, 0);

            var bunWriter = new AssetsFileWriter(File.OpenWrite("TMP.unity3d"));
            assetFile.Bundle.file.Write(bunWriter, new List<BundleReplacer> {bunRepl});
            bunWriter.Close();

            if (Compression != AssetBundleCompressionType.NONE)
            {
                var am = new AssetsManager();
                var bun = am.LoadBundleFile("TMP.unity3d");
                using (var stream = File.OpenWrite(assetFile.AssetName))
                using (var writer = new AssetsFileWriter(stream))
                {
                    // hacer esto seleccionable
                    bun.file.Pack(bun.file.reader, writer, Compression);
                    am.UnloadAll(true);
                    File.Delete("TMP.unity3d");
                }
            }
        }
        else
        {
            File.WriteAllBytes(assetFile.AssetName, newAssetData);
        }
    }

    public static List<string> GetToImportList(
        Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> NewAssets,
        Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> OldAssets)
    {
        var ToImport = new List<string>();
        // Buscar fuentes compatibles para importar
        foreach (var asset in NewAssets)
        foreach (var oldasset in OldAssets)
            // Puede que cambie en otras versiones
            if (asset.Value.Item1.Contains(oldasset.Value.Item1))
            {
                ToImport.Add(asset.Value.Item1);
                break;
            }

        return ToImport;
    }
}

/// <summary>
///     Code from
///     https://github.com/nesrak1/UABEA/blob/72a9912ea3909a670bdb7754e85588760448c0bd/TexturePlugin/Program.cs#L16-L53
/// </summary>
public static class TextureHelper
{
    public static byte[] GetRawTextureBytes(TextureFile texFile, AssetsFileInstance inst)
    {
        var rootPath = Path.GetDirectoryName(inst.path);
        if (texFile.m_StreamData.size != 0 && texFile.m_StreamData.path != string.Empty)
        {
            var fixedStreamPath = texFile.m_StreamData.path;
            if (!Path.IsPathRooted(fixedStreamPath) && rootPath != null)
                fixedStreamPath = Path.Combine(rootPath, fixedStreamPath);
            if (File.Exists(fixedStreamPath))
            {
                Stream stream = File.OpenRead(fixedStreamPath);
                stream.Position = (long) texFile.m_StreamData.offset;
                texFile.pictureData = new byte[texFile.m_StreamData.size];
                stream.Read(texFile.pictureData, 0, (int) texFile.m_StreamData.size);
            }
            else
            {
                return null;
            }
        }

        return texFile.pictureData;
    }
}