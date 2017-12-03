using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class LetterManager : MonoBehaviour
{
	[SerializeField] private Text textArea;
	[SerializeField] private string activeColor;
	[SerializeField] private string inactiveColor;
	
	private Queue<string> currentWords = new Queue<string>();
	private List<WordPair> completionMap = new List<WordPair>();
	
	public void SetCurrentWords(List<string> levelWords)
	{
		//TODO remove
		if (levelWords.Count < 1)
		{
			Debug.LogError("Empty Words");	
			return;
		}
		
		currentWords = new Queue<string>(levelWords);

		string currentWord = currentWords.Dequeue();
		
		setCurrentWord(currentWord);
		
		UpdateText();
	}

	private void setCurrentWord(string word)
	{
		char[] chars = word.ToCharArray();
		foreach (char c in chars)
		{
			completionMap.Add(new WordPair(c+"", false));
		}
	}

	public string GetNextLetter()
	{
		foreach (WordPair pair in completionMap)
		{
			if (!pair.found) return pair.letter;
		}

		Debug.LogError("No more letters to find, update the current word");
		return null;
	}

	public void HitLetter(string letter)
	{
		if (currentWords.Count == 0)
		{
			Debug.LogWarning("Already found all the words");
			return;
		}
		
		foreach (WordPair pair in completionMap)
		{
			if (pair.letter.Equals(letter) && !pair.found)
			{
				pair.found = true;
				
				if (isWordComplete())
				{
					if (currentWords.Count  > 0)
					{
						string nextWord = currentWords.Dequeue();
						setCurrentWord(nextWord);
					}
				}
			}
		}
		UpdateText();
		
		Debug.LogError("Can't find letter " + letter);
	}

	private bool isWordComplete()
	{
		foreach (WordPair pair in completionMap)
		{
			if (!pair.found) return false;
		}
		return true;
	}
	
	public bool IsLevelWordlistComplete()
	{
		return currentWords.Count == 0;
	}

	private void UpdateText()
	{
		StringBuilder builder = new StringBuilder();
		foreach (WordPair c in completionMap)
		{
			builder.Append(getRichTextChar(c.letter, c.found ? activeColor : inactiveColor));
			builder.Append(" ");
		}
		textArea.text = builder.ToString();
	}

	private string getRichTextChar(string c, string color)
	{
		return "<color=" + color+ ">" + c + "</color>";
	}
	
	private class WordPair
	{
		public WordPair(string letter, bool found)
		{
			this.letter = letter;
			this.found = found;
		}

		public string letter;
		public bool found;
	}
}
