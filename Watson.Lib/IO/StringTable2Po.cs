using Watson.Lib.Assets;
using Yarhl.FileFormat;
using Yarhl.Media.Text;

namespace Watson.Lib.IO;

public class StringTable2Po : IConverter<StringTable.TableData[], Po>
{
    public Po Convert(StringTable.TableData[] source)
    {
        var currentCulture = Thread.CurrentThread.CurrentCulture;
        var po = new Po
        {
            Header = new PoHeader("Watson", "d3fau4@not-d3fau4.com", currentCulture.Name)
            {
                LanguageTeam = "Any"
            }
        };

        foreach (var entry in source)
            po.Add(new PoEntry
            {
                Original = entry.m_Localized,
                Context = $"{entry.m_id}"
            });

        return po;
    }
}