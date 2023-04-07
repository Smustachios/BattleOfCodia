using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Shows stats of an parent object if hovered over the collider with a mouse.
/// </summary>
public class StatsRenderer : MonoBehaviour
{
	public TextMeshPro StatsText;

	private SpriteRenderer _spriteRenderer; // Background
	private Dictionary<string, int> _stats;

	private void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_stats = new Dictionary<string, int>();
	}

    private void Start()
    {
		// Turn off all renderes at the start
        _spriteRenderer.enabled = false;
        StatsText.enabled = false;
    }

	// Show object
    public void OnMouseEnter()
	{
		_spriteRenderer.enabled = true;
		StatsText.enabled = true;
	}

	// Hide object
	public void OnMouseExit()
	{
		_spriteRenderer.enabled = false;
		StatsText.enabled = false;
	}

	private void BuildStatsString()
	{
		StatsText.text = null; // Reset text string

		foreach (KeyValuePair<string, int> pair in _stats)
		{
			StatsText.text += $"{pair.Key} : {pair.Value}\n";
		}
	}

	// If change to stats happen update stats renderer object
	// but only show stats that are not 0
	public void UpdateStatsInfo(Dictionary<string, int> stats)
	{
		_stats.Clear(); // Reset dictionary

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
