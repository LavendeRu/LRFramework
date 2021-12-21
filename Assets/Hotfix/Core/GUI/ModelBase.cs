using System.Threading.Tasks;
public class ModelBase
{
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
    public virtual async Task OnInitialize()
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
    {
    }
    public virtual void OnDestroy()
    {
    }
    public virtual void OnLogin()
    {
    }
    public virtual void OnLogout()
    {
    }
    public virtual void OnReLogin()
    {
    }
}

