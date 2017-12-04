using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStatus
{

	public static int currentLevel = 0;

	public static int GetCurrentLevel()
	{
		Debug.Log("current level " + currentLevel);
		return currentLevel;
	}

	public static int incLevel()
	{
		Debug.Log("inc level");
		return ++currentLevel;
	}
}
