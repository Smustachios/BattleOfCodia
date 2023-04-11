using UnityEngine;
using System;

/// <summary>
/// Manages level changing and battle log
/// </summary>
public class GameManager : MonoBehaviour
{
    public Party HeroParty;
    public Party MonsterParty;
    public BattleLog BattleLog;

    private Level _levelBuilder;
	private int _level = 1;

    public static Action<string> UpdateBattleLog;


    private void Awake()
    {
        _levelBuilder = gameObject.GetComponent<Level>();
    }

	// Game and first battle starts here
    private void Start()
    {
		_levelBuilder.StartLevel(_level);
    }

	// Incriment and start new level
	public void ChangeLevel()
	{
		_level++;
		_levelBuilder.StartLevel(_level);
	}
}
