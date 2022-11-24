using Watson.Lib.Assets;
using Watson.Lib.IO;
using Watson.Lib.Utils;

namespace Watson.Program;

public class Program
{
    public static void Main(string[] args)
    {
        var m_stringTables = new StringTable(new UnityAssetFile(args[0]), new Assembly(args[1]));

        StringTable_Exporter.Export(m_stringTables);
    }
}