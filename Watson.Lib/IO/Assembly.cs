using Cpp2IL.Core;
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

        public Assembly(string DataFolder)
        {
            if (Directory.Exists(Path.Combine(DataFolder, "Managed")))
                assemblyType = AssemblyType.Mono;
            else
                assemblyType = AssemblyType.IL2CPP;


            if (assemblyType == AssemblyType.IL2CPP)
            {
                Cpp2IlApi.InitializeLibCpp2Il("", "", new int[] { 1, 2, 3 }, false);
            }

            throw new NotImplementedException();
        }
    }
}
