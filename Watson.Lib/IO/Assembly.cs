﻿using Cpp2IL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watson.Lib.IO
{
    public class Assembly
    {
        public enum AssemblyType
        {
            Mono,
            IL2CPP
        }

        public AssemblyType assemblyType;
        public string AssemblyFolder;

        public Assembly(string DataFolder, Stream assetstream = null, string exepath = null, string metadatapath = null)
        {
            if (Directory.Exists(Path.Combine(DataFolder, "Managed")))
                assemblyType = AssemblyType.Mono;
            else
                assemblyType = AssemblyType.IL2CPP;


            if (assemblyType == AssemblyType.IL2CPP)
            {
                Cpp2IlApi.InitializeLibCpp2Il(exepath, metadatapath, Cpp2IlApi.GetVersionFromDataUnity3D(assetstream), false);
                var Dlls = Cpp2IlApi.MakeDummyDLLs();
                
                foreach (var Dll in Dlls)
                {
                    string tmp = $"{Path.GetTempPath()}{Path.PathSeparator}Assembly{Path.PathSeparator}";

                    if (Directory.Exists(tmp))
                        Directory.Delete(tmp, true);

                    Directory.CreateDirectory(tmp);

                    Dll.Write(Path.Combine(tmp, Dll.FullName));

                    AssemblyFolder = tmp;
                }
            } 
            else
            {
                AssemblyFolder = Path.Combine(DataFolder, "Managed");
            }
        }
    }
}
