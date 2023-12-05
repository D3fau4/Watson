using Watson.Lib.Assets;
using Watson.Lib.IO;
using Yarhl.FileSystem;
using Yarhl.Media.Text;

namespace Watson.Lib.Utils;

public static class StringTable_Importer
{
}

public static class StringTable_Exporter
{
    public static void Export(StringTable source, string outpath = "out")
    {
        if (!Directory.Exists(outpath))
            Directory.CreateDirectory(outpath);

        foreach (var entrys in source.m_tableData)
        {
            var po = new StringTable2Po();
            var poobj = po.Convert(entrys.Value);
            var po2Binary = new Po2Binary();
            var binary = po2Binary.Convert(poobj);
            var node1 = new Node(entrys.Key["m_Name"].AsString, binary);
            node1.Stream?.WriteTo(Path.Combine(outpath, $"{entrys.Key["m_Name"].AsString}.po"));
        }
    }
}