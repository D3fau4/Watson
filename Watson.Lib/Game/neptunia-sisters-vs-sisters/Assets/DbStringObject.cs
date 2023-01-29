using AssetsTools.NET;
using AssetsTools.NET.Extra;
using Watson.Lib.Assets;
using Watson.Lib.IO;

namespace Watson.Lib.Game.neptunia_sisters_vs_sisters.Assets;

public class DbStringObject : IAsset
{
    public UnityAssetFile m_AssetFile;
    public Dictionary<long, Tuple<string, AssetTypeValueField, AssetFileInfo, AssetsFileInstance>> m_TextsAssets;

    public DbStringObject(UnityAssetFile mAssetFile)
    {
        m_AssetFile = mAssetFile;
        m_TextsAssets = new();
    }

    public void Load()
    {
        foreach (var m_TextAsset in m_AssetFile.GetAssetsOfType(AssetClassID.MonoBehaviour))
        {
            var baseField = m_AssetFile.AM.GetBaseField(m_AssetFile.Assets, m_TextAsset);

            for (int i = 0; i < baseField.Children.Count; i++)
            {
                if (baseField.Children[i].TypeName.Equals("DbStringMake"))
                {
                    m_TextsAssets.Add(m_TextAsset.PathId,
                        Tuple.Create(baseField["m_Name"].Value.AsString, baseField, m_TextAsset, m_AssetFile.Assets));
                }
            }
            
            
        }
    }

    public void Close()
    {
        throw new NotImplementedException();
    }
}