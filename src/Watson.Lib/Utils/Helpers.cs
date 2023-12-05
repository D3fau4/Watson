using AssetsTools.NET;
using AssetsTools.NET.Extra;
using AssetsTools.NET.Texture;
using Watson.Lib.IO;

namespace Watson.Lib.Utils.Helpers;

public static class AssetHelper
{
    public static void Close(UnityAssetFile assetFile)
    {
        assetFile.Close();
    }

    public static void Save(UnityAssetFile assetFile,
        AssetBundleCompressionType Compression = AssetBundleCompressionType.None)
    {


        if (assetFile.IsBundle)
        {
            assetFile.Bundle.file.BlockAndDirInfo.DirectoryInfos[0].SetNewData(assetFile.Assets.file);
            using (AssetsFileWriter writer = new AssetsFileWriter("TMP.unity3d"))
            {
                assetFile.Bundle.file.Write(writer);
            }
            assetFile.AM.UnloadAll(true);
            File.Move("TMP.unity3d", assetFile.AssetName, true);
        }
        else
        {
            using (AssetsFileWriter writer = new AssetsFileWriter(assetFile.AssetName))
            {
                assetFile.Assets.file.Write(writer);
            }
        }
    }

    public static List<string> GetToImportList(
        Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>> NewAssets,
        Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>> OldAssets)
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
    public static byte[]? GetRawTextureBytes(TextureFile texFile, AssetsFileInstance inst)
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
                stream.Position = (long)texFile.m_StreamData.offset;
                texFile.pictureData = new byte[texFile.m_StreamData.size];
                stream.Read(texFile.pictureData, 0, (int)texFile.m_StreamData.size);
            }
            else
            {
                return null;
            }
        }

        return texFile.pictureData;
    }
}