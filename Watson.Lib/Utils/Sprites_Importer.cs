using AssetsTools.NET;
using AssetsTools.NET.Extra;
using Watson.Lib.Utils.Helpers;

namespace Watson.Lib.Utils;

public static class Sprites_Importer
{
    public static List<AssetsReplacer> Import(
        Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>> NewSprites,
        Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>> OldSprites,
        Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>> NewTextures2D,
        Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>> OldTextures2D)
    {
        var m = new List<AssetsReplacer>();

        foreach (var sprite in NewSprites)
        foreach (var spriteold in OldSprites)
            if (sprite.Value.Item1.Equals(spriteold.Value.Item1))
            {
                // 
                sprite.Value.Item2["m_RenderDataKey"]["first"][0].Value.AsUInt =
                    spriteold.Value.Item2["m_RenderDataKey"]["first"][0].AsUInt;
                sprite.Value.Item2["m_RenderDataKey"]["first"][1].Value.AsUInt =
                    spriteold.Value.Item2["m_RenderDataKey"]["first"][1].Value.AsUInt;
                sprite.Value.Item2["m_RenderDataKey"]["first"][2].Value.AsUInt
                    = spriteold.Value.Item2["m_RenderDataKey"]["first"][2].Value.AsUInt;
                sprite.Value.Item2["m_RenderDataKey"]["first"][3].Value.AsUInt
                    = spriteold.Value.Item2["m_RenderDataKey"]["first"][0].Value.AsUInt;

                sprite.Value.Item2["m_RenderDataKey"]["second"].Value.AsULong =
                    spriteold.Value.Item2["m_RenderDataKey"]["second"].Value.AsULong;

                sprite.Value.Item2["m_RD"]["texture"]["m_FileID"].Value.AsInt
                    = spriteold.Value.Item2["m_RD"]["texture"]["m_FileID"].Value.AsInt;
                sprite.Value.Item2["m_RD"]["texture"]["m_PathID"].Value
                    .AsLong = spriteold.Value.Item2["m_RD"]["texture"]["m_PathID"].Value.AsLong;

                var SpriteData = sprite.Value.Item2.WriteToByteArray();
                
                m.Add(new AssetsReplacerFromMemory(spriteold.Value.Item4.file, spriteold.Value.Item3, SpriteData));
            }

        foreach (var sprite in NewTextures2D)
        foreach (var spriteold in OldTextures2D)
            if (sprite.Value.Item1.Equals(spriteold.Value.Item1))
            {
                var encImageBytes = TextureHelper.GetRawTextureBytes(TextureFile.ReadTextureFile(sprite.Value.Item2),
                    sprite.Value.Item4);

                var m_StreamData = sprite.Value.Item2.Get("m_StreamData");
                // Limpiar referencias a la textura
                m_StreamData.Get("offset").Value.AsLong = 0;
                m_StreamData.Get("size").Value.AsLong = 0;
                m_StreamData.Get("path").Value.AsString = "";

                sprite.Value.Item2["m_StreamData"].Value = m_StreamData.Value;

                /* Escribe la texture en assets */
                sprite.Value.Item2["image data"].Value.ValueType = AssetValueType.Array;
                sprite.Value.Item2["image data"].TemplateField.ValueType = AssetValueType.ByteArray;

                sprite.Value.Item2["image data"].Value.AsByteArray = encImageBytes;

                var Texture2Data = sprite.Value.Item2.WriteToByteArray();
                
                m.Add(new AssetsReplacerFromMemory(sprite.Value.Item4.file, spriteold.Value.Item3, Texture2Data));
            }

        return m;
    }
}