using AssetsTools.NET;
using AssetsTools.NET.Extra;

namespace Watson.Lib.Utils;

public static class TMPFont_Importer
{
    public static List<string> GetToImportList(
        Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>> NewFontNames,
        Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>> OldFontNames,
        string oldsuffix = "", string newsuffix = "")
    {
        var ToImport = new List<string>();
        // Buscar fuentes compatibles para importar
        foreach (var font in NewFontNames)
        foreach (var fontold in OldFontNames)
        {
            var tmpname = font.Value.Item1;
            if (oldsuffix != string.Empty && newsuffix != string.Empty)
                tmpname = tmpname.Replace(oldsuffix, newsuffix);
            // Puede que cambie en otras versiones
            if (tmpname.Equals(fontold.Value.Item1))
            {
                ToImport.Add(font.Value.Item1);
                break;
            }
        }


        return ToImport;
    }

    // Esto es mas feo que pegar a un padre
    public static List<AssetsReplacer> Import(
        Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>> NewFontNames,
        Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>> OldFontNames,
        Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>> NewFontTextures2D,
        Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>> OldFontTextures2D,
        string oldsuffix = "", string newsuffix = "")
    {
        var m = new List<AssetsReplacer>();
        throw new NotImplementedException();
        return m;
    }
}