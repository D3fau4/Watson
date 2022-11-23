using AssetsTools.NET;
using AssetsTools.NET.Extra;

namespace Watson.Lib.IO;

public class UnityAssetFile
{
    public AssetsManager AM;
    public string AssetName;
    public AssetsFileInstance Assets;
    public BundleFileInstance Bundle;
    public bool IsBundle;

    public UnityAssetFile(Stream stream, bool IsBundle = false)
    {
        throw new NotImplementedException();
    }

    public UnityAssetFile(string file)
    {
        AssetName = Path.GetFileName(file);
        AM = new AssetsManager();
        try
        {
            Assets = AM.LoadAssetsFile(file, true);

            AM.LoadClassPackage(new MemoryStream(Resources.Resources.classdata));
            AM.LoadClassDatabaseFromPackage(Assets.file.Metadata.UnityVersion);
        }
        catch (Exception ex)
        {
            // Si recibe un error de que el archivo es muy pequeño intentar abrir como AssetBundle. 
            if (ex.Message.Contains("too small") || ex.Message.Contains("Unable to read beyond the end"))
            {
                // Descargar lo que haya conseguido cargar.
                AM.UnloadAll();

                Bundle = AM.LoadBundleFile(file);

                // Siempre index 0 ya que es el que contiene todos los archivos
                Assets = AM.LoadAssetsFileFromBundle(Bundle, 0, true);

                AM.LoadClassPackage(new MemoryStream(Resources.Resources.classdata));
                AM.LoadClassDatabaseFromPackage(Assets.file.Metadata.UnityVersion);
                IsBundle = true;
            }
        }
    }

    public void Close()
    {
        AM.UnloadAll();
    }

    public List<AssetFileInfo> GetAssetsOfType(AssetClassID ID)
    {
        var list = new List<AssetFileInfo>();
        foreach (var inf in Assets.file.GetAssetsOfType((int)ID)) list.Add(inf);
        return list;
    }
}