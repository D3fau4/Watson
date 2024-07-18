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
    private string m_assetfilepath { get; set; } = "data.unity3d";
    private string ExtractedAssetsFolder { get; set; }
    private StatusContext ctx { get; set; }

    private Dictionary<string, string[]> Say = new Dictionary<string, string[]>();

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
        m_assetfilepath = Path.Combine(gamedatapath, "data.unity3d");
        ExtractedAssetsFolder = TempDirectory.CreateTempDirectory();
        ctx.Status("Extrayendo Archivos...");
        UnityAssetFile.LoadAndExtractUnity3D(m_assetfilepath, ExtractedAssetsFolder);

    }

    public void Proccess()
    {
        ctx.Status("Leyendo Archivos...");
        foreach (string filePath in Directory.GetFiles(ExtractedAssetsFolder)) {
            AnsiConsole.MarkupLine("[yellow]Leyendo Archivo:[/] " + Path.GetFileName(filePath));
            var m_assetfile = new UnityAssetFile(filePath, gamedatapath);
            List<string> txt = new List<string>();
            foreach (AssetFileInfo m_monobehaviour in m_assetfile.GetAssetsOfType(AssetClassID.MonoBehaviour)) {
                var deserialized =
                    m_assetfile.AM.GetBaseField(m_assetfile.Assets, m_monobehaviour);
                var m_Script = m_assetfile.AM.GetExtAsset(m_assetfile.Assets, deserialized["m_Script"]).baseField;
                if (m_Script.Get("m_ClassName").Value.AsString.Equals("Say")) {
                    txt.Add(deserialized.Get("storyText").Value.AsString);
                }
            }
            if (txt.Count > 0)
                Say.Add(Path.GetFileName(filePath), txt.ToArray());
            m_assetfile.Close();
        }

        Directory.Delete(ExtractedAssetsFolder, true);
    }

    public void Import()
    {
        throw new NotImplementedException();
    }

    public void Export(string outpath = "out")
    {
        ctx.Status("Exportando Archivos...");
        if (!Directory.Exists(outpath))
            Directory.CreateDirectory(outpath);
        foreach (KeyValuePair<string,string[]> keyValuePair in Say) {
            AnsiConsole.MarkupLine("[yellow]Exportando Archivo:[/] " + keyValuePair.Key);
            var arrayString2Po = new ArrayString2Po(keyValuePair.Key, "Say");
            var po = arrayString2Po.Convert(keyValuePair.Value);
            var po2Binary = new Po2Binary();
            po2Binary.Convert(po).Stream?.WriteTo(Path.Combine(outpath, $"{keyValuePair.Key}.po"));
        }
    }
}
