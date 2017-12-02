using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenLimit : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
				
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "ground")
		{
			//TODO remove
			Debug.Log("create new ground");
			GameManager.instance.InstanceNewGround();	
		} else if (other.gameObject.tag == "player")
		{
			//TODO remove
			Debug.Log("Player out of screen");
			GameManager.instance.PlayerKilled();
		}
		
	}
}
