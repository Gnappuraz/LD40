using System;
using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] private LetterManager letterManager;
    [SerializeField] private GameObject player;

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
    [SerializeField] private List<GameObject> level4;
    [SerializeField] private List<GameObject> level5;
    [SerializeField] private List<float> speedDiffModifiers;

    [SerializeField] private Transform terrainHolder;

    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator bgAnimator;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitLetterClip;
    [SerializeField] private AudioClip deathclip;
    
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

        //DontDestroyOnLoad(gameObject);

        LevelWordParser wordParser = new LevelWordParser();
        levelWords = wordParser.getWordDictionary();
        
        difficultyObjects.Add(level0);
        difficultyObjects.Add(level1);
        difficultyObjects.Add(level2);
        difficultyObjects.Add(level3);
        difficultyObjects.Add(level4);
        difficultyObjects.Add(level5);
    }

    void Start()
    {
        InitGame();   
    }

    void InitGame()
    {
        currentLevel = GameStatus.GetCurrentLevel();
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
        else {
            List<GameObject> pool = difficultyObjects[difficulty];
            GameObject toInstantiate = pool[Random.Range(0, pool.Count)];

            instance = Instantiate(toInstantiate, new Vector3(), toInstantiate.transform.rotation, terrainHolder) as GameObject;
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
        
        if(selected == null) return;

        string nextLetter = letterManager.GetNextLetter();
        if (nextLetter != null)
        {
            selected.SetLetter(nextLetter);    
        }
    }

    void Update()
    {
        MoveUpToSpeed();
    }

    public void PlayerKilled()
    {
        audioSource.PlayOneShot(deathclip);
        targetSpeed = 0;
        Destroy(player);
        
        bgAnimator.SetTrigger("death");
    }

    public void InstanceNewGround()
    {
        spawnTerrain(currentDifficulty);   
    }

    public void DestroyGround(Collider2D other)
    {
        Destroy(other.gameObject);
    }

    private void GoNextDifficulty()
    {
       
        if(currentDifficulty < 5) currentDifficulty++;
        
        targetSpeed = speedDiffModifiers[currentDifficulty];
    }

    public void PlayerHitLetter(string letter)
    {
        audioSource.PlayOneShot(hitLetterClip);
        letterManager.HitLetter(letter);

        if (letterManager.IsLevelWordlistComplete())
        {
            targetSpeed = 0;
            playerAnimator.SetTrigger("stop");

            bgAnimator.SetTrigger("nextLvl");

        } else
        {
            GoNextDifficulty();
        }
    }
    
    public void LoadNextLevel()
    {
        GameStatus.incLevel();
        SceneManager.LoadScene("screen_1_lettera");
    }
    
    public void LoadSameLevel()
    {
        SceneManager.LoadScene("screen_fail");
    }

    private void MoveUpToSpeed()
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