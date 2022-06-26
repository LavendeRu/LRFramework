using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class UIGameConfig : TSingleton<UIGameConfig>
{
    public class UIConfig
    {
        public string UIGUID { get; set; }

        public string UIType { get; set; }
    }

    private Dictionary<string, UIConfig> UIConfigMap ;

    public UIConfig GetUIConfig(string rUIName)
    {
        return this.UIConfigMap[rUIName];
    }
    private UIGameConfig()
    {
        this.ParseUIPanelTypeJson();
    }

    //解析json文件
    private void ParseUIPanelTypeJson()
    {
        this.UIConfigMap = new Dictionary<string, UIConfig>();
        TextAsset rGameUIConfig = ResourceManager.Instance.GetConfigAssetCache<TextAsset>("GameUI");
        this.UIConfigMap = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, UIConfig>>(rGameUIConfig.text);
    }
}

