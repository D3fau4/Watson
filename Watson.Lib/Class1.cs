using AssetsTools.NET.Extra;

namespace Watson.Lib
{
    public class Class1
    {
        public void meme()
        {
            var am = new AssetsManager();
            var inst = am.LoadAssetsFile("resources.assets", true);

            am.LoadClassPackage("classdata.tpk");
            am.LoadClassDatabaseFromPackage(inst.file.typeTree.unityVersion);

            foreach (var inf in inst.table.GetAssetsOfType((int)AssetClassID.GameObject))
            {
                var baseField = am.GetTypeInstance(inst, inf).GetBaseField();

                var name = baseField.Get("m_Name").GetValue().AsString();
                Console.WriteLine(name);
            }

            am.UnloadAllAssetsFiles();
        }
    }
}