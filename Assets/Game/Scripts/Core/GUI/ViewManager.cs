﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI窗体(位置)类型
/// </summary>
public enum ViewOpenType
{
    /// <summary>
    /// 全屏
    /// </summary>
    FullPage,
    /// <summary>
    /// 固定窗体
    /// </summary>
    Fixed,
    /// <summary>
    /// 弹出窗体
    /// </summary>
    PopUp,
    /// <summary>
    /// 全局窗体
    /// </summary>
    Global,
}


public enum ViewLayer
{
    UI,

    UITop,

}

/// <summary>
/// UI管理器
/// </summary>
public sealed class ViewManager : ModuleBase
{
    #region Instance

    static ViewManager mInstance;
    public static ViewManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = ModuleManager.Instance.Get<ViewManager>();
            }

            return mInstance;
        }
    }

    #endregion
    /// <summary>
    /// 存放各种全屏显示节点
    /// </summary>
    public GameObject PageRoot;

    /// <summary>
    /// 存放各种固定显示
    /// </summary>
    public GameObject FixedRoot;

    /// <summary>
    /// 存放各种弹出框节点
    /// </summary>
    public GameObject PopupRoot;

    /// <summary>
    /// 存放各种全局显示的节点
    /// </summary>
    public GameObject GlobalsRoot;
    /// <summary>
    /// 当前的UI中的Views，每个View是用GUID来作唯一标识
    /// </summary>
    private Dictionary<string, ViewBase> mCurPageViews;
    public Dictionary<string, ViewBase> CurPageViews { get { return this.mCurPageViews; } }

    private Dictionary<string, ViewBase> mCurPopupViews;
    public Dictionary<string, ViewBase> CurPopupViews { get { return this.mCurPopupViews; } }
    private Dictionary<string, ViewBase> mCurFixedViews;
    public Dictionary<string, ViewBase> CurFixedViews { get { return this.mCurFixedViews; } }

    private Dictionary<string, ViewBase> mCurGlobalViews;
    public Dictionary<string, ViewBase> CurGlobalViews { get { return this.mCurGlobalViews; } }

    /// <summary>
    /// 打开UI
    /// </summary>
    /// <param name="rViewName"></param>

    public void Open(string rViewName)
    {
        var rUIConfig = UIGameConfig.Instance.GetUIConfig(rViewName);
        if (!Enum.TryParse(rUIConfig.UIType, out ViewOpenType rViewOpenType))
        {
            return;

        }

        var rViewBase = this.GetView(rViewName, rViewOpenType);

        if (rViewBase == null)
        {
            Debug.LogError("GetView Error");
            return;
        }

        switch (rViewOpenType)
        {
            case ViewOpenType.FullPage:

                this.mCurPageViews.Add(rViewBase.GUID, rViewBase);
                break;
            case ViewOpenType.Fixed:
                this.mCurFixedViews.Add(rViewBase.GUID, rViewBase);
                break;
            case ViewOpenType.PopUp:
                this.mCurPopupViews.Add(rViewBase.GUID, rViewBase);
                break;
            case ViewOpenType.Global:
                this.mCurGlobalViews.Add(rViewBase.GUID, rViewBase);
                break;
            default:
                break;
        }

    }

    private ViewBase GetView(string rViewName, ViewOpenType rViewOpenType)
    {

        GameObject rPrefab = ResourceManager.Instance.GetUIAssetCache<GameObject>(rViewName);
        var rGo = GameObject.Instantiate(rPrefab);
        switch (rViewOpenType)
        {
            case ViewOpenType.FullPage:
                rGo.transform.SetParent(UIRoot.Instance.PageRoot.transform);
                break;
            case ViewOpenType.Fixed:
                rGo.transform.SetParent(UIRoot.Instance.FixedRoot.transform);
                break;
            case ViewOpenType.PopUp:
                rGo.transform.SetParent(UIRoot.Instance.PopupRoot.transform);
                break;
            case ViewOpenType.Global:
                rGo.transform.SetParent(UIRoot.Instance.GlobalsRoot.transform);
                break;
        }
        ViewBase rViewBase = rPrefab.GetComponent<ViewBase>();
        rViewBase.Initialize();
        return rViewBase;
    }

    /// <summary>
    /// 关闭UI
    /// </summary>
    /// <param name="rGUID"></param>
    /// <param name="rViewOpenType"></param>
    public void Close(string rGUID, ViewOpenType rViewOpenType)
    {
        Dictionary<string, ViewBase> rDict = null;
        switch (rViewOpenType)
        {
            case ViewOpenType.FullPage:
                rDict = this.mCurPageViews;
                break;
            case ViewOpenType.Fixed:
                rDict = this.mCurFixedViews;
                break;
            case ViewOpenType.PopUp:
                rDict = this.mCurPopupViews;
                break;
            case ViewOpenType.Global:
                rDict = this.mCurGlobalViews;
                break;
            default:
                break;
        }

        if (rDict.TryGetValue(rGUID, out var rView))
        {
            GameObject.DestroyImmediate(rView.gameObject);
            rDict.Remove(rGUID);

        }
    }

    public void ClosseAll()
    {
        //TODO
    }


    public static void SceneEnter()
    {

    }

    /// <summary>
    /// 切换场景，资源管理相关
    /// </summary>
    public static void SceneExit()
    {

    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public override void Init()
    {
    }

    public override void UnInit()
    {
    }

    public override void StartGame()
    {

    }

    public override void Update(float fTime)
    {
    }

    public override void Dispose()
    {

    }
}

