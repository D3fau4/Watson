using MessagePack;

namespace Watson.Lib.Game.AI_TheSomniumFiles2.Assets;

public class I18n
{
    public readonly I18Text cache = new();
    public string filename = "";
    public MessagePackSerializerOptions options;

    public I18n(Stream file, string filename = "")
    {
        this.filename = filename;
        options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);

        cache = new I18Text(MessagePackSerializer.Deserialize<Dictionary<string, string>>(file, options));
    }
}