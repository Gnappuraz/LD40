using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour {

    public float speed;
    public GameObject basicComponent;
    public GameObject[] components;

    private List<GameObject> activeComponents;
    private List<GameObject> notActiveComponents;

	// Use this for initialization
	public void LevelSetup () {
        int index;
        GameObject instance;
        GameObject toInstantiate;

        instance = Instantiate(basicComponent, new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;

        activeComponents.Add(basicComponent);

        index = Random.Range(0, activeComponents.Count - 1);
        activeComponents.Add(notActiveComponents[index]);
        notActiveComponents.RemoveAt(index);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
