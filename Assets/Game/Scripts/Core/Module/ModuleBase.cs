public class ModuleBase
{
    public ModuleBase() { }

    private string mName;
    public string Name
    {
        get
        {
            if(string.IsNullOrEmpty(this.mName))
                this.mName = GetType().Name;
            return this.mName;
        }
    }
    protected bool mIsInit = false;
    public bool IsInit { get { return mIsInit; } }

    public virtual void Init() { mIsInit = true; }
    public virtual void UnInit() { mIsInit = false; }
    public virtual void StartGame() { }
    public virtual void Update(float fTime) { }
    public virtual void Dispose() { }
}