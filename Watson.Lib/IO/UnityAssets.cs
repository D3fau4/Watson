using AssetsTools.NET;
using AssetsTools.NET.Extra;

namespace Watson.Lib.IO
{
    public class UnityAssets
    {
        public AssetsManager AM;
        public  AssetsFileInstance Assets;
        public BundleFileInstance Bundle;
        public bool IsBundle = false;
        public string AssetName;

        public UnityAssets(Stream stream, bool IsBundle = false) => throw new NotImplementedException();
        public void Close() => AM.UnloadAll();

        public UnityAssets(string file)
        {
            AssetName = Path.GetFileName(file);
            AM = new AssetsManager();
            try
            {
                Assets = AM.LoadAssetsFile(file, true);

                AM.LoadClassPackage(new MemoryStream(Resources.Resources.classdata));
                AM.LoadClassDatabaseFromPackage(Assets.file.typeTree.unityVersion);
            } 
            catch(Exception ex)
            {
                // Si recibe un error de que el archivo es muy pequeño intentar abrir como AssetBundle. 
                if (ex.Message.Contains("too small"))
                {
                    // Descargar lo que haya conseguido cargar.
                    AM.UnloadAll();

                    Bundle = AM.LoadBundleFile(file, true);

                    // Siempre index 0 ya que es el que contiene todos los archivos
                    Assets = AM.LoadAssetsFileFromBundle(Bundle, 0, true);

                    AM.LoadClassPackage(new MemoryStream(Resources.Resources.classdata));
                    AM.LoadClassDatabaseFromPackage(Assets.file.typeTree.unityVersion);
                    IsBundle = true;
                }
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
    }
}
