using System.Collections.Generic;
using UnityEngine;

public class ModuleManager : TSingleton<ModuleManager>
{
	private bool mIsInit = false;
	private bool mIsRunning = false;

	List<ModuleBase> mModules = new List<ModuleBase>();

    public ModuleManager()
    {
		Add<GameLogic>();
        Add<ViewManager>();
        Add<SceneManager>();

        Add<LoadManager>();
		Add<ResourceManager>();
    }

	public ModuleBase Get(string rName)
	{
		for(int i = 0; i < mModules.Count; i++)
		{
			if(mModules[i].Name == rName)
			{
				return mModules[i];
			}
		}
		GameLog.LogError($"[Module获取失败] {rName}: 未被注册.");
		return null;
	}

	public T Get<T>() where T : ModuleBase
	{
		return Get(typeof(T).Name) as T;
	}

	public void Add<T>() where T : ModuleBase, new()
    {
        T module = new T();
        mModules.Add(module);
    }

	public void Init()
	{
		if(mIsInit)
			return;

		for(int i = 0; i < this.mModules.Count; ++i)
			this.mModules[i].Init();
	}

	public bool IsAllInit()
	{
		for(int i = 0; i < this.mModules.Count; ++i)
		{
			if(!this.mModules[i].IsInit)
				return false;
		}
		mIsInit = true;
		return true;
	}

	public void UnInit()
	{
		this.mIsRunning = false;
		this.mIsInit = false;
		for(int i = this.mModules.Count - 1; i >= 0; --i)
			this.mModules[i].UnInit();
	}

	public void Update()
	{
		if(!this.mIsInit)
			return;
		for(int i = 0; i < this.mModules.Count; ++i)
		{
			mModules[i].Update(Time.deltaTime);
		}

		if(!this.mIsRunning)
			return;
	}

	public void StartGame()
	{
		this.mIsRunning = true;
		for(int i = 0; i < this.mModules.Count; ++i)
		{
			this.mModules[i].StartGame();
		}
	}
}