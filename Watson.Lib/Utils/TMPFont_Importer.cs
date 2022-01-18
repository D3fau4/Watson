using AssetsTools.NET;
using AssetsTools.NET.Extra;
using Watson.Lib.IO;

namespace Watson.Lib.Utils
{
    public static class TMPFont_Importer
    {
        public static List<string> GetToImportList(Dictionary<long, Tuple<string, AssetTypeValueField>> NewFontNames, Dictionary<long, Tuple<string, AssetTypeValueField>> OldFontNames)
        {
            List<string> ToImport = new List<string>();
            // Buscar fuentes compatibles para importar
            foreach (var font in OldFontNames)
            {
                foreach (var fontnew in NewFontNames)
                {
                    if (font.Value.Item1.Contains(fontnew.Value.Item1))
                    {
                        ToImport.Add(font.Value.Item1);
                        break;
                    }
                }
            }

            return ToImport;
        }

        public static void Import(Dictionary<long, Tuple<string, AssetTypeValueField>> NewFontNames, Dictionary<long, Tuple<string, AssetTypeValueField>> OldFontNames)
        {
            foreach(var font in NewFontNames)
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

                        /* Remplazar Material */
                        // Establece el FileID
                        font.Value.Item2["atlas"]["m_FileID"].GetValue().Set(fontold.Value.Item2["atlas"]["m_FileID"].GetValue().AsInt64());
                        // Establece el PathID
                        font.Value.Item2["atlas"]["m_PathID"].GetValue().Set(fontold.Value.Item2["atlas"]["m_PathID"].GetValue().AsInt64());
                        
                        var newMonoBytes = font.Value.Item2.WriteToByteArray();

                        break;
                    }
                }
            }
        }
    }
}
