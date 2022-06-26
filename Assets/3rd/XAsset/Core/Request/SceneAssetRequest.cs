#region Copyright © 2020 Aver. All rights reserved.
/*
=====================================================
 AverFrameWork v1.0
 Filename:    SceneAssetRequest.cs
 Author:      Zeng Zhiwei
 Time:        2020/12/11 10:44:19
=====================================================
*/
#endregion

using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

public class SceneAssetRequest : AssetRequest
{
    protected readonly string sceneName;
    public string assetBundleName;
    protected BundleRequest BundleRequest;
    protected List<BundleRequest> children = new List<BundleRequest>();

    public SceneAssetRequest(string path, bool addictive)
    {
        name = path;
        LoadManager.GetAssetBundleName(path, out assetBundleName);
        sceneName = Path.GetFileNameWithoutExtension(name);
        loadSceneMode = addictive ? LoadSceneMode.Additive : LoadSceneMode.Single;
    }

    public LoadSceneMode loadSceneMode { get; protected set; }

    public override float progress
    {
        get { return 1; }
    }

    internal override void Load()
    {
        if(!string.IsNullOrEmpty(assetBundleName))
        {
            BundleRequest = LoadManager.LoadBundle(assetBundleName);
            if(BundleRequest != null)
            {
                var bundles = LoadManager.GetAllDependencies(assetBundleName);
                foreach(var item in bundles) children.Add(LoadManager.LoadBundle(item));
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, loadSceneMode);
            }
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, loadSceneMode);
        }

        loadState = AssetLoadState.Loaded;
    }

    internal override void Unload()
    {
        if(BundleRequest != null)
            BundleRequest.Release();

        if(children.Count > 0)
        {
            foreach(var item in children) item.Release();
            children.Clear();
        }

        if(loadSceneMode == LoadSceneMode.Additive)
            if(UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName).isLoaded)
                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);

        BundleRequest = null;
        loadState = AssetLoadState.Unload;
    }
}