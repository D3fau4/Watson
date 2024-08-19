using AssetsTools.NET;
using AssetsTools.NET.Extra;
using AssetsTools.NET.Texture;
using Watson.Lib.Assets;
using Watson.Lib.Utils.Helpers;

namespace Watson.Lib.Utils;

public static class TMPFont_Importer
{
    public static List<string> GetToImportList(
        TMPFont NewFontNames,
        TMPFont OldFontNames,
        string oldsuffix = "", string newsuffix = "")
    {
        var ToImport = new List<string>();
        // Buscar fuentes compatibles para importar
        foreach (var font in NewFontNames.m_FontNames)
        foreach (var fontold in OldFontNames.m_FontNames)
        {
            var tmpname = font.Value.Item1;
            if (oldsuffix != string.Empty && newsuffix != string.Empty)
                tmpname = tmpname.Replace(oldsuffix, newsuffix);
            // Puede que cambie en otras versiones
            if (tmpname.Equals(fontold.Value.Item1))
            {
                ToImport.Add(font.Value.Item1);
            }
        }


        return ToImport;
    }

    // Esto es mas feo que pegar a un padre
    public static TMPFont Import(
        TMPFont NewFontNames,
        TMPFont OldFontNames,
        string oldsuffix = "", string newsuffix = "")
    {
        foreach (var font in NewFontNames.m_FontNames)
        foreach (var fontold in OldFontNames.m_FontNames)
        {
            var tmpname = font.Value.Item1;
            if (oldsuffix != string.Empty && newsuffix != string.Empty)
                tmpname = tmpname.Replace(oldsuffix, newsuffix);
            if (tmpname.Equals(fontold.Value.Item1))
            {

                /* Remplazar m_Script */
                // Establece el FileID
                font.Value.Item2["m_Script"]["m_FileID"].Value = fontold.Value.Item2["m_Script"]["m_FileID"].Value;
                // Establece el PathID
                font.Value.Item2["m_Script"]["m_PathID"].Value = fontold.Value.Item2["m_Script"]["m_PathID"].Value;

                /* Remplazar Material */
                // Establece el FileID
                font.Value.Item2["material"]["m_FileID"].Value = fontold.Value.Item2["material"]["m_FileID"].Value;
                // Establece el PathID
                font.Value.Item2["material"]["m_PathID"].Value = fontold.Value.Item2["material"]["m_PathID"].Value;

                if (StringUtils.IsUnityVersionGreaterThan("2017.2.0", fontold.Value.Item4.file.Metadata.UnityVersion))
                {
                    /* Remplazar Atlas */
                    // Establece el FileID
                    font.Value.Item2["m_AtlasTextures"][0]["m_FileID"].Value = fontold.Value.Item2["m_AtlasTextures"][0]["m_FileID"].Value;
                    // Establece el PathID
                    font.Value.Item2["m_AtlasTextures"][0]["m_PathID"].Value = fontold.Value.Item2["m_AtlasTextures"][0]["m_PathID"].Value;
                }
                else
                {
                    /* Remplazar Atlas */
                    // Establece el FileID
                    font.Value.Item2["atlas"]["m_FileID"].Value = fontold.Value.Item2["atlas"]["m_FileID"].Value;
                    // Establece el PathID
                    font.Value.Item2["atlas"]["m_PathID"].Value = fontold.Value.Item2["atlas"]["m_PathID"].Value;
                }
                fontold.Value.Item3.SetNewData(font.Value.Item2);
            }
        }

        foreach (var font in NewFontNames.m_FontTextures)
        foreach (var fontold in OldFontNames.m_FontTextures)
        {
            var tmpname = font.Value.Item1;
            if (oldsuffix != string.Empty && newsuffix != string.Empty)
                tmpname = tmpname.Replace(oldsuffix, newsuffix);
            if (tmpname.Equals(fontold.Value.Item1))
            {
                var encImageBytes =
                    TextureHelper.GetRawTextureBytes(TextureFile.ReadTextureFile(font.Value.Item2), font.Value.Item4);

                var m_StreamData = font.Value.Item2.Get("m_StreamData");
                m_StreamData["offset"].Value.AsInt = 0;
                m_StreamData["size"].Value.AsInt = 0;
                m_StreamData["path"].Value.AsString = string.Empty;

                font.Value.Item2["m_StreamData"].Value = m_StreamData.Value;

                var image_data = font.Value.Item2.Get("image data");
                image_data.Value.ValueType = AssetValueType.ByteArray;
                image_data.TemplateField.ValueType = AssetValueType.ByteArray;
                image_data.Value.AsByteArray = encImageBytes;
                // TODO: Mirar que realmente esto funcione
                font.Value.Item2["image data"].Value = image_data.Value;

                fontold.Value.Item3.SetNewData(font.Value.Item2);
            }
        }

        return OldFontNames;
    }
}
