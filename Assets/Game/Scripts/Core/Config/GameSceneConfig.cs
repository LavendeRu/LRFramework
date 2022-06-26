using System;
using System.Collections.Generic;

public struct SceneConfig
{
    public string name;
    public string path;
    public Type viewClass;

    public SceneConfig(string name, string path, Type viewClass)
    {
        this.name = name;
        this.path = path;
        this.viewClass = viewClass;
    }
}

public enum SceneID
{
    None,
    Test,
}

public static class GameSceneConfig
{
    public static Dictionary<SceneID, SceneConfig> SceneMap = new Dictionary<SceneID, SceneConfig>
    {
        {SceneID.Test,new SceneConfig("Test","Assets/AssetPackages/Scenes/Test.unity",typeof(TestScene)) },
    };
}