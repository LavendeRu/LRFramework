using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public sealed class ResourceManager : ModuleBase
{
    #region Instance

    static ResourceManager mInstance;
    public static ResourceManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = ModuleManager.Instance.Get<ResourceManager>();
            }

            return mInstance;
        }
    }

    #endregion
    private static string UIPathPrefix = "Assets/AssetPackages/GUI/Prefabs/";
    private static string ConfigPathPrefix = "Assets/AssetPackages/Configs/";
    private static string ScenePathPrefix = "Assets/AssetPackages/Scenes/";
    public T GetUIAssetCache<T>(string rName) where T : Object
    {
#if UNITY_EDITIR
        string rPath = UIPathPrefix + rName;
        object rTarget = AssetDatabase.LoadAssetAtPath<T>(rPath);
        return rTarget as T;
#endif

        var rUIAssetData = LoadManager.LoadPrefab(rName);
        return rUIAssetData as T;
    }
    public T GetSceneAssetCache<T>(string rName) where T : Object
    {
#if UNITY_EDITIR
        string rPath =  ScenePathPrefix + rName;
        object rTarget = AssetDatabase.LoadAssetAtPath<T>(rPath);
        return rTarget as T;
#endif

        var rSceneData = LoadManager.LoadScene(rName);
        return rSceneData as T;
    }

    public T GetConfigAssetCache<T>(string rName) where T : Object
    {
#if UNITY_EDITIR
        string rPath = ConfigPathPrefix + rName;
        object rTarget = AssetDatabase.LoadAssetAtPath<T>(rPath);
        return rTarget as T;
#endif
        var rJsonData = LoadManager.LoadConfig(rName);
        return rJsonData as T;
    }
}
