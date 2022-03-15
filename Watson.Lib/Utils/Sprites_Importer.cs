using AssetsTools.NET;
using AssetsTools.NET.Extra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watson.Lib.Utils
{
    public static class Sprites_Importer
    {
        public static List<AssetsReplacer> Import(
            Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> NewSprites,
            Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> OldSprites,
            Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> NewTextures2D,
            Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> OldTextures2D)
        {
            List<AssetsReplacer> m = new List<AssetsReplacer>();

            foreach (var sprite in NewSprites)
            {
                foreach (var spriteold in OldSprites)
                {
                    if (sprite.Value.Item1.Equals(spriteold.Value.Item1))
                    {
                        sprite.Value.Item2["m_RenderDataKey"] = spriteold.Value.Item2["m_RenderDataKey"];
                        sprite.Value.Item2["m_RD"]["texture"] = spriteold.Value.Item2["m_RD"]["texture"];

                        var SpriteData = sprite.Value.Item2.WriteToByteArray();

                        m.Add(new AssetsReplacerFromMemory(
                            0, spriteold.Value.Item3.index, (int)spriteold.Value.Item3.curFileType,
                            AssetHelper.GetScriptIndex(spriteold.Value.Item4.file, spriteold.Value.Item3), SpriteData
                        ));
                    }
                }
            }

            foreach (var sprite in NewTextures2D)
            {
                foreach (var spriteold in OldTextures2D)
                {
                    if (sprite.Value.Item1.Equals(spriteold.Value.Item1))
                    {
                        var Texture2Data = sprite.Value.Item2.WriteToByteArray();

                        m.Add(new AssetsReplacerFromMemory(
                            0, spriteold.Value.Item3.index, (int)spriteold.Value.Item3.curFileType,
                            AssetHelper.GetScriptIndex(spriteold.Value.Item4.file, spriteold.Value.Item3), Texture2Data
                        ));
                    }
                }
            }

            return m;
        }
    }
}
