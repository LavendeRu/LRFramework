using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ABBrowserTest : MonoBehaviour
{
    private string bundleName = "uilogin";
    private string assetName = "uilogin";
    // Start is called before the first frame update
    void Start()
    {
        this.InstantiateBundle();
    }

    private void InstantiateBundle()
    {
        AssetBundle asset = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath,bundleName));
        var asset_gob = asset.LoadAsset<GameObject>(assetName);

        MonoBehaviour.Instantiate(asset_gob);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
