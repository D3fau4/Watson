using AssetsTools.NET;
using AssetsTools.NET.Extra;
using Watson.Lib.IO;

namespace Watson.Lib.Assets;

public class Texture2D : IAsset
{
    private readonly Assembly m_DLL;
    public UnityAssetFile m_AssetFile;
    public Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>> m_Textures2D;

    public Texture2D(UnityAssetFile FontBundle, Assembly assembly)
    {
        m_Textures2D = new Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>>();

        m_AssetFile = FontBundle;
        m_DLL = assembly;
    }

    public void Load()
    {
        // Buscar Texture2D
        foreach (var m_Texture in m_AssetFile.GetAssetsOfType(AssetClassID.Texture2D))
        {
            var baseField = m_AssetFile.AM.GetBaseField(m_AssetFile.Assets, m_Texture);
            m_Textures2D.Add(m_Texture.PathId,
                Tuple.Create(baseField["m_Name"].Value.AsString, baseField, m_Texture, m_AssetFile.Assets));
        }
    }

    public void Close()
    {
        m_Textures2D.Clear();
        m_AssetFile.Close();
    }
}