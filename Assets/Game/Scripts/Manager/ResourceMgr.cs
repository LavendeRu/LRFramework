using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class ResourceMgr : MonoSingleton<ResourceMgr>
{
    private void Awake()
    {
        this.gameObject.AddComponent<AssetBundleManager>();
    }
    public T GetUIAssetCache<T>(string rName) where T : Object
    {
        //#if UNITY_EDITIR
        string rPath = "Assets/AssetPackages/GUI/Prefabs" + rName;
        object rTarget = AssetDatabase.LoadAssetAtPath<T>(rPath);
        return rTarget as T;
        //#endif
        //@ToDo AssetBundle资源加载
    }
    public T GetSceneAssetCache<T>(string rName) where T : Object
    {
        string rPath = "Assets/AssetPackages" + rName;
        object rTarget = AssetDatabase.LoadAssetAtPath<T>(rPath);
        return rTarget as T;
    }

    private ResourceMgr()
    {

    }
}
