[System.Serializable]
public class AssetConfig
{
    public static AssetConfig Instance { get; private set; }
    public bool UseAssetBundle = false;

    public void Init()
    {
        Instance = this;

    }
}