using Yarhl.FileFormat;

namespace Watson.Lib.Game.AI_TheSomniumFiles2.Assets;

public class I18Text : IFormat
{
    public I18Text(Dictionary<string, string> f, string ff = "")
    {
        File = f;
        filename = ff;
    }

    public I18Text()
    {
        File = new Dictionary<string, string>();
    }

    public Dictionary<string, string> File { get; set; }
    public string filename { get; set; }
}