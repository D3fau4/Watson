using Watson.Lib.Assets;
using Watson.Lib.IO;
using Watson.Lib.Utils;

namespace Watson.Program;

public class Program
{
    public static void Main(string[] args)
    {
        new Watson.Lib.Game.neptunia_sisters_vs_sisters.Game(args[0]).Proccess();
    }
}