using System;
using System.Collections.Generic;
using System.Linq;
using Watson.Lib.IO;


namespace Watson.Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            UnityAssets file = new UnityAssets(args[0]);
        }
    }
}