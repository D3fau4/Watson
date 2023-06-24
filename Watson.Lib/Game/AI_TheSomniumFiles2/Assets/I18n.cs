using MessagePack;
using Yarhl.Media.Text;

namespace Watson.Lib.Game.AI_TheSomniumFiles2.Assets;

public class I18n
{
    private readonly Dictionary<string, string> cache = new();
    public MessagePackSerializerOptions options;

    public I18n(Stream file)
    {
        options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);

        cache = MessagePackSerializer.Deserialize<Dictionary<string, string>>(file, options);
    }

    public I18n(I18Text file)
    {
        cache = file.File;
    }
}