namespace Watson.Lib.Game.PARANORMASIGHT;

using Assets;
using AssetsTools.NET.Cpp2IL;
using IO;
using Spectre.Console;
using Utils;
using Utils.Helpers;

public class Game : IGame
{
    public static readonly string gamename = "PARANORMASIGHT";
    private StatusContext ctx { get; set; }
    private string gamedatapath { get; set; }
    TMPFont oldfont { get; set; }
    public Game(StatusContext ctx, string gamedatapath)
    {
        this.ctx = ctx;
        this.gamedatapath = gamedatapath;
    }

    public void Load()
    {
        this.ctx.Status("Buscando fuentes...");
        var fontbundle = Directory.GetFiles(Path.Combine(gamedatapath, $"{gamename}_Data"),
            "sharedassets0.assets", SearchOption.AllDirectories);

        foreach (string font in fontbundle) {
            if (font.Contains(".json") || font.Contains(".xml") || font.Contains(".info") || font.Contains(".config") || font.Contains(".dat") || font.Contains(".dll"))
                continue;
            try {
                var b = new UnityAssetFile(font, Path.Combine(gamedatapath, $"{gamename}_Data"));

                oldfont= new TMPFont(b);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                AnsiConsole.MarkupLine($"[red]{font}[/]");
            }
        }
    }

    public void Proccess()
    {
        throw new NotImplementedException();
    }

    public void Import()
    {
        throw new NotImplementedException();
    }

    public void ImportF(string font)
    {
        TMPFont newFonts = null;
        try {
            var b = new UnityAssetFile(font, Path.Combine(gamedatapath, $"{gamename}_Data"));

            newFonts = new TMPFont(b);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            AnsiConsole.MarkupLine($"[red]{font}[/]");
        }

        List<string> a = TMPFont_Importer.GetToImportList(newFonts, oldfont, " Atlas", " Atlas");
        for (int i = 0; i < a.Count; i++) {
                AnsiConsole.MarkupLine($"[green]{a[i]}[/]");
            oldfont = TMPFont_Importer.Import(newFonts, oldfont);
        }
    }

    public void Export(string outpath = "out")
    {
        AssetHelper.Save(oldfont.m_AssetFile);
    }
}
