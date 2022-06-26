using System;
using System.Collections.Generic;
using UnityEngine;

public class ViewBase : MonoBehaviour
{
    /// <summary>
    /// View唯一标识
    /// </summary>
    private string mGUID;
    public string GUID { get { return this.mGUID; } }


    public virtual void OnOpen()
    {
    }

    public virtual void OnPause()
    {
    }

    public virtual void OnResume()
    {
    }

    public virtual void OnExit()
    {
    }


    public void Initialize()
    {
        this.mGUID = Guid.NewGuid().ToString();
    }
}
