namespace Watson.Lib.Game;

public interface IGame
{
    public void Load();

    public void Proccess();

    public void Import(string poPath = "");

    public void Export(string outpath = "out");
}
