using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterController : MonoBehaviour
{
	[SerializeField] private Text text;
	private Collider2D collider;
	private bool done;

	void Awake ()
	{
		collider = GetComponent<Collider2D>();
		text.text = "";
		gameObject.SetActive(false);
	}

	public void SetLetter(string letter)
	{
		gameObject.SetActive(true);
		text.text = letter;
		done = false;
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("player") && !done)
		{
			done = true;
			gameObject.SetActive(false);
			GameManager.instance.PlayerHitLetter(text.text);
		}
	}
}
