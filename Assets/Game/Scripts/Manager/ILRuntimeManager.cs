using UnityEngine;
using System.Collections;
using System.IO;
using ILRuntime.Runtime.Enviorment;
public class ILRuntimeManager : MonoSingleton<ILRuntimeManager>
{
    AppDomain mAppDomain;

    System.IO.MemoryStream fs;
    System.IO.MemoryStream p;

    private bool mIsGameStart = false;
    private void Awake()
    {
        this.InitILRuntime();
    }
    public void InitILRuntime()
    {
        this.mAppDomain = new AppDomain();
    }

    public void LoadHotFixAssembly(byte[] dll, byte[] pdb)
    {
        this.fs = new MemoryStream(dll);
        this.p = new MemoryStream(pdb);

        try
        {
            this.mAppDomain.LoadAssembly(fs, p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
        }
        catch
        {
            Debug.LogError("加载热更DLL失败，请确保已经编译过热更DLL");
            return;
        }
        this.InitializeILRuntime();

    }

    private void InitializeILRuntime()
    {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
        //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
        this.mAppDomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
        //这里做一些ILRuntime的注册
    }

    public void EnterGame()
    {
        this.mIsGameStart = true;

        this.mAppDomain.Invoke("HotFix_Project.HotfixLogic", "Init", null, null);

    }

    public void Update()
    {
        if (!this.mIsGameStart) return;
        this.mAppDomain.Invoke("HotFix_Project.HotfixLogic", "Update", null, null);
    }

    public void FixedUpdate()
    {
        if (!this.mIsGameStart) return;
        this.mAppDomain.Invoke("HotFix_Project.HotfixLogic", "FixedUpdate", null, null);
    }

    public void LateUpdate()
    {
        if (!this.mIsGameStart) return;
        this.mAppDomain.Invoke("HotFix_Project.HotfixLogic", "LateUpdate", null, null);
    }
}


