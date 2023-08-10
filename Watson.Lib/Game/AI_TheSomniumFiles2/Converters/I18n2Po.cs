using Watson.Lib.Game.AI_TheSomniumFiles2.Assets;
using Yarhl.FileFormat;
using Yarhl.Media.Text;

namespace Watson.Lib.Game.AI_TheSomniumFiles2.Converters;

public class I18n2Po : IConverter<I18Text, Po>
{
    public Po Convert(I18Text source)
    {
        var currentCulture = Thread.CurrentThread.CurrentCulture;
        var po = new Po
        {
            Header = new PoHeader("AI: The Somnium Files - Nirvana Initiative", "d3fau4@not-d3fau4.com",
                currentCulture.Name)
            {
                LanguageTeam = "Naix"
            }
        };

        foreach (var entry in source.File)
        {
            if (entry.Value == null)
                continue;
            var ori = entry.Value;
            if (ori.Equals(""))
                ori = "{EMPTY]";

            po.Add(new PoEntry
            {
                Original = ori,
                Context = $"{entry.Key}"
            });
        }

        return po;
    }
}