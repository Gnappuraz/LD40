using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationControllerScript : MonoBehaviour {
    
    bool permittedDialog = false;
    Animation animation;

    public Texture2D fadeOutTexture;
    public float fadeSpeed = 0.8f;

    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;

    public void PermitDialog()
    {
        DialogSceneManager.instance.PermitDialog();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("main");
    }
}
