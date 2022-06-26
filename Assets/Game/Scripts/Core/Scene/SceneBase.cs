using System;

public abstract class SceneBase
{
    public string PanelName;
    public string AssetPath;
    public SceneID SceneID { get; set; }
    public LoadState LoadState { get; set; }
    private Action<AssetRequest> mLoadedCallback;
    private AssetRequest mAssetRequest;

    public bool IsLoading()
    {
        return this.LoadState == LoadState.LOADING;
    }

    public void Dispose()
    {
        if(this.mAssetRequest != null)
            LoadManager.UnloadAsset(this.mAssetRequest);
    }

    public void Load(Action<AssetRequest> rLoadedCallback = null)
    {
        this.mLoadedCallback = rLoadedCallback;
        this.LoadState = LoadState.LOADING;
        LoadManager.LoadSceneAsync(this.AssetPath, false, this.OnLoadCompleted);
    }

    private void OnLoadCompleted(AssetRequest rAssetRequest)
    {
        this.mAssetRequest = rAssetRequest;
        if(!string.IsNullOrEmpty(rAssetRequest.error))
        {
            rAssetRequest.Release();
            GameLog.LogError("加载场景失败:" + this.PanelName);
            return;
        }

        this.LoadState = LoadState.LOADED;

        if(this.mLoadedCallback != null)
            this.mLoadedCallback.Invoke(rAssetRequest);
    }
    protected abstract void OnLoaded();
    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void OnUpdate();
}