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

    [SerializeField] private Animator playerAnimator;
    
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
    }

    void Start()
    {
        InitGame();   
    }

    void InitGame()
    {
        GameObject instance;

        currentDifficulty = 0;
        speed = 0;
        targetSpeed = speedDiffModifiers[currentDifficulty];
          
        letterManager.SetCurrentWords(levelWords[currentLevel]);
        
        //do not instantiate at 0 or there is not gonna be a OntriggerEnter2D for the screenLimitCollider
        spawnTerrain(-1);
    }

    private GameObject spawnTerrain(int difficulty)
    {

        GameObject instance = null;
        if (difficulty == -1)
        {
            instance = Instantiate(startingTerrain, new Vector3(), Quaternion.identity, terrainHolder) as GameObject;
            instance.transform.localPosition = new Vector3(0, 0, 1f);
        }
        else
        {
            instance = Instantiate(startingTerrain, new Vector3(), Quaternion.identity, terrainHolder) as GameObject;
            instance.transform.localPosition = new Vector3(lastSpawned.gameObject.transform.position.x + lastSpawned.size.x, 0, 1f);
            enableLetter(instance.GetComponent<LevelSection>());
        }

        lastSpawned = instance.GetComponent<BoxCollider2D>();
        return instance;
    }

    private void enableLetter(LevelSection section)
    {
        int letters = section.letterPlaceholders.Count;
        int r = (int) (Time.deltaTime * 100);
        LetterController selected = section.letterPlaceholders[r % letters];

        string nextLetter = letterManager.GetNextLetter();
        if (nextLetter != null)
        {
            selected.SetLetter(nextLetter);    
        }
    }

    void Update()
    {
        moveUpToSpeed();
    }

    public void PlayerKilled()
    {
        targetSpeed = 0;
        //GO to kill screen
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
            playerAnimator.SetTrigger("stop");
            //GO to next level
        } else
        {
            //increase difficulty
        }
    }

    private void moveUpToSpeed()
    {
        if (Math.Abs(targetSpeed - speed) < 0.001)
        {
            speed = targetSpeed;
        }

        if (Math.Abs(startAccTime) < 0.001)
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