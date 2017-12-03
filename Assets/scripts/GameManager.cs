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
    
    List<List<GameObject>> levelsObjects = new List<List<GameObject>>();

    private int difficulty;
    
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        LevelWordParser wordParser = new LevelWordParser();
        levelWords = wordParser.getWordDictionary();
        
        levelsObjects.Add(level0);
        levelsObjects.Add(level1);
        levelsObjects.Add(level2);
        levelsObjects.Add(level3);
        
        InitGame();
    }

    void InitGame()
    {
        GameObject instance;

        activeComponents = new List<GameObject>();
        notActiveComponents = new List<GameObject>();

        //do not instantiate at 0 or there is not gonna be a OntriggerEnter2D for the screenLimitCollider
        instance = Instantiate(startingTerrain, new Vector3(1.0f, 0, 1f), Quaternion.identity) as GameObject;
        lastSpawned = instance.GetComponent<BoxCollider2D>();
        
        //activeComponents.Add(startingTerrain);
    }

    private void spawnTerrain(int difficulty)
    {
        //TODO
        //Pick from elements with same difficulty
        //Check pool for object with same difficulty
        //if there is one use it otherwise instantiate it
        
        Debug.Log(lastSpawned.gameObject.transform.position.x + lastSpawned.size.x*2);
        GameObject instance = Instantiate(startingTerrain, new Vector3(lastSpawned.gameObject.transform.position.x + lastSpawned.size.x, 0, 1f), Quaternion.identity) as GameObject;
        lastSpawned = instance.GetComponent<BoxCollider2D>();
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
}