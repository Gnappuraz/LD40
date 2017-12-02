using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    public LevelManager levelManager;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        levelManager = GetComponent<LevelManager>();

        InitGame();
    }

    void InitGame()
    {
        levelManager.LevelSetup();
    }

    void Update()
    {

    }
}