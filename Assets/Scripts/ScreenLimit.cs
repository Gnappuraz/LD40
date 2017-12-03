using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLimit : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("ground"))
		{
			GameManager.instance.InstanceNewGround();	
		} else if (other.gameObject.CompareTag("player"))
		{
			GameManager.instance.PlayerKilled();
		}
	}
}
