namespace Watson.Lib.Game.LaterAlligator;

using AssetsTools.NET;
using AssetsTools.NET.Extra;
using IO;
using Spectre.Console;
using Utils;
using Yarhl.FileSystem;
using Yarhl.IO;
using Yarhl.Media.Text;

public class Game : IGame
{
    public string gamepath { get; set; }
    private string gamedatapath { get; set; }
    private string assemblyFolder { get; set; }
    private string ExtractedAssetsFolder { get; set; }
    private StatusContext ctx { get; set; }

    private Dictionary<string, string[]> Say = new Dictionary<string, string[]>();
    private Dictionary<string, string[]> InvokeNextLineAsync = new Dictionary<string, string[]>();

    public Game(string gamepath, StatusContext ctx)
    {
        this.gamepath = gamepath;
        this.ctx = ctx;
        Load();
    }

    public void Load()
    {
        gamedatapath = Path.Combine(gamepath, "LaterAlligator_Data");
        assemblyFolder = Path.Combine(gamedatapath, "Managed");
        ExtractedAssetsFolder = TempDirectory.CreateTempDirectory();
        ctx.Status("Extrayendo Archivos...");
    }

    public void Proccess()
    {
        ctx.Status("Leyendo Archivos...");
        foreach (string filePath in Directory.GetFiles(gamedatapath, "*.bundle", SearchOption.AllDirectories)) {
            /*if (!filePath.Contains("scenes_scenes_locationgirlsgirlsgirls.bundle"))
                continue;*/

            AnsiConsole.MarkupLine("[yellow]Leyendo Archivo:[/] " + Path.GetFileName(filePath));
            var m_assetfile = new UnityAssetFile(filePath, gamedatapath);
            List<string> storyTexts = new List<string>();
            List<string> nextLineAsync = new List<string>();
            foreach (AssetFileInfo m_monobehaviour in m_assetfile.GetAssetsOfType(AssetClassID.MonoBehaviour)) {
                var deserialized =
                    m_assetfile.AM.GetBaseField(m_assetfile.Assets, m_monobehaviour);
                try
                {
                    if (!deserialized["targetMethod"].IsDummy &&
                        deserialized["targetMethod"].Value.AsString == "NextLineAsync") {
                        foreach (AssetTypeValueField objValue in deserialized["methodParameters.Array"]) {
                            if (objValue["objValue"].IsDummy || objValue["objValue"]["stringValue"].IsDummy)
                                continue;
                            nextLineAsync.Add(objValue["objValue"]["stringValue"].Value.AsString);
                        }
                    }

                    if (!deserialized["storyText"].IsDummy)
                        storyTexts.Add(deserialized.Get("storyText").Value.AsString);
                } catch (Exception e) {
#if DEBUG
                    AnsiConsole.WriteException(e);
#endif
                }
            }

            if (storyTexts.Count > 0)
                Say.Add(Path.GetFileName(filePath), storyTexts.ToArray());
            if (nextLineAsync.Count > 0)
                InvokeNextLineAsync.Add(Path.GetFileName(filePath), nextLineAsync.ToArray());

            m_assetfile.Close();
        }
    }

    public void Import()
    {
        throw new NotImplementedException();
    }

    public void Export(string outputPath = "out")
    {
        ctx.Status("Exportando Archivos...");
        Directory.CreateDirectory(outputPath);
        foreach (var (fileName, texts) in Say)
        {
            AnsiConsole.MarkupLine($"[yellow]Exportando Archivo:[/] {fileName}");
            var po = new ArrayString2Po(fileName, "Say").Convert(texts);
            new Po2Binary().Convert(po).Stream?.WriteTo(Path.Combine(outputPath, $"{fileName}.Say.po"));
        }

        foreach (var (fileName, texts) in InvokeNextLineAsync) {
            AnsiConsole.MarkupLine($"[yellow]Exportando Archivo:[/] {fileName}");
            var po = new ArrayString2Po(fileName, "InvokeNextLineAsync").Convert(texts);
            new Po2Binary().Convert(po).Stream?.WriteTo(Path.Combine(outputPath, $"{fileName}.InvokeNextLineAsync.po"));
        }
    }
}
