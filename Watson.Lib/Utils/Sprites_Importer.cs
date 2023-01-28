using AssetsTools.NET;
using AssetsTools.NET.Extra;
using Watson.Lib.Utils.Helpers;
using AssetHelper = AssetsTools.NET.Extra.AssetHelper;

namespace Watson.Lib.Utils;

public static class Sprites_Importer
{
    public static List<AssetsReplacer> Import(
        Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> NewSprites,
        Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> OldSprites,
        Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> NewTextures2D,
        Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfoEx, AssetsFileInstance>> OldTextures2D,
        string oldsuffix = " Atlas", string newsuffix = "-tex")
    {
        var m = new List<AssetsReplacer>();

        foreach (var sprite in NewSprites)
        foreach (var spriteold in OldSprites)
            if (sprite.Value.Item1.Equals(spriteold.Value.Item1))
            {
                sprite.Value.Item2["m_RenderDataKey"]["first"][0].GetValue()
                    .Set(spriteold.Value.Item2["m_RenderDataKey"]["first"][0].GetValue().AsUInt());
                sprite.Value.Item2["m_RenderDataKey"]["first"][1].GetValue()
                    .Set(spriteold.Value.Item2["m_RenderDataKey"]["first"][1].GetValue().AsUInt());
                sprite.Value.Item2["m_RenderDataKey"]["first"][2].GetValue()
                    .Set(spriteold.Value.Item2["m_RenderDataKey"]["first"][2].GetValue().AsUInt());
                sprite.Value.Item2["m_RenderDataKey"]["first"][3].GetValue()
                    .Set(spriteold.Value.Item2["m_RenderDataKey"]["first"][0].GetValue().AsUInt());

                sprite.Value.Item2["m_RenderDataKey"]["second"].GetValue()
                    .Set(spriteold.Value.Item2["m_RenderDataKey"]["second"].GetValue().AsInt64());

                sprite.Value.Item2["m_RD"]["texture"]["m_FileID"].GetValue()
                    .Set(spriteold.Value.Item2["m_RD"]["texture"]["m_FileID"].GetValue().AsInt());
                sprite.Value.Item2["m_RD"]["texture"]["m_PathID"].GetValue()
                    .Set(spriteold.Value.Item2["m_RD"]["texture"]["m_PathID"].GetValue().AsInt64());

                var SpriteData = sprite.Value.Item2.WriteToByteArray();

                m.Add(new AssetsReplacerFromMemory(
                    0, spriteold.Value.Item3.index, (int) spriteold.Value.Item3.curFileType,
                    AssetHelper.GetScriptIndex(spriteold.Value.Item4.file, spriteold.Value.Item3), SpriteData
                ));
            }

        foreach (var sprite in NewTextures2D)
        foreach (var spriteold in OldTextures2D)
            if (sprite.Value.Item1.Equals(spriteold.Value.Item1))
            {
                var encImageBytes = TextureHelper.GetRawTextureBytes(TextureFile.ReadTextureFile(sprite.Value.Item2),
                    sprite.Value.Item4);

                var m_StreamData = sprite.Value.Item2.Get("m_StreamData");
                // Limpiar referencias a la textura
                m_StreamData.Get("offset").GetValue().Set(0);
                m_StreamData.Get("size").GetValue().Set(0);
                m_StreamData.Get("path").GetValue().Set("");

                sprite.Value.Item2["m_StreamData"] = m_StreamData;

                /* Escribe la texture en assets */
                var image_data = sprite.Value.Item2.Get("image data");
                image_data.GetValue().type = EnumValueTypes.ByteArray;
                image_data.templateField.valueType = EnumValueTypes.ByteArray;
                var byteArray = new AssetTypeByteArray
                {
                    size = (uint) encImageBytes.Length,
                    data = encImageBytes
                };
                image_data.GetValue().Set(byteArray);
                sprite.Value.Item2["image data"] = image_data;

                var Texture2Data = sprite.Value.Item2.WriteToByteArray();

                //File.WriteAllBytes("sprite.bin", encImageBytes);

                m.Add(new AssetsReplacerFromMemory(
                    0, spriteold.Value.Item3.index, (int) spriteold.Value.Item3.curFileType,
                    AssetHelper.GetScriptIndex(spriteold.Value.Item4.file, spriteold.Value.Item3), Texture2Data
                ));
            }

        return m;
    }
}