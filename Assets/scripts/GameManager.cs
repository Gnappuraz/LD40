using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public float speed;
    public GameObject basicComponent;
    public GameObject[] components;

    private List<GameObject> activeComponents;
    private List<GameObject> notActiveComponents;
    private GameObject headComponent;

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
        int index;
        GameObject instance;
        GameObject toInstantiate;

        activeComponents = new List<GameObject>();
        notActiveComponents = new List<GameObject>();

        instance = Instantiate(basicComponent, new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
        headComponent = instance;

        activeComponents.Add(basicComponent);

        index = Random.Range(0, notActiveComponents.Count - 1);
        //activeComponents.Add(notActiveComponents[index]);
        //notActiveComponents.RemoveAt(index);
    }

    void Update()
    {
        GameObject instance;
        GameObject toInstantiate;
        RectTransform rectTransform;

        if (headComponent.transform.position.x >= 0 )
        {
            rectTransform = (RectTransform)headComponent.transform;
            instance = Instantiate(basicComponent, new Vector3(rectTransform.rect.width, 0, 1f), Quaternion.identity) as GameObject;
            headComponent = instance;
        }
    }
}