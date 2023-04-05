using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowCharacterStats : MonoBehaviour
{
	public TextMeshPro StatsText;
	private SpriteRenderer _spriteRenderer;
	private Character _character;
	private Dictionary<string, int> _stats;

	private void Awake()
	{
		_character = GetComponentInParent<Character>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_stats = new Dictionary<string, int>();
		_spriteRenderer.enabled = false;
		StatsText.enabled = false;
	}

	public void OnMouseEnter()
	{
		_spriteRenderer.enabled = true;
		StatsText.enabled = true;
	}

	public void OnMouseExit()
	{
		_spriteRenderer.enabled = false;
		StatsText.enabled = false;
	}

	private void BuildStatsString()
	{
		StatsText.text = null;

		foreach (KeyValuePair<string, int> pair in _stats)
		{
			StatsText.text += $"{pair.Key} : {pair.Value}\n";
		}
	}

	public void UpdateStatsInfo()
	{ 
		_stats.Clear();

		foreach (KeyValuePair<string, int> pair in _character.Stats)
		{
			if (pair.Value != 0)
			{
				_stats.Add(pair.Key, pair.Value);
			}
		}

		BuildStatsString();
	}
}
