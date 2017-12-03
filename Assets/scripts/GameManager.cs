using System;
using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using UnityEditor;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public float speed;
    
    private List<GameObject> activeComponents;
    private List<GameObject> notActiveComponents;
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

        activeComponents = new List<GameObject>();
        notActiveComponents = new List<GameObject>();

        //do not instantiate at 0 or there is not gonna be a OntriggerEnter2D for the screenLimitCollider
        instance = spawnTerrain(-1);

        currentDifficulty = 0;
        speed = speedDiffModifiers[currentDifficulty];

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
        speed = speedDiffModifiers[currentDifficulty];
    }

    public void PlayerHitLetter(string letter)
    {
        //Send letter to manager (update letter shown)
        //is level completed? go to next level
        // is not then increase difficulty (speed and difficulty set) 
    }
}