using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDestroyer : MonoBehaviour {


	private void OnTriggerEnter2D(Collider2D other)
	{
		GameManager.instance.DestroyGround(other);
	}
}
