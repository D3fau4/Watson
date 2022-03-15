using AssetsTools.NET;
using AssetsTools.NET.Extra;
using Watson.Lib.IO;

namespace Watson.Lib.Utils
{
    public static class TMPFont_Importer
    {
        public static List<string> GetToImportList(Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> NewFontNames, 
            Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> OldFontNames)
        {
            List<string> ToImport = new List<string>();
            // Buscar fuentes compatibles para importar
            foreach (var font in NewFontNames)
            {
                foreach (var fontold in OldFontNames)
                {
                    // Puede que cambie en otras versiones
                    if (font.Value.Item1.Replace(" Atlas", "-tex").Contains(fontold.Value.Item1))
                    {
                        ToImport.Add(font.Value.Item1);
                        break;
                    }
                }
            }

            return ToImport;
        }

        // Esto es mas feo que pegar a un padre
        public static List<AssetsReplacer> Import(
            Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> NewFontNames, 
            Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> OldFontNames, 
            Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> NewFontTextures2D,
            Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> OldFontTextures2D)
        {
            List<AssetsReplacer> m = new List<AssetsReplacer>();

            foreach (var font in NewFontNames)
            {
                foreach(var fontold in OldFontNames)
                {
                    if (font.Value.Item1.Contains(fontold.Value.Item1))
                    {
                        /* Remplazar m_Script */
                        // Establece el FileID
                        font.Value.Item2["m_Script"]["m_FileID"].GetValue().Set(fontold.Value.Item2["m_Script"]["m_FileID"].GetValue().AsInt64());
                        // Establece el PathID
                        font.Value.Item2["m_Script"]["m_PathID"].GetValue().Set(fontold.Value.Item2["m_Script"]["m_PathID"].GetValue().AsInt64());

                        /* Remplazar Material */
                        // Establece el FileID
                        font.Value.Item2["material"]["m_FileID"].GetValue().Set(fontold.Value.Item2["material"]["m_FileID"].GetValue().AsInt64());
                        // Establece el PathID
                        font.Value.Item2["material"]["m_PathID"].GetValue().Set(fontold.Value.Item2["material"]["m_PathID"].GetValue().AsInt64());

                        /* Remplazar Atlas */
                        // Establece el FileID
                        font.Value.Item2["atlas"]["m_FileID"].GetValue().Set(fontold.Value.Item2["atlas"]["m_FileID"].GetValue().AsInt64());
                        // Establece el PathID
                        font.Value.Item2["atlas"]["m_PathID"].GetValue().Set(fontold.Value.Item2["atlas"]["m_PathID"].GetValue().AsInt64());

                        var newMonoBytes = font.Value.Item2.WriteToByteArray();

                        m.Add(new AssetsReplacerFromMemory(
                            0, fontold.Value.Item3.index, (int)fontold.Value.Item3.curFileType,
                            AssetHelper.GetScriptIndex(fontold.Value.Item4.file, fontold.Value.Item3), newMonoBytes
                        ));

                        break;
                    }
                }
            }

            foreach (var font in NewFontTextures2D)
            {
                foreach (var fontold in OldFontTextures2D)
                {
                    if (font.Value.Item1.Replace(" Atlas", "-tex").Contains(fontold.Value.Item1))
                    {
                        var encImageBytes = Helpers.TextureHelper.GetRawTextureBytes(TextureFile.ReadTextureFile(font.Value.Item2), font.Value.Item4);

                        AssetTypeValueField m_StreamData = font.Value.Item2.Get("m_StreamData");
                        // Limpiar referencias a la textura
                        m_StreamData.Get("offset").GetValue().Set(0);
                        m_StreamData.Get("size").GetValue().Set(0);
                        m_StreamData.Get("path").GetValue().Set("");

                        font.Value.Item2["m_StreamData"] = m_StreamData;

                        /* Escribe la texture en assets */
                        AssetTypeValueField image_data = font.Value.Item2.Get("image data");
                        image_data.GetValue().type = EnumValueTypes.ByteArray;
                        image_data.templateField.valueType = EnumValueTypes.ByteArray;
                        AssetTypeByteArray byteArray = new AssetTypeByteArray()
                        {
                            size = (uint)encImageBytes.Length,
                            data = encImageBytes
                        };
                        image_data.GetValue().Set(byteArray);
                        font.Value.Item2["image data"] = image_data;

                        var Texture2Data = font.Value.Item2.WriteToByteArray();

                        File.WriteAllBytes("sprite.bin", encImageBytes);

                        m.Add(new AssetsReplacerFromMemory(
                            0, fontold.Value.Item3.index, (int)fontold.Value.Item3.curFileType,
                            AssetHelper.GetScriptIndex(fontold.Value.Item4.file, fontold.Value.Item3), Texture2Data
                        ));
                    }
                }
            }
            return m;
        }
    }
}
