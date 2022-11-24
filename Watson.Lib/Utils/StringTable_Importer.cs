using AssetsTools.NET;
using AssetsTools.NET.Extra;
using Watson.Lib.Assets;

namespace Watson.Lib.Utils;

public static class StringTable_Importer
{
    public static StringTable.TableData[] Export(
        Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>> StringTables)
    {
        var list = new List<StringTable.TableData>();
        foreach (var stringTable in StringTables)
        {
            var count = stringTable.Value.Item2["m_TableData"]["Array"].Value.AsArray.size;
            for (var i = 0; i < count; i++)
            {
                var data = new StringTable.TableData();
                var localized = stringTable.Value.Item2["m_TableData"]["Array"][i]["m_Localized"].AsString;
                var id = stringTable.Value.Item2["m_TableData"]["Array"][i]["m_id"].AsLong;
                data.m_Localized = localized;
                data.m_id = id;
                list.Add(data);
            }
        }

        return list.ToArray();
    }
}