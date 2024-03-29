﻿using AssetsTools.NET;
using AssetsTools.NET.Extra;
using Watson.Lib.IO;

namespace Watson.Lib.Assets;

public class StringTable : IAsset
{
    public UnityAssetFile m_AssetFile;
    public Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>> m_StringTables;
    public Dictionary<AssetTypeValueField, TableData[]> m_tableData = new();

    public StringTable(UnityAssetFile StringTableBundle)
    {
        m_StringTables =
            new Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>>();
        m_AssetFile = StringTableBundle;
        Load();
    }

    public void Load()
    {
        foreach (var m_Asset in m_AssetFile.GetAssetsOfType(AssetClassID.MonoBehaviour))
        {
            var deserialized = m_AssetFile.AM.GetBaseField(m_AssetFile.Assets, m_Asset);

            var asset = deserialized.Get("m_TableData");
            if (asset.Value != null)
                m_StringTables.Add(m_Asset.PathId,
                    Tuple.Create(deserialized.Get("m_Name").Value.AsString, deserialized, m_Asset,
                        m_AssetFile.Assets));
        }

        foreach (var stringTable in m_StringTables)
        {
            var count = stringTable.Value.Item2["m_TableData"]["Array"].Value.AsArray.size;
            var list = new List<TableData>();
            for (var i = 0; i < count; i++)
            {
                var data = new TableData();
                var localized = stringTable.Value.Item2["m_TableData"]["Array"][i]["m_Localized"].AsString;
                var id = stringTable.Value.Item2["m_TableData"]["Array"][i]["m_Id"].Value.AsLong;
                data.m_Localized = localized;
                data.m_id = id;

                // TODO: recoger Metadata

                list.Add(data);
            }

            m_tableData.Add(stringTable.Value.Item2, list.ToArray());
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