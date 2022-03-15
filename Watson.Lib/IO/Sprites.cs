using AssetsTools.NET;
using AssetsTools.NET.Extra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watson.Lib.IO
{
    public class Sprites
    {
        public UnityAssets m_Assets;
        public Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> m_Texture2D;
        public Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> m_Sprites;

        public Sprites(string SpriteBundle)
        {
            m_Sprites = new Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>>();
            m_Texture2D = new Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>>();

            m_Assets = new UnityAssets(SpriteBundle);

            // Buscar Sprites
            foreach (var Texture in m_Assets.GetAssetsOfType(AssetClassID.Sprite))
            {
                var baseField = m_Assets.AM.GetTypeInstance(m_Assets.Assets, Texture).GetBaseField();
                m_Sprites.Add(Texture.index, Tuple.Create(baseField["m_Name"].value.AsString(), baseField, Texture, m_Assets.Assets));
            }

            // Buscar Texture2D
            foreach (var Texture in m_Assets.GetAssetsOfType(AssetClassID.Texture2D))
            {
                var baseField = m_Assets.AM.GetTypeInstance(m_Assets.Assets, Texture).GetBaseField();
                m_Texture2D.Add(Texture.index, Tuple.Create(baseField["m_Name"].value.AsString(), baseField, Texture, m_Assets.Assets));
            }
        }
    }
}
