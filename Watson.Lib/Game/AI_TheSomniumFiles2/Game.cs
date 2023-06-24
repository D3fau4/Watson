using Watson.Lib.Game.AI_TheSomniumFiles2.Assets;
using Watson.Lib.Game.AI_TheSomniumFiles2.Enums;
using System.IO;
using Spectre.Console;

namespace Watson.Lib.Game.AI_TheSomniumFiles2;

public class Game : IGame
{
    public static readonly string gamename = "AI_TheSomniumFiles2";
    private string m_gamepath { get; set; }
    private LanguageType m_languageType { get; set; }
    private List<I18n> m_Texts = new List<I18n>();

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
    }

    public void Proccess()
    {
        throw new NotImplementedException();
    }

    public void Import()
    {
        throw new NotImplementedException();
    }

    public void Export(string outpath = "out")
    {
        throw new NotImplementedException();
    }
}