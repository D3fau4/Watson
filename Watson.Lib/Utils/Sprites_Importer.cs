using AssetsTools.NET;
using AssetsTools.NET.Extra;
using AssetsTools.NET.Texture;
using Watson.Lib.Assets;
using Watson.Lib.IO;
using Watson.Lib.Utils.Helpers;

namespace Watson.Lib.Utils;

public static class Sprites_Importer
{
    public static Sprites Import(
        Sprites NewSprites,
        Sprites OldSprites)
    {
        
        foreach (var sprite in NewSprites.m_Sprites)
        foreach (var spriteold in OldSprites.m_Sprites)
            if (sprite.Value.Item1.Equals(spriteold.Value.Item1))
            {
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
                
                spriteold.Value.Item3.SetNewData(sprite.Value.Item2);
            }

        foreach (var sprite in NewSprites.m_Texture2D)
        foreach (var spriteold in OldSprites.m_Texture2D)
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
                spriteold.Value.Item3.SetNewData(sprite.Value.Item2);
            }

        return OldSprites;
    }
}