using UnityEngine;
using System.Collections.Generic;

public class Level : MonoBehaviour
{
    // Hero characters
    public GameObject Hero;
    public GameObject Solider;
    public GameObject Margot;
    public GameObject FinnFlecher;
    // Monster characters
    public GameObject UncodedOne;
    public GameObject Skeleton;
    public GameObject DarkHand;
    public GameObject UndeadSolider;

    private Battle _battle;

    private void Awake()
    {
        _battle = gameObject.GetComponent<Battle>();
    }

	// All avaible level start methods are here
	public void StartLevel(int level)
	{
		switch (level) 
		{
			case 1:
				LevelOne();
				_battle.StartBattle();
				break;
			case 2:
				LevelTwo();
				_battle.StartBattle();
				break;
			case 3:
				LevelThree();
				_battle.StartBattle();
				break;
			case 4:
				LevelFour();
				_battle.StartBattle();
				break;
			case 5:
				LevelFive();
				_battle.StartBattle();
				break;
			default:
				break;
		}
	}

	// Add characters with each new level to the party
    private void LevelOne()
    {
        _battle.HeroParty.AddCharacters(Hero);
        _battle.MonsterParty.AddCharacters(Skeleton, Skeleton);
    }

	private void LevelTwo()
	{
		_battle.MonsterParty.AddCharacters(Skeleton, Skeleton);
		_battle.HeroParty.ResetParty();
	}

	private void LevelThree()
	{
		_battle.MonsterParty.AddCharacters(Skeleton, Skeleton, Skeleton, UndeadSolider, UndeadSolider);
		_battle.HeroParty.ResetParty();
		_battle.HeroParty.AddCharacters(Solider, Solider);
	}

	private void LevelFour()
	{
		_battle.MonsterParty.AddCharacters(Skeleton, Skeleton, UndeadSolider, UndeadSolider, DarkHand);
		_battle.HeroParty.ResetParty();
		_battle.HeroParty.AddCharacters(FinnFlecher, Solider);
	}

	private void LevelFive()
	{
		_battle.MonsterParty.AddCharacters(Skeleton, Skeleton, UndeadSolider, UndeadSolider, DarkHand, UncodedOne);
		_battle.HeroParty.ResetParty();
		_battle.HeroParty.AddCharacters(Margot, Solider, Solider);
	}
}
