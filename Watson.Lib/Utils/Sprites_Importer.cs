﻿using AssetsTools.NET;
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
                        var encImageBytes = Helpers.TextureHelper.GetRawTextureBytes(TextureFile.ReadTextureFile(sprite.Value.Item2), sprite.Value.Item4);

                        AssetTypeValueField m_StreamData = sprite.Value.Item2.Get("m_StreamData");
                        // Limpiar referencias a la textura
                        m_StreamData.Get("offset").GetValue().Set(0);
                        m_StreamData.Get("size").GetValue().Set(0);
                        m_StreamData.Get("path").GetValue().Set("");

                        sprite.Value.Item2["m_StreamData"] = m_StreamData;

                        /* Escribe la texture en assets */
                        AssetTypeValueField image_data = sprite.Value.Item2.Get("image data");
                        image_data.GetValue().type = EnumValueTypes.ByteArray;
                        image_data.templateField.valueType = EnumValueTypes.ByteArray;
                        AssetTypeByteArray byteArray = new AssetTypeByteArray()
                        {
                            size = (uint)encImageBytes.Length,
                            data = encImageBytes
                        };
                        image_data.GetValue().Set(byteArray);
                        sprite.Value.Item2["image data"] = image_data;

                        var Texture2Data = sprite.Value.Item2.WriteToByteArray();

                        File.WriteAllBytes("sprite.bin", encImageBytes);

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
