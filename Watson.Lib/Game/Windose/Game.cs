using Watson.Lib.IO;

namespace Watson.Lib.Game.Windose;

public class Game : IGame
{
    private Assembly m_Assemblys;
    private UnityAssetFile m_assetfile;

    public Game(string path)
    {
        gamepath = path;
        Load();
    }

    public string gamepath { get; set; }
    private string gamedatapath { get; set; }
    private string assemblyFolder { get; set; }
    private string m_assetfilepath { get; set; } = "resources.assets";

    public void Load()
    {
        gamedatapath = Path.Combine(gamepath, "Windose_Data");
        assemblyFolder = Path.Combine(gamedatapath, "Managed");
        m_assetfilepath = Path.Combine(gamedatapath, "resources.assets");
        m_Assemblys = new Assembly(assemblyFolder);
        m_assetfile = new UnityAssetFile(m_assetfilepath);

        throw new NotImplementedException();
    }

    public void Proccess()
    {
        throw new NotImplementedException();
    }

    public void Import()
    {
        throw new NotImplementedException();
    }

    public void Export()
    {
        throw new NotImplementedException();
    }
}