using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
namespace HotFix_Project
{
    public class GameLanch : MonoSingleton<GameLanch>
    {
        private string mDllUrl = Application.streamingAssetsPath + "/Hotfix/HotFix_Project.dll";

        private string mPdbUrl = Application.streamingAssetsPath + "/Hotfix/HotFix_Project.pdb";
        private void Awake()
        {
            //初始化游戏框架

            //资源管理器
            this.gameObject.AddComponent<ResourceMgr>();

            //ILRuntime管理器
            this.gameObject.AddComponent<ILRuntimeManager>();

            this.StartCoroutine(this.CheckHotfixUpdate());
        }

        private IEnumerator CheckHotfixUpdate()
        {
            //从服务器上下载热更ab包或者热更逻辑的代码到私有目录

#if UNITY_ANDROID
        UnityWebRequest  rRequest = UnityWebRequest.Get(this.mDllUrl);
#else
            UnityWebRequest rRequest = UnityWebRequest.Get("file:///" + this.mDllUrl);
#endif
            rRequest.SendWebRequest();
            while (!rRequest.downloadHandler.isDone)
                yield return null;
            if (rRequest.isHttpError || rRequest.isNetworkError)
            {
                Debug.Log(rRequest.error);
            }
            byte[] dll = rRequest.downloadHandler.data;
            rRequest.Dispose();

#if UNITY_ANDROID
        UnityWebRequest  rRequestPdb = UnityWebRequest.Get(this.mPdbUrl);
#else
            UnityWebRequest rRequestPdb = UnityWebRequest.Get("file:///" + this.mPdbUrl);
#endif
            rRequestPdb.SendWebRequest();
            while (!rRequestPdb.downloadHandler.isDone)
                yield return null;
            if (rRequestPdb.isHttpError || rRequestPdb.isNetworkError)
            {
                Debug.Log(rRequestPdb.error);
            }
            byte[] pdb = rRequestPdb.downloadHandler.data;
            rRequestPdb.Dispose();

            ILRuntimeManager.Instance.LoadHotFixAssembly(dll, pdb);
            ILRuntimeManager.Instance.EnterGame();
            yield break;
        }

    }
}


