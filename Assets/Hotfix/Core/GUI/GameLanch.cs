using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core

{
    public class GameLanch : MonoSingleton<GameLanch>
    {

        private void Awake()
        {
            //初始化游戏框架：资源管理，网络管理，日志管理...

            //资源管理
            this.gameObject.AddComponent<ResourceMgr>();

            //网络管理


            //日志管理


            this.gameObject.AddComponent<ILRuntimeManager>();
        }

        void Start()
        {

        }

        void Update()
        {

        }

        private GameLanch()
        {

        }
    }
}
