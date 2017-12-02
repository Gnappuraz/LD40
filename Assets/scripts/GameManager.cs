using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public float speed;
    [SerializeField] private GameObject basicComponent;
    [SerializeField] public GameObject[] components;

    public Transform objectSpawn;
    
    private List<GameObject> activeComponents;
    private List<GameObject> notActiveComponents;
    private BoxCollider2D lastSpawned;

    private int difficulty;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        InitGame();
    }

    void InitGame()
    {
        GameObject instance;

        activeComponents = new List<GameObject>();
        notActiveComponents = new List<GameObject>();

        //do not instantiate at 0 or there is not gonna be a OntriggerEnter2D for the screenLimitCollider
        instance = Instantiate(basicComponent, new Vector3(1.0f, 0, 1f), Quaternion.identity) as GameObject;
        lastSpawned = instance.GetComponent<BoxCollider2D>();

        activeComponents.Add(basicComponent);
    }

    private void spawnTerrain(int difficulty)
    {
        //Pick from elements with same difficulty
        //Check pool for object with same difficulty
        //if there is one use it otherwise instantiate it
        
        Debug.Log(lastSpawned.size.x);
        GameObject instance = Instantiate(basicComponent, new Vector3(lastSpawned.size.x, 0, 1f), Quaternion.identity) as GameObject;
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
}