using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSceneManager : MonoBehaviour
{

    public static DialogSceneManager instance = null;

    public string[] frasi_anna;
    public string[] frasi_nonno;

    public int index = -1;

    public Text textAnna;
    public Text textNonno;
    public Button dialogButton;
    bool permittedDialog = false;
    Animation animation;

    public Texture2D fadeOutTexture;
    public float fadeSpeed = 0.8f;

    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;

    // Use this for initialization
    void Start()
    {

        dialogButton.onClick.AddListener(GoAhead);

        frasi_anna = new string[] {"\"Hi Granpa, how are you today?\"",
                            "\"Do you remember me? I’m Anna...\"",
                            "\"Have you see the sky yesterday night?\"",
                            "\"So many stars, the most incredible Starry Night!\"",
                            "\"I think that you have to renovate your bedroom.\"",
                            "\"Maybe you can buy a more comfortable chair.\"",
                            "\"This afternoon I’m going to pick up the keys.\"",
                            "\"Finally I’ll have a house only for me!\"",
                            "\"Finally I’ll be just me.\"",
                            "\"Granpa, are you alright?\"",
                            "\"Granpa?\"",
                            "\"No, you’re not alone. I’m here as always and, this nursing home, is beautiful...\"",
                            "\"Granpa...\"" };

        frasi_nonno = new string[] {"\"Mhmm… Mhmm...\"",
                            "\"Mhmm… Mhmm...\"",
                            "\"Mhm...\"",
                            "\"Mhmm… Mhmm...\"",
                            "\"Mhm...\"",
                            "\"Mhmm… Mhmm...\"",
                            "\"Mhm...\"",
                            "\"Mhmm… Mhmm...\"",
                            "\"...\"",
                            "\"... ...\"",
                            "\"Alone?\"",
                            "\"Alone...\"",
                            ""};
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GoAhead();
        }
    }

    public void GoAhead()
    {
        if (index < frasi_anna.Length - 1 && permittedDialog)
        {
            index++;

            textAnna.text = frasi_anna[index];
            textNonno.text = frasi_nonno[index];
        }

        if (index == frasi_anna.Length - 1)
            GameObject.Find("sfondo").GetComponent<Animator>().SetBool("end", true);
        
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void PermitDialog()
    {
        permittedDialog = true;

        GoAhead();
    }
}
