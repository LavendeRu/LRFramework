using System;
using UnityEngine;
/// <summary>
/// 场景管理器
/// </summary>
public class SceneManager : ModuleBase
{
    private static SceneBase mCurScene;

    #region API

    public static void ChangeScene(SceneID rSeneID)
    {
        var rLastSceneID = GetSceneID();
        if(rLastSceneID == rSeneID)
        {
            GameLog.Log("[ChangeScene]current scene is the target... sceneID:" + rSeneID.ToString());
            return;
        }

        if(IsLoading())
        {
            GameLog.Log("[ChangeScene]current scene is loading....:" + rLastSceneID.ToString() + rSeneID.ToString());
            return;
        }
        //退出场景
        ExitLastScene();

        //新场景的加载
        EnterCurrentScene(rSeneID);
    }

    #endregion

    public override void Update(float fTime) 
    {

    }

    private static SceneID GetSceneID()
    {
        if(SceneManager.mCurScene == null)
            return SceneID.None;

        return SceneManager.mCurScene.SceneID;
    }

    private static bool IsLoading()
    {
        if(SceneManager.mCurScene == null)
            return false;

        return SceneManager.mCurScene.IsLoading();
    }

    private static SceneBase CreateScene(SceneID rSceneID)
    {
        SceneBase rTempScene;

        SceneConfig sceneConfig;
        GameSceneConfig.SceneMap.TryGetValue(rSceneID, out sceneConfig);
        Type sceneClass = sceneConfig.viewClass;

        // 反射拿到实例
        rTempScene = Activator.CreateInstance(sceneClass) as SceneBase;
        if(rTempScene != null)
        {
            rTempScene.SceneID = rSceneID;
            rTempScene.AssetPath = sceneConfig.path;
            rTempScene.PanelName = sceneConfig.name;
            return rTempScene;
        }

        return null;
    }
    
    private static void ExitLastScene()
    {
        //卸载上一个场景，
        if(SceneManager.mCurScene != null)
            SceneManager.mCurScene.OnExit();

        //调用ui模块的退出场景，
        ViewManager.SceneExit();
        //清理内存
        Resources.UnloadUnusedAssets();
    }

    private static void EnterCurrentScene(SceneID rSceneID)
    {
        var scene = CreateScene(rSceneID);
        // 异步加载场景
        scene.Load(OnSceneLoaded);

        SceneManager.mCurScene = scene;
    }

    private static void OnSceneLoaded(AssetRequest rAssetRequest)
    {
        //调用ui模块的进入场景
        ViewManager.SceneEnter();
        // 当前场景OnEnter
        SceneManager.mCurScene.OnEnter();
        //发送场景切换事件
    }
}