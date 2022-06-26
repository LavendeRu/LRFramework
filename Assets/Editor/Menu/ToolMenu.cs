using UnityEditor;

public class ToolMenu
{
    [MenuItem("Tools/导出AssetBundle", false, 1)]
    static void ExportBundle()
    {
        ExportAssetBundle.BuildBundle();
    }
}

