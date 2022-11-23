using AssetsTools.NET;
using AssetsTools.NET.Extra;
using Watson.Lib.IO;

namespace Watson.Lib.Assets;

public class StringTable : IAsset
{
    private readonly Assembly m_DLL;
    public UnityAssetFile m_AssetFile;
    public Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> m_StringTables;

    public StringTable(UnityAssetFile StringTableBundle, Assembly assembly)
    {
        m_StringTables =
            new Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>>();
        m_AssetFile = StringTableBundle;
        m_DLL = assembly;
        Load();
    }

    public void Load()
    {
        foreach (var m_Asset in m_AssetFile.GetAssetsOfType(AssetClassID.MonoBehaviour))
        {
            var deserialized =
                MonoDeserializer.GetMonoBaseField(m_AssetFile.AM, m_AssetFile.Assets, m_Asset, m_DLL.AssemblyFolder);
            var asset = deserialized.Get("m_TableData");
            if (asset != null)
                m_StringTables.Add(m_Asset.index,
                    Tuple.Create(deserialized.Get("m_Name").GetValue().AsString(), deserialized, m_Asset,
                        m_AssetFile.Assets));
        }
    }

    public void Close()
    {
        m_AssetFile.Close();
        m_StringTables.Clear();
    }

    public struct TableData
    {
        public long m_id;
        public string m_Localized;
        public Metadata m_metada;
    }

    public struct Metadata
    {
        public string[] m_Items;
    }
}