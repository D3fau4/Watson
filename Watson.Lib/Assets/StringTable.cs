using AssetsTools.NET;
using AssetsTools.NET.Extra;
using Watson.Lib.IO;

namespace Watson.Lib.Assets;

public class StringTable : IAsset
{
    private readonly Assembly m_DLL;
    public UnityAssetFile m_AssetFile;
    public Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>> m_StringTables;
    public List<TableData> m_tableData = new List<TableData>(); 

    public StringTable(UnityAssetFile StringTableBundle, Assembly assembly)
    {
        m_StringTables =
            new Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>>();
        m_AssetFile = StringTableBundle;
        m_DLL = assembly;
        Load();
    }

    public void Load()
    {
        foreach (var m_Asset in m_AssetFile.GetAssetsOfType(AssetClassID.MonoBehaviour))
        {
            var deserialized = m_AssetFile.AM.GetBaseField(m_AssetFile.Assets, m_Asset);
            //MonoDeserializer.GetMonoBaseField(m_AssetFile.AM, m_AssetFile.Assets, m_Asset, m_DLL.AssemblyFolder);
            var asset = deserialized.Get("m_TableData");
            if (asset != null)
                m_StringTables.Add(m_Asset.PathId,
                    Tuple.Create(deserialized.Get("m_Name").Value.AsString, deserialized, m_Asset,
                        m_AssetFile.Assets));
        }
        
        foreach (var stringTable in m_StringTables)
        {
            var count = stringTable.Value.Item2["m_TableData"]["Array"].Value.AsArray.size;
            for (var i = 0; i < count; i++)
            {
                var data = new StringTable.TableData();
                var localized = stringTable.Value.Item2["m_TableData"]["Array"][i]["m_Localized"].AsString;
                var id = stringTable.Value.Item2["m_TableData"]["Array"][i]["m_id"].AsLong;
                data.m_Localized = localized;
                data.m_id = id;
                
                // TODO: recoger Metadata
                
                m_tableData.Add(data);
            }
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