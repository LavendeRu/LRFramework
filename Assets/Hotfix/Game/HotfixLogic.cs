using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class HotfixLogic
    {
        public static void Init()
        {
            Debug.Log("HotfixLogic Init");
            
            GameLogic.Instance.Init();
        }

        public static void Update()
        {
            //Debug.Log("HotfixLogic Update");
        }

        public static void LateUpdate()
        {
            //Debug.Log("HotfixLogic LateUpdate");
        }

        public static void FixedUpdate()
        {
            //Debug.Log("HotfixLogic FixedUpdate");
        }
    }

}


