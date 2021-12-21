using Core;
using UnityEngine;

namespace Game

{
    public class GameLogic
    {
        public static GameLogic Instance = new GameLogic();

        public void Init()
        {
            this.EnterScene();
        }
        private void EnterScene()
        {
            //显示场景
            Debug.Log("EnterScene--显示场景");
            //GameObject rMapPrefab = ResourceMgr.Instance.GetSceneAssetCache<GameObject>("");
            //GameObject rMap = GameObject.Instantiate(rMapPrefab);
        }
    }
}
