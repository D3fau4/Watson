using AssetsTools.NET;
using AssetsTools.NET.Extra;
using Watson.Lib.Assets;

namespace Watson.Lib.Utils;

public static class StringTable_Importer
{
    public static StringTable.TableData[] Export(
        Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> StringTables)
    {
        foreach (var stringTable in StringTables)
        {
            var d = stringTable.Value.Item2.Get("m_TableData");
            var m = stringTable.Value.Item2["m_TableData"]["Array"].GetValue().AsArray();
            Console.WriteLine("");
        }

        return null;
    }
}