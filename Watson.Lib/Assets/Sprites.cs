using AssetsTools.NET;
using AssetsTools.NET.Extra;
using Watson.Lib.IO;

namespace Watson.Lib.Assets;

public class Sprites : IAsset
{
    public UnityAssetFile m_AssetFile;
    public Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> m_Sprites;
    public Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> m_Texture2D;

    public Sprites(UnityAssetFile SpriteBundle)
    {
        m_Sprites = new Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>>();
        m_Texture2D = new Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>>();

        m_AssetFile = SpriteBundle;
    }

    public void Load()
    {
        // Buscar Sprites
        foreach (var m_Sprite in m_AssetFile.GetAssetsOfType(AssetClassID.Sprite))
        {
            var baseField = m_AssetFile.AM.GetTypeInstance(m_AssetFile.Assets, m_Sprite).GetBaseField();
            m_Sprites.Add(m_Sprite.index,
                Tuple.Create(baseField["m_Name"].value.AsString(), baseField, m_Sprite, m_AssetFile.Assets));
        }

        // Buscar Texture2D
        foreach (var m_Texture in m_AssetFile.GetAssetsOfType(AssetClassID.Texture2D))
        {
            var baseField = m_AssetFile.AM.GetTypeInstance(m_AssetFile.Assets, m_Texture).GetBaseField();
            m_Texture2D.Add(m_Texture.index,
                Tuple.Create(baseField["m_Name"].value.AsString(), baseField, m_Texture, m_AssetFile.Assets));
        }
    }

    public void Close()
    {
        m_Sprites.Clear();
        m_Texture2D.Clear();
        m_AssetFile.Close();
    }
}