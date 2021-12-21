using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIRoot : MonoSingleton<UIRoot>
{

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

    public Camera UICamera;

    public EventSystem UIEventSystem;
    private UIRoot()
    {

    }
}


