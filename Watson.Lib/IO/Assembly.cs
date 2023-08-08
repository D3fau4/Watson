
namespace Watson.Lib.IO;

public class Assembly
{
    public enum AssemblyType
    {
        Mono,
        IL2CPP
    }

    public static AssemblyType CheckGameBackEnd(string DataFolder)
    {
        return Directory.Exists(Path.Combine(DataFolder, "Managed")) ? AssemblyType.Mono : AssemblyType.IL2CPP;
    }
}