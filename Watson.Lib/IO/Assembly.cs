using Cpp2IL.Core;

namespace Watson.Lib.IO;


public class Assembly
{
    public enum AssemblyType
    {
        Mono,
        IL2CPP
    }

    public string AssemblyFolder;

    public AssemblyType assemblyType;
    
    [Obsolete("Ahora se cargan desde UnityAssetFile")]
    public Assembly(string DataFolder)
    {
        assemblyType = CheckGameBackEnd(DataFolder);
        if (assemblyType == AssemblyType.IL2CPP)
        {
            var exepath = Path.Combine(Directory.GetParent(DataFolder).FullName, "GameAssembly.dll");
            if (!File.Exists(exepath))
                exepath = Path.Combine(Directory.GetParent(DataFolder).FullName, "GameAssembly.so");
            var metadatapath = Path.Combine(DataFolder, "il2cpp_data", "Metadata", "global-metadata.dat");
            if (!File.Exists(metadatapath))
                throw new Exception("Version not supported");
            Cpp2IlApi.InitializeLibCpp2Il(exepath, metadatapath, Cpp2IlApi.DetermineUnityVersion(null, DataFolder),
                false);
            var Dlls = Cpp2IlApi.MakeDummyDLLs();

            var tmp = Path.Combine(Path.GetTempPath(), "Assembly");
            if (Directory.Exists(tmp))
                Directory.Delete(tmp, true);
            Directory.CreateDirectory(tmp);

            foreach (var Dll in Dlls)
            {
                var m = Path.Combine(tmp, Dll.MainModule.Name);
                Dll.Write(m);

                AssemblyFolder = tmp;
            }
        }
        else
        {
            AssemblyFolder = Path.Combine(DataFolder, "Managed");
        }
    }

    public static AssemblyType CheckGameBackEnd(string DataFolder)
    {
        return Directory.Exists(Path.Combine(DataFolder, "Managed")) ? AssemblyType.Mono : AssemblyType.IL2CPP;
    }
}