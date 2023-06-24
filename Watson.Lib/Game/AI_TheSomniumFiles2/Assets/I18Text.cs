using Yarhl.FileFormat;

namespace Watson.Lib.Game.AI_TheSomniumFiles2.Assets;

public class I18Text : IFormat
{
    public Dictionary<string, string> File;

    public I18Text(Dictionary<string, string> f)
    {
        File = f;
    }

    public I18Text()
    {
        File = new Dictionary<string, string>();
    }
}