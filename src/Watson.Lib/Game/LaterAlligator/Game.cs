namespace Watson.Lib.Game.LaterAlligator;

using IO;
using Utils;

public class Game : IGame
{
    public string gamepath { get; set; }
    private string gamedatapath { get; set; }
    private string assemblyFolder { get; set; }
    private string m_assetfilepath { get; set; } = "data.unity3d";
    private string ExtractedAssetsFolder { get; set; }
    public void Load()
    {
        gamedatapath = Path.Combine(gamepath, "Windose_Data");
        assemblyFolder = Path.Combine(gamedatapath, "Managed");
        m_assetfilepath = Path.Combine(gamedatapath, "data.unity3d");
        ExtractedAssetsFolder = TempDirectory.CreateTempDirectory();
        UnityAssetFile.LoadAndExtractUnity3D(m_assetfilepath, ExtractedAssetsFolder);
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
