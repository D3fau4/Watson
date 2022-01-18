using AssetsTools.NET;
using AssetsTools.NET.Extra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watson.Lib.IO
{
    public class TMPFont
    {
        private UnityAssets m_Assets;
        private Assembly m_DLL;
        public Dictionary<long, Tuple<string, AssetTypeValueField>> m_FontNames;
        public Dictionary<long, Tuple<string, AssetTypeValueField>> m_FontTextures;

        public TMPFont(string FontBundle, Assembly assembly)
        {
            m_FontNames = new Dictionary<long, Tuple<string, AssetTypeValueField>>();
            m_FontTextures = new Dictionary<long, Tuple<string, AssetTypeValueField>>();

            m_Assets = new UnityAssets(FontBundle);
            m_DLL = assembly;

            if (assembly.assemblyType == Assembly.AssemblyType.IL2CPP)
                throw new Exception("IL2CPP not supported");

            // Listar todas las fuentes
            foreach (var Asset in m_Assets.GetAssetsOfType(AssetClassID.MonoBehaviour))
            {
                var deserialized = MonoDeserializer.GetMonoBaseField(m_Assets.AM, m_Assets.Assets, Asset, m_DLL.AssemblyFolder);
                var asset = deserialized.Get("m_fontInfo");

                if (asset != null)
                    // Almacenar el nombre de asset que contiene la fuente.
                    m_FontNames.Add(Asset.index, Tuple.Create(deserialized.Get("m_Name").GetValue().AsString(), deserialized));
            }

            // Buscar Texture2D
            foreach (var Texture in m_Assets.GetAssetsOfType(AssetClassID.Texture2D))
            {
                var baseField = m_Assets.AM.GetTypeInstance(m_Assets.Assets, Texture).GetBaseField();
                m_FontTextures.Add(Texture.index, Tuple.Create(baseField["m_Name"].value.AsString(), baseField));
            }
        }
    }
}
