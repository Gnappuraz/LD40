using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PagineNavigator : MonoBehaviour {

	public void LoadNext()
	{
		switch (GameStatus.GetCurrentLevel())
		{
			case 0: SceneManager.LoadScene("screen_2_old_ghog");
				break;
			case 1: SceneManager.LoadScene("main");
				break;
			case 2: SceneManager.LoadScene("main");
				break;
			case 3: SceneManager.LoadScene("screen_theend");
				break;
		}
	}
}
