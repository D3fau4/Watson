using AssetsTools.NET;
using AssetsTools.NET.Extra;
using Watson.Lib.IO;

namespace Watson.Lib.Assets;

public class TMPFont : IAsset
{
    public UnityAssetFile m_AssetFile;
    public Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>> m_FontNames;
    public Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>> m_FontTextures;

    public TMPFont(UnityAssetFile FontBundle)
    {
        m_FontNames = new Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>>();
        m_FontTextures =
            new Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>>();

        m_AssetFile = FontBundle;
        Load();
    }

    public void Load()
    {
        LoadFonts();
        LoadTextures();
    }

    private void LoadFonts()
    {
        foreach (var asset in m_AssetFile.GetAssetsOfType(AssetClassID.MonoBehaviour))
        {
            var baseField = m_AssetFile.AM.GetBaseField(m_AssetFile.Assets, asset);
            var fontInfo = baseField.Get("m_fontInfo");
            if (!fontInfo.IsDummy)
            {
                m_FontNames.Add(asset.PathId, Tuple.Create(baseField.Get("m_Name").Value.AsString, baseField, asset, m_AssetFile.Assets)!);
            }
        }
    }

    private void LoadTextures()
    {
        foreach (var texture in m_AssetFile.GetAssetsOfType(AssetClassID.Texture2D))
        {
            var baseField = m_AssetFile.AM.GetBaseField(m_AssetFile.Assets, texture);
            m_FontTextures.Add(texture.PathId, Tuple.Create(baseField["m_Name"].Value.AsString, baseField, texture, m_AssetFile.Assets)!);
        }
    }

    public void Close()
    {
        m_FontNames.Clear();
        m_FontTextures.Clear();
        m_AssetFile.Close();
    }
}
