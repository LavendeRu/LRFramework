using System.Collections;
using UnityEngine;
//下面这行为了取消使用WWW的警告，Unity2018以后推荐使用UnityWebRequest，处于兼容性考虑Demo依然使用WWW
#pragma warning disable CS0618
namespace HotFix_Project
{
    public class GameLanch : MonoSingleton<GameLanch>
    {
        private void Awake()
        {
            //初始化游戏框架：资源管理器
            this.gameObject.AddComponent<ResourceMgr>();
            this.gameObject.AddComponent<ILRuntimeManager>();

            this.StartCoroutine(this.CheckHotfixUpdate());
        }

        private IEnumerator CheckHotfixUpdate()
        {
            //从服务器上下载热更ab包或者热更逻辑的代码到私有目录

#if UNITY_ANDROID
        WWW www = new WWW(Application.streamingAssetsPath + "/Hotfix/HotFix_Project.dll");
#else
            WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/Hotfix/HotFix_Project.dll");
#endif
            while (!www.isDone)
                yield return null;
            if (!string.IsNullOrEmpty(www.error))
                UnityEngine.Debug.LogError(www.error);
            byte[] dll = www.bytes;
            www.Dispose();

#if UNITY_ANDROID
        www = new WWW(Application.streamingAssetsPath + "/Hotfix/HotFix_Project.pdb");
#else
            www = new WWW("file:///" + Application.streamingAssetsPath + "/Hotfix/HotFix_Project.pdb");
#endif
            while (!www.isDone)
                yield return null;
            if (!string.IsNullOrEmpty(www.error))
                UnityEngine.Debug.LogError(www.error);
            byte[] pdb = www.bytes;
            www.Dispose();

            ILRuntimeManager.Instance.LoadHotFixAssembly(dll,pdb);
            ILRuntimeManager.Instance.EnterGame();
            yield break;
        }

    }
}


