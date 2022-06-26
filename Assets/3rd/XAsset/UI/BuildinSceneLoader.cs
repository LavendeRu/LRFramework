using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildinSceneLoader : MonoBehaviour {

    public void Load(int sceneIndex)
	{
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex);
	}
}
