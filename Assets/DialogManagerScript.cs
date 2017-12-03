using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManagerScript : MonoBehaviour
{

    public static DialogManagerScript instance = null;

    public string[] frasi_anna = {"Hi Granpa, how are you today ?",
                            "Do you remember me? I’m Anna",
                            "Have you see the sky yesterday night?",
                            "So many stars, the most incredible Starry Night!",
                            "I think that you have to renovate your bedroom",
                            "Maybe you can buy a more comfortable chair",
                            "This afternoon i’m going to pick up the keys",
                            "Finally I’ll have a house only for me",
                            "Finally I’ll be just me",
                            "Granpa, are you alright?",
                            "Granpa?",
                            "No, you’re not alone. I’m here and this nursing home, is beautiful",
                            "Granpa..." };

    public string[] frasi_nonno = {"Mhmm… Mhmm...",
                            "Mhmm… Mhmm...",
                            "Mhmm… Mhmm...",
                            "Mhmm… Mhmm...",
                            "Mhmm… Mhmm...",
                            "Mhmm… Mhmm...",
                            "Mhmm… Mhmm...",
                            "Mhmm… Mhmm...",
                            "...",
                            "...",
                            "Alone…",
                            "Alone…",
                            ""};

    public int index = -1;

    public Text textAnna;
    public Text textNonno;
    public Button dialogButton;
    bool permittedDialog = false;

    // Use this for initialization
    void Start()
    {
        //textAnna = GameObject.Find("TextAnna").GetComponent<Text>();
        //textNonno = GameObject.Find("TextNonno").GetComponent<Text>();
        dialogButton = GameObject.Find("Button").GetComponent<Button>();

        dialogButton.onClick.AddListener(GoAhead);
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

            //textAnna.text = frasi_anna[index];
            //textNonno.text = frasi_nonno[index];
        }
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PermitDialog()
    {
        permittedDialog = true;

        GoAhead();
    }
}
