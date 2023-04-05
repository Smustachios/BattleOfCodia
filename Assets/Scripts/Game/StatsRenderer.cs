using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsRenderer : MonoBehaviour
{
	public TextMeshPro StatsText;
	private SpriteRenderer _spriteRenderer;
	private Dictionary<string, int> _stats;

	private void Awake()
	{
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

	public void UpdateStatsInfo(Dictionary<string, int> stats)
	{
		_stats.Clear();

		foreach (KeyValuePair<string, int> pair in stats)
		{
			if (pair.Value != 0)
			{
				_stats.Add(pair.Key, pair.Value);
			}
		}

		BuildStatsString();
	}
}
