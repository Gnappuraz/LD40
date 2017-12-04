using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnScript : MonoBehaviour {

	public void LoadByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
