using AssetsTools.NET;
using AssetsTools.NET.Extra;
using Watson.Lib.IO;

namespace Watson.Lib.Assets;

public class TextAsset : IAsset
{
    public UnityAssetFile m_AssetFile;
    public Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>> m_TextsAssets;

    public TextAsset(UnityAssetFile mAssetFile)
    {
        m_AssetFile = mAssetFile;
        m_TextsAssets = new Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>>();
    }

    public void Load()
    {
        foreach (var m_TextAsset in m_AssetFile.GetAssetsOfType(AssetClassID.TextAsset))
        {
            var baseField = m_AssetFile.AM.GetBaseField(m_AssetFile.Assets, m_TextAsset);
            m_TextsAssets.Add(m_TextAsset.PathId,
                Tuple.Create(baseField["m_Name"].Value.AsString, baseField, m_TextAsset, m_AssetFile.Assets));
        }
    }

    public void Close()
    {
        throw new NotImplementedException();
    }
}