#region Copyright © 2020 Aver. All rights reserved.
/*
=====================================================
 AverFrameWork v1.0
 Filename:    SceneAssetRequestAsync.cs
 Author:      Zeng Zhiwei
 Time:        2020/12/11 10:44:54
=====================================================
*/
#endregion

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAssetRequestAsync : SceneAssetRequest
{
    private AsyncOperation _request;

    public SceneAssetRequestAsync(string path, bool addictive)
        : base(path, addictive)
    {
    }

    public override float progress
    {
        get
        {
            if(isDone) return 1;

            if(loadState == AssetLoadState.Init) return 0;

            if(_request != null) return _request.progress * 0.7f + 0.3f;

            if(BundleRequest == null) return 1;

            var value = BundleRequest.progress;
            var max = children.Count;
            if(max <= 0)
                return value * 0.3f;

            for(var i = 0; i < max; i++)
            {
                var item = children[i];
                value += item.progress;
            }

            return value / (max + 1) * 0.3f;
        }
    }

    private bool OnError(BundleRequest bundleRequest)
    {
        error = bundleRequest.error;
        if(!string.IsNullOrEmpty(error))
        {
            loadState = AssetLoadState.Loaded;
            return true;
        }

        return false;
    }

    internal override bool Update()
    {
        if(!base.Update()) return false;

        if(loadState == AssetLoadState.Init) return true;

        if(_request == null)
        {
            if(BundleRequest == null)
            {
                error = "bundle == null";
                loadState = AssetLoadState.Loaded;
                return false;
            }

            if(!BundleRequest.isDone) return true;

            if(OnError(BundleRequest)) return false;

            for(var i = 0; i < children.Count; i++)
            {
                var item = children[i];
                if(!item.isDone) return true;
                if(OnError(item)) return false;
            }

            LoadScene();

            return true;
        }

        if(_request.isDone)
        {
            loadState = AssetLoadState.Loaded;
            Complete();
            return false;
        }

        return true;
    }

    private void LoadScene()
    {
        try
        {
            _request = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
            loadState = AssetLoadState.LoadAsset;
        }
        catch(Exception e)
        {
            GameLog.LogException(e);
            error = e.Message;
            loadState = AssetLoadState.Loaded;
        }
    }

    internal override void Load()
    {
        if(!string.IsNullOrEmpty(assetBundleName))
        {
            BundleRequest = LoadManager.LoadBundleAsync(assetBundleName);
            var bundles = LoadManager.GetAllDependencies(assetBundleName);
            foreach(var item in bundles) children.Add(LoadManager.LoadBundleAsync(item));
            loadState = AssetLoadState.LoadAssetBundle;
        }
        else
        {
            LoadScene();
        }
    }

    internal override void Unload()
    {
        base.Unload();
        _request = null;
    }
}