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

        public List<string> OldFontNames = new List<string>();
        public List<string> NewFontNames = new List<string>();
        

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
                    OldFontNames.Add(deserialized.Get("m_Name").GetValue().AsString());
            }

            // Listar todas las fuentes de OLD
            foreach (var newassets in _new.GetAssetsOfType(AssetClassID.MonoBehaviour))
            {
                var deserialized = MonoDeserializer.GetMonoBaseField(_new.AM, _new.Assets, newassets, _newDLL.AssemblyFolder);
                var asset = deserialized.Get("m_fontInfo");

                if (asset != null)
                    // Almacenar el nombre de asset que contiene la fuente.
                    NewFontNames.Add(deserialized.Get("m_Name").GetValue().AsString());
            }

            // Buscar Texture2D
        }

        public List<string> GetToImportList()
        {
            List<string> ToImport = new List<string>();
            // Buscar fuentes compatibles para importar
            foreach (var font in OldFontNames)
            {
                if (NewFontNames.Contains(font))
                    ToImport.Add(font);
            }

            return ToImport;
        }
    }
}
