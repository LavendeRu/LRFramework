using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class UtilTool
{
    public static void SetLayer(GameObject rObj, string rLayerName, bool bIsIncludeChildren = false)
    {
        int nLayer = LayerMask.NameToLayer(rLayerName);
        SetLayer(rObj, nLayer, bIsIncludeChildren);
    }

    public static void SetLayer(GameObject rObj, int nLayer, bool bIsIncludeChildren)
    {
        if (rObj)
        {
            rObj.layer = nLayer;
            if (bIsIncludeChildren)
            {
                int nChildNum = rObj.transform.childCount;
                for (int i = 0; i < nChildNum; i++)
                {
                    var rChildObj = rObj.transform.GetChild(i).gameObject;
                    SetLayer(rChildObj, nLayer, bIsIncludeChildren);
                }
            }
        }
    }


    private static StringBuilder m_stringBuilder = new StringBuilder();
    private static readonly long epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;

    //获取当前时间戳(秒)
    public static long GetCurrTimeStamp()
    {
        return (DateTime.UtcNow.Ticks - epoch) / 10000000;
    }

    // 当前时间戳（毫秒）
    public static long GetCurrMilliSeconds()
    {
        return (DateTime.UtcNow.Ticks - epoch) / 10000;
    }
}
