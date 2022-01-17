using AssetsTools.NET;
using AssetsTools.NET.Extra;

namespace Watson.Lib.IO
{
    public class UnityAssets
    {
        public AssetsManager AM;
        public  AssetsFileInstance Assets;
        private BundleFileInstance Bundle;

        public UnityAssets(Stream stream, bool IsBundle = false) => throw new NotImplementedException();

        public UnityAssets(string file, bool IsBundle = false)
        {
            if (!IsBundle)
            {
                AM = new AssetsManager();

                Assets = AM.LoadAssetsFile(file, true);

                AM.LoadClassPackage(new MemoryStream(Resources.Resources.classdata));
                AM.LoadClassDatabaseFromPackage(Assets.file.typeTree.unityVersion);
            }
            else
            {
                AM = new AssetsManager();

                Bundle = AM.LoadBundleFile(file, true);
                // Siempre index 0 ya que es el que contiene todos los archivos
                Assets = AM.LoadAssetsFileFromBundle(Bundle, 0, true);

                AM.LoadClassPackage(new MemoryStream(Resources.Resources.classdata));
                AM.LoadClassDatabaseFromPackage(Assets.file.typeTree.unityVersion);
            }
        }

        public List<AssetFileInfoEx> GetAssetsOfType(AssetClassID ID)
        {
            List<AssetFileInfoEx> list = new List<AssetFileInfoEx>();
            foreach (var inf in Assets.table.GetAssetsOfType((int)ID))
            {
                list.Add(inf);
            }
            return list;
        }

        public void Close()
        {
            AM.UnloadAll();
        }
    }
}
