using System.Collections;
using System.Collections.Generic;
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
}
