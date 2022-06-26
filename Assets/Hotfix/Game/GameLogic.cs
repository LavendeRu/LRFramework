using UnityEngine;

public sealed class GameLogic : ModuleBase
{
    #region Instance

    static GameLogic mInstance;
    public static GameLogic Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = ModuleManager.Instance.Get<GameLogic>();
            }

            return mInstance;
        }
    }

    #endregion
    public override void Init()
    {
        this.EnterScene();
    }
    private void EnterScene()
    {
        //显示场景
        Debug.Log("EnterScene--显示场景");
        //SceneManager.ChangeScene("MainCity");


    }
}
