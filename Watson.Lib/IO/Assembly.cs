
using AssetsTools.NET.Cpp2IL;
using AssetsTools.NET.Extra;

namespace Watson.Lib.IO;

public static class Assembly
{
    public enum AssemblyType
    {
        Mono,
        Il2Cpp
    }

    public static AssemblyType CheckGameBackEnd(string DataFolder)
    {
        return Directory.Exists(Path.Combine(DataFolder, "Managed")) ? AssemblyType.Mono : AssemblyType.Il2Cpp;
    }
    
    public static IMonoBehaviourTemplateGenerator? LoadAssamblys(string DataFolder = "")
    {
        var type = Assembly.CheckGameBackEnd(DataFolder);

        if (type == Assembly.AssemblyType.Mono)
        {
            return new MonoCecilTempGenerator(Path.Combine(DataFolder, "Managed"));
        }
        else
        {
            var il2CppFiles = FindCpp2IlFiles.Find(DataFolder);
            if (il2CppFiles.success)
                return new Cpp2IlTempGenerator(il2CppFiles.metaPath, il2CppFiles.asmPath);
        }

        return null;
    }
}