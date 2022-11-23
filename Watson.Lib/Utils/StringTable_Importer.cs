using AssetsTools.NET;
using AssetsTools.NET.Extra;
using Watson.Lib.Assets;

namespace Watson.Lib.Utils;

public static class StringTable_Importer
{
    public static StringTable.TableData[] Export(
        Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>> StringTables)
    {
        List<string> list = new List<string>();
        foreach (var stringTable in StringTables)
        {
            var count = stringTable.Value.Item2["m_TableData"]["Array"].Value.AsArray.size;
            for (int i = 0; i < count; i++)
            {
                var meme = stringTable.Value.Item2["m_TableData"]["Array"][i]["m_Localized"].AsString;
                list.Add(meme);
            }
        }
        File.WriteAllLines("Holoerror.txt", list.ToArray());
        return null;
    }
}