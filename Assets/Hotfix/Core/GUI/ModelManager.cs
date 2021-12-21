using System.Collections.Generic;
using System.Threading.Tasks;

public class ModelManager : TSingleton<ModelManager>
{
    private List<ModelBase> mModelList { get; } = new List<ModelBase>();

    public LoginModel Login { get; } = new LoginModel();
    private ModelManager()
    {
        this.mModelList.Add(this.Login);
    }


    public async Task Initialize()
    {
        for (int i = 0; i < this.mModelList.Count; i++)
        {
            await this.mModelList[i].OnInitialize();
        }
    }


    public void OnDestroy()
    {
        for (int i = 0; i < this.mModelList.Count; i++)
        {
            this.mModelList[i].OnDestroy();
        }
    }
    public void OnLoginIn()
    {
        for (int i = 0; i < this.mModelList.Count; i++)
        {
            this.mModelList[i].OnLogin();
        }
    }


    public void OnLoginOut()
    {
        for (int i = 0; i < this.mModelList.Count; i++)
        {
            this.mModelList[i].OnLogout();
        }
    }

    public void OnReLoginIn()
    {
        for (int i = 0; i < this.mModelList.Count; i++)
        {
            this.mModelList[i].OnReLogin();
        }
    }

}


