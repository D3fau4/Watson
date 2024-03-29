﻿using AssetsTools.NET;
using AssetsTools.NET.Extra;
using Watson.Lib.IO;

namespace Watson.Lib.Assets;

public class TMPFont : IAsset
{
    public UnityAssetFile m_AssetFile;
    public Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>> m_FontNames;
    public Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>> m_FontTextures;

    public TMPFont(UnityAssetFile FontBundle)
    {
        m_FontNames = new Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>>();
        m_FontTextures =
            new Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>>();

        m_AssetFile = FontBundle;
        Load();
    }

    public void Load()
    {
        // Listar todas las fuentes
        foreach (var m_Asset in m_AssetFile.GetAssetsOfType(AssetClassID.MonoBehaviour))
        {
            var deserialized =
                m_AssetFile.AM.GetBaseField(m_AssetFile.Assets, m_Asset);
            var asset = deserialized.Get("m_fontInfo");
            if (asset != null)
                // Almacenar el nombre de asset que contiene la fuente.
                m_FontNames.Add(m_Asset.PathId,
                    Tuple.Create(deserialized.Get("m_Name").Value.AsString, deserialized, m_Asset,
                        m_AssetFile.Assets));
        }

        // Buscar Texture2D
        foreach (var m_Texture in m_AssetFile.GetAssetsOfType(AssetClassID.Texture2D))
        {
            var baseField = m_AssetFile.AM.GetBaseField(m_AssetFile.Assets, m_Texture);
            m_FontTextures.Add(m_Texture.PathId,
                Tuple.Create(baseField["m_Name"].Value.AsString, baseField, m_Texture, m_AssetFile.Assets));
        }
    }

    public void Close()
    {
        m_FontNames.Clear();
        m_FontTextures.Clear();
        m_AssetFile.Close();
    }
}