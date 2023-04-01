using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public Party HeroParty;
    public Party MonsterParty;
    public BattleLog BattleLog;

    private Level _levelBuilder;
	private int _level = 0;

    public static Action<string> UpdateBattleLog;

    private void Awake()
    {
        _levelBuilder = gameObject.GetComponent<Level>();
    }

    private void Start()
    {
		_level++;
		_levelBuilder.StartLevel(_level);
    }

	// Incriment and start new level
	public void ChangeLevel()
	{
		_level++;
		_levelBuilder.StartLevel(_level);
	}
}
