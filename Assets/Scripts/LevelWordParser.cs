using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWordParser {
	
	Dictionary<int, List<string>> words = new Dictionary<int, List<string>>();
	private string separator = "-";
	
	public LevelWordParser()
	{
		TextAsset textFile = (TextAsset) Resources.Load ("levels", typeof(TextAsset));

		string[] allLines = textFile.text.Split('\n');

		int level = 0;
		foreach (string line in allLines)
		{
			string safeLine = line.Replace("\n", "");
			if (safeLine.StartsWith(separator))
			{
				level = int.Parse(safeLine.Replace(separator, ""));
				words.Add(level, new List<string>());
			}
			else
			{
				//TODO remove
				Debug.Log(level + ":" + safeLine);
				words[level].Add(safeLine);
			}
		}
	}

	public Dictionary<int, List<string>> getWordDictionary()
	{
		return words;
	}
}
