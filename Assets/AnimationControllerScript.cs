using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationControllerScript : MonoBehaviour {

	public void PermitDialog()
    {
        DialogSceneManager.instance.PermitDialog();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("main");
    }
}
