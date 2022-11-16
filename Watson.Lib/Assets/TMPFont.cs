using AssetsTools.NET;
using AssetsTools.NET.Extra;
using Watson.Lib.IO;

namespace Watson.Lib.Assets;

public class TMPFont : IAsset
{
    public UnityAssetFile m_AssetFile;
    private readonly Assembly m_DLL;
    public Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> m_FontNames;
    public Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> m_FontTextures;

    public TMPFont(UnityAssetFile FontBundle, Assembly assembly)
    {
        m_FontNames = new Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>>();
        m_FontTextures =
            new Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>>();

        m_AssetFile = FontBundle;
        m_DLL = assembly;
        Load();
    }

    public void Load()
    {
        // Listar todas las fuentes
        foreach (var m_Asset in m_AssetFile.GetAssetsOfType(AssetClassID.MonoBehaviour))
        {
            var deserialized =
                MonoDeserializer.GetMonoBaseField(m_AssetFile.AM, m_AssetFile.Assets, m_Asset, m_DLL.AssemblyFolder);
            var asset = deserialized.Get("m_fontInfo");
            if (asset != null)
                // Almacenar el nombre de asset que contiene la fuente.
                m_FontNames.Add(m_Asset.index,
                    Tuple.Create(deserialized.Get("m_Name").GetValue().AsString(), deserialized, m_Asset,
                        m_AssetFile.Assets));
        }

        // Buscar Texture2D
        foreach (var m_Texture in m_AssetFile.GetAssetsOfType(AssetClassID.Texture2D))
        {
            var baseField = m_AssetFile.AM.GetTypeInstance(m_AssetFile.Assets, m_Texture).GetBaseField();
            m_FontTextures.Add(m_Texture.index,
                Tuple.Create(baseField["m_Name"].value.AsString(), baseField, m_Texture, m_AssetFile.Assets));
        }
    }

    public void Close()
    {
        m_FontNames.Clear();
        m_FontTextures.Clear();
        m_AssetFile.Close();
    }
}