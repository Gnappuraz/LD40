using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLimit : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
				
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("ground"))
		{
			//TODO remove
			Debug.Log("create new ground");
			GameManager.instance.InstanceNewGround();	
		} else if (other.gameObject.CompareTag("player"))
		{
			//TODO remove
			Debug.Log("Player out of screen");
			GameManager.instance.PlayerKilled();
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("ground"))
		{
			//TODO remove
			Debug.Log("create new ground");
			GameManager.instance.InstanceNewGround();
		}
	}
}
