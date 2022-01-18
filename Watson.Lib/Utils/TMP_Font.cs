using AssetsTools.NET;
using AssetsTools.NET.Extra;
using Watson.Lib.IO;

namespace Watson.Lib.Utils
{
    public class TMP_Font
    {
        private UnityAssets _old;
        private UnityAssets _new;
        private Assembly _oldDLL;
        private Assembly _newDLL;

        public Dictionary<long, Tuple<string, AssetTypeValueField>> OldFontNames = new Dictionary<long, Tuple<string, AssetTypeValueField>>();
        public Dictionary<long, Tuple<string, AssetTypeValueField>> NewFontNames = new Dictionary<long, Tuple<string, AssetTypeValueField>>();
        
        public Dictionary<long, Tuple<string, AssetTypeValueField>> OldFontTextures = new Dictionary<long, Tuple<string, AssetTypeValueField>>();
        public Dictionary<long, Tuple<string, AssetTypeValueField>> NewFontTextures = new Dictionary<long, Tuple<string, AssetTypeValueField>>();

        public TMP_Font(string FontBundleOld, string FontBundleNew, Assembly Oldassembly, Assembly Newassembly)
        {
            _old = new UnityAssets(FontBundleOld);
            _new = new UnityAssets(FontBundleNew);

            _oldDLL = Oldassembly;
            _newDLL = Newassembly;

            if (_newDLL.assemblyType == Assembly.AssemblyType.IL2CPP || _oldDLL.assemblyType == Assembly.AssemblyType.IL2CPP)
                throw new Exception("IL2CPP not supported");

            // Listar todas las fuentes de OLD
            foreach (var oldassets in _old.GetAssetsOfType(AssetClassID.MonoBehaviour))
            {
                var deserialized = MonoDeserializer.GetMonoBaseField(_old.AM, _old.Assets, oldassets, _oldDLL.AssemblyFolder);
                var asset = deserialized.Get("m_fontInfo");

                if (asset != null)
                    // Almacenar el nombre de asset que contiene la fuente.
                    OldFontNames.Add(oldassets.index, Tuple.Create(deserialized.Get("m_Name").GetValue().AsString(), deserialized));
            }

            // Listar todas las fuentes de OLD
            foreach (var newassets in _new.GetAssetsOfType(AssetClassID.MonoBehaviour))
            {
                var deserialized = MonoDeserializer.GetMonoBaseField(_new.AM, _new.Assets, newassets, _newDLL.AssemblyFolder);
                var asset = deserialized.Get("m_fontInfo");

                if (asset != null)
                    // Almacenar el nombre de asset que contiene la fuente.
                    NewFontNames.Add(newassets.index, Tuple.Create(deserialized.Get("m_Name").GetValue().AsString(), deserialized));
            }

            // Buscar Texture2D
            foreach (var oldtextures in _old.GetAssetsOfType(AssetClassID.Texture2D))
            {
                var baseField = _old.AM.GetTypeInstance(_old.Assets, oldtextures).GetBaseField();
                OldFontTextures.Add(oldtextures.index, Tuple.Create(baseField["m_Name"].value.AsString(), baseField));
            }

            foreach (var newtextures in _new.GetAssetsOfType(AssetClassID.Texture2D))
            {
                var baseField = _new.AM.GetTypeInstance(_new.Assets, newtextures).GetBaseField();
                NewFontTextures.Add(newtextures.index, Tuple.Create(baseField["m_Name"].value.AsString(), baseField));
            }
        }

        public List<string> GetToImportList()
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

        public void Import()
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

        public void Close()
        {
            _old.Close();
            _new.Close();
        }
    }
}
