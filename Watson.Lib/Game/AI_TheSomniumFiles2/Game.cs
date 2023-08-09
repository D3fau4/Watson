using Watson.Lib.Game.AI_TheSomniumFiles2.Assets;
using Watson.Lib.Game.AI_TheSomniumFiles2.Enums;
using AssetsTools.NET.Texture;
using Spectre.Console;
using Watson.Lib.Assets;
using Watson.Lib.IO;

namespace Watson.Lib.Game.AI_TheSomniumFiles2;

public class Game : IGame
{
    public static readonly string gamename = "AI_TheSomniumFiles2";
    private string m_gamepath { get; set; }
    private LanguageType m_languageType { get; set; }
    private List<I18n> m_Texts = new List<I18n>();
    private TMPFont m_fonts;
    private Sprites[] m_Sprites;

    public Game(string gamePath, LanguageType lan)
    {
        m_gamepath = gamePath;
        m_languageType = lan;
    }

    public void Load()
    {
        foreach (var f in Directory.EnumerateFiles(
                     Path.Combine(m_gamepath, $"{gamename}_Data", "StreamingAssets", "Text", m_languageType.ToString()), "*",
                     SearchOption.AllDirectories))
        {
            AnsiConsole.WriteLine($"Leyendo: {f}");
            try
            {
                var text = new I18n(new FileStream(File.OpenHandle(f), FileAccess.Read));
                m_Texts.Add(text);
            }
            catch (Exception e)
            {
                AnsiConsole.MarkupLine($"[red]Fallo al leer: {f}[/]");
            }
        }


        var fontbundle = Directory.GetFiles(Path.Combine(m_gamepath, $"{gamename}_Data", "StreamingAssets", "aa"), "fonts_assets_all*", SearchOption.AllDirectories)[0];
        
        m_fonts = new TMPFont(new UnityAssetFile(fontbundle,  Path.Combine(m_gamepath, $"{gamename}_Data")));
        
        var Spritesbundle = Directory.GetFiles(Path.Combine(m_gamepath, $"{gamename}_Data", "StreamingAssets", "aa"), "*image*", SearchOption.AllDirectories);
        List<Sprites> list = new List<Sprites>();
        foreach (var spriteb in Spritesbundle)
        {
            list.Add(new Sprites(new UnityAssetFile(spriteb, Path.Combine(m_gamepath, $"{gamename}_Data"))));
        }
        m_Sprites = list.ToArray();
    }

    public void Proccess()
    {
        foreach (var sprite in m_Sprites)
        {
            sprite.Load();
        }
    }

    public void Import()
    {
        throw new NotImplementedException();
    }

    public void Export(string outpath = "out")
    {
        if (!Directory.Exists(outpath))
            Directory.CreateDirectory(outpath);
        
        foreach (var sprite in m_Sprites)
        {
            foreach (var t in sprite.m_Texture2D)
            {
                var texture = TextureFile.ReadTextureFile(t.Value.Item2); // load base field into helper class
                var textureBgraRaw = texture.GetTextureData(t.Value.Item4); // get the raw bgra32 data
                var textureImage = Image.LoadPixelData<Bgra32>(textureBgraRaw, texture.m_Width, texture.m_Height); // use imagesharp to convert to image
                textureImage.Mutate(i => i.Flip(FlipMode.Vertical)); // flip on x-axis (all textures in unity are stored flipped like this)
                textureImage.SaveAsPng(Path.Combine(outpath, $"{t.Value.Item1}-{t.Key}.png"));
            }
        }
        throw new NotImplementedException();
    }

    public string[] listFonts(string filter = "")
    {
        List<string> fonts = new List<string>();
        foreach (var keys in m_fonts.m_FontNames)
        {
            AnsiConsole.MarkupLine($"[green]Font: {keys.Value.Item1} - {keys.Value.Item2.Get("m_FaceInfo").Get("m_FamilyName").AsString}[/]");
            
            if(filter.Equals(string.Empty))
                fonts.Add(keys.Value.Item2.Get("m_FaceInfo").Get("m_FamilyName").AsString);
            else
            {
                if (keys.Value.Item1.Contains($"#{filter}-font"))
                {
                    fonts.Add(keys.Value.Item2.Get("m_FaceInfo").Get("m_FamilyName").AsString);
                }
            }
        }

        return fonts.ToArray();
    }
}