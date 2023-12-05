using AssetsTools.NET.Texture;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Spectre.Console;
using Watson.Lib.Assets;
using Watson.Lib.Game.AI_TheSomniumFiles2.Assets;
using Watson.Lib.Game.AI_TheSomniumFiles2.Converters;
using Watson.Lib.Game.AI_TheSomniumFiles2.Enums;
using Watson.Lib.IO;
using Yarhl.FileSystem;
using Yarhl.Media.Text;

namespace Watson.Lib.Game.AI_TheSomniumFiles2;

public class Game : IGame
{
    public static readonly string gamename = "AI_TheSomniumFiles2";
    private readonly StatusContext? ctx;
    private TMPFont m_fonts;
    private Sprites[] m_Sprites;
    private readonly List<I18n> m_Texts = new();

    public Game(string gamePath, LanguageType lan, StatusContext ctx = null)
    {
        m_gamepath = gamePath;
        m_languageType = lan;
        this.ctx = ctx;
    }

    private string m_gamepath { get; }
    private LanguageType m_languageType { get; }

    public void Load()
    {
        ctx.Status("Buscando texto...");
        foreach (var f in Directory.EnumerateFiles(
                     Path.Combine(m_gamepath, $"{gamename}_Data", "StreamingAssets", "Text", m_languageType.ToString()),
                     "*",
                     SearchOption.AllDirectories))
            //AnsiConsole.WriteLine($"Leyendo: {f}");
            try
            {
                var text = new I18n(new FileStream(File.OpenHandle(f), FileAccess.Read), Path.GetFileName(f));
                m_Texts.Add(text);
            }
            catch (Exception e)
            {
                //AnsiConsole.MarkupLine($"[red]Fallo al leer: {f}[/]");
            }

        ctx.Status("Buscando fuentes...");
        var fontbundle = Directory.GetFiles(Path.Combine(m_gamepath, $"{gamename}_Data", "StreamingAssets", "aa"),
            "fonts_assets_all*", SearchOption.AllDirectories)[0];

        m_fonts = new TMPFont(new UnityAssetFile(fontbundle, Path.Combine(m_gamepath, $"{gamename}_Data")));

        ctx.Status("Buscando sprites...");
        var Spritesbundle = Directory.GetFiles(Path.Combine(m_gamepath, $"{gamename}_Data", "StreamingAssets", "aa"),
            "*image*", SearchOption.AllDirectories);
        var list = new List<Sprites>();
        foreach (var spriteb in Spritesbundle)
            list.Add(new Sprites(new UnityAssetFile(spriteb, Path.Combine(m_gamepath, $"{gamename}_Data"))));
        m_Sprites = list.ToArray();
    }

    public void Proccess()
    {
        ctx.Status("Procesando sprites...");
        foreach (var sprite in m_Sprites) sprite.Load();
    }

    public void Import()
    {
        throw new NotImplementedException();
    }

    public void Export(string outpath = "out")
    {
        if (!Directory.Exists(outpath))
            Directory.CreateDirectory(outpath);

        ctx.Status("Exportando textos...");
        try
        {
            foreach (var entrys in m_Texts)
            {
                if(entrys.filename.Contains("Test_"))
                    continue;
                if (!Directory.Exists(Path.Combine(outpath, "Po")))
                    Directory.CreateDirectory(Path.Combine(outpath, "Po"));
                var po = new I18n2Po();
                var poobj = po.Convert(entrys.cache);
                var po2Binary = new Po2Binary();
                var binary = po2Binary.Convert(poobj);
                var node1 = new Node(entrys.filename, binary);
                node1.Stream?.WriteTo(Path.Combine(outpath, "Po", $"{entrys.filename}.po"));
            }
        }
        catch (Exception e)
        {
            AnsiConsole.MarkupLine($"[red]{e}[/]");
            return;
        }

        ctx.Status("Exportando sprites...");
        foreach (var sprite in m_Sprites)
        foreach (var t in sprite.m_Texture2D)
        {
            //AnsiConsole.MarkupLine($"Exportando: {t.Value.Item1}");
            if (!Directory.Exists(Path.Combine(outpath, "Sprites",
                    sprite.m_AssetFile.Bundle.name.Replace(".bundle", ""))))
                Directory.CreateDirectory(Path.Combine(outpath, "Sprites",
                    sprite.m_AssetFile.Bundle.name.Replace(".bundle", "")));

            var texture = TextureFile.ReadTextureFile(t.Value.Item2); // load base field into helper class
            var textureBgraRaw = texture.GetTextureData(t.Value.Item4); // get the raw bgra32 data
            var textureImage =
                Image.LoadPixelData<Bgra32>(textureBgraRaw, texture.m_Width,
                    texture.m_Height); // use imagesharp to convert to image
            textureImage.Mutate(i =>
                i.Flip(FlipMode.Vertical)); // flip on x-axis (all textures in unity are stored flipped like this)
            textureImage.SaveAsPng(Path.Combine(outpath, "Sprites",
                sprite.m_AssetFile.Bundle.name.Replace(".bundle", ""), $"{t.Value.Item1}-{t.Key}.png"));
            /*AnsiConsole.MarkupLine($"[green]¡Exportado!: {Path.Combine(outpath, "Sprites",
                    sprite.m_AssetFile.Bundle.name.Replace(".bundle", ""), $"{t.Value.Item1}-{t.Key}.png")}[/]");*/
        }
    }

    public string[] listFonts(string filter = "")
    {
        var fonts = new List<string>();
        foreach (var keys in m_fonts.m_FontNames)
        {
            AnsiConsole.MarkupLine(
                $"[green]Font: {keys.Value.Item1} - {keys.Value.Item2.Get("m_FaceInfo").Get("m_FamilyName").AsString}[/]");

            if (filter.Equals(string.Empty))
            {
                fonts.Add(keys.Value.Item2.Get("m_FaceInfo").Get("m_FamilyName").AsString);
            }
            else
            {
                if (keys.Value.Item1.Contains($"#{filter}-font"))
                    fonts.Add(keys.Value.Item2.Get("m_FaceInfo").Get("m_FamilyName").AsString);
            }
        }

        return fonts.ToArray();
    }
}