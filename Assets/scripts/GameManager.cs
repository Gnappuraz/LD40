using System;
using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    [SerializeField] private LetterManager letterManager;

    public float speed;
    public float targetSpeed;
    private float startAccTime;
    [SerializeField] private float acceleration;
    
    private BoxCollider2D lastSpawned;

    private Dictionary<int, List<string>> levelWords;

    [SerializeField] private GameObject startingTerrain;
    [SerializeField] private List<GameObject> level0;
    [SerializeField] private List<GameObject> level1;
    [SerializeField] private List<GameObject> level2;
    [SerializeField] private List<GameObject> level3;
    [SerializeField] private List<float> speedDiffModifiers;

    [SerializeField] private Transform terrainHolder; 
    
    List<List<GameObject>> difficultyObjects = new List<List<GameObject>>();

    private int currentDifficulty;
    public int currentLevel = 0;
    public int startingLevel = 0;
    
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        LevelWordParser wordParser = new LevelWordParser();
        levelWords = wordParser.getWordDictionary();
        
        difficultyObjects.Add(level0);
        difficultyObjects.Add(level1);
        difficultyObjects.Add(level2);
        difficultyObjects.Add(level3);
        
        InitGame();
    }

    void InitGame()
    {
        GameObject instance;

        //do not instantiate at 0 or there is not gonna be a OntriggerEnter2D for the screenLimitCollider
        instance = spawnTerrain(-1);

        currentDifficulty = 0;
        speed = 0;
        targetSpeed = speedDiffModifiers[currentDifficulty];
        
        
        letterManager.SetCurrentWords(levelWords[currentLevel]);

        //activeComponents.Add(startingTerrain);
    }

    private GameObject spawnTerrain(int difficulty)
    {
        //TODO
        //Pick from elements with same difficulty
        //Check pool for object with same difficulty
        //if there is one use it otherwise instantiate it
        
        //Debug.Log(lastSpawned.gameObject.transform.position.x + lastSpawned.size.x*2);
        
        //Get current level
        //Get random object from current level

        GameObject instance = null;
        if (difficulty == -1)
        {
            instance = Instantiate(startingTerrain, new Vector3(), Quaternion.identity, terrainHolder) as GameObject;
            instance.transform.localPosition =
                new Vector3(0, 0, 1f);
        }
        else
        {
            instance = Instantiate(startingTerrain, new Vector3(), Quaternion.identity, terrainHolder) as GameObject;
            instance.transform.localPosition =
                new Vector3(lastSpawned.gameObject.transform.position.x + lastSpawned.size.x, 0, 1f);
        }

        lastSpawned = instance.GetComponent<BoxCollider2D>();
        return instance;
    }

    void Update()
    {
        moveUpToSpeed();
    }

    public void PlayerKilled()
    {
        
    }

    public void InstanceNewGround()
    {
        spawnTerrain(0);   
    }

    public void DestroyGround(Collider2D other)
    {
        Destroy(other.gameObject);
    }

    private void goNextDifficulty()
    {
        currentDifficulty++;
        targetSpeed = speedDiffModifiers[currentDifficulty];
    }

    public void PlayerHitLetter(string letter)
    {
        letterManager.HitLetter(letter);

        if (letterManager.IsLevelWordlistComplete())
        {
            targetSpeed = 0;
            //GO to next level
        }
        
        
        //Send letter to manager (update letter shown)
        //is level completed? go to next level
        // is not then increase difficulty (speed and difficulty set) 
    }

    private void moveUpToSpeed()
    {
        if (targetSpeed == speed)
        {
            return;
        }

        if (startAccTime == 0f)
        {
             startAccTime = Time.time;   
        }
       
        if (Math.Abs(speed - targetSpeed) > 0.001f)
        {
            speed = Mathf.SmoothStep(speed, targetSpeed, (Time.time - startAccTime)/acceleration);
        }
        else
        {
            targetSpeed = speed;
            startAccTime = 0;
        }
    }
}