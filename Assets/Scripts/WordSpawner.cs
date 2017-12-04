using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WordSpawner : MonoBehaviour
{

	[SerializeField] private List<GameObject> spawners;
	
	[SerializeField] private List<string> words;
	[SerializeField] private float timer;

	private float remainingTime;
	private Queue<GameObject> spawnPool = new Queue<GameObject>();
	private Queue<string> wordPool = new Queue<string>();
	
	void Start ()
	{
		remainingTime = timer;
	}
	
	void Update ()
	{

		remainingTime = remainingTime - Time.deltaTime;

		if (remainingTime < 0)
		{
			SpawnWord();
			remainingTime = timer;
		}
	}

	private void SpawnWord()
	{
		if (spawnPool.Count < 1)
		{
			spawnPool = new Queue<GameObject>(spawners.OrderBy(a => Guid.NewGuid()).ToList());
		}

		spawnPool.Dequeue().GetComponent<Text>().text = GetText();
		spawnPool.Dequeue().GetComponent<Animator>().SetTrigger("show");
	}

	private string GetText()
	{
		if (wordPool.Count < 1)
		{
			wordPool = new Queue<string>(words.OrderBy(a => Guid.NewGuid()).ToList());
		}

		return wordPool.Dequeue();
	}
}
