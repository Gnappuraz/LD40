﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDestroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		//TODO remove
		Debug.Log("Destroy component");
		GameManager.instance.DestroyGround(other);
	}
}