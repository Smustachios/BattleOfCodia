using UnityEngine;

/// <summary>
/// Holds and builds levels
/// </summary>
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
	private ItemSpawner _itemSpawner;


    private void Awake()
    {
        _battle = gameObject.GetComponent<Battle>();
		_itemSpawner = gameObject.GetComponent<ItemSpawner>();
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

	// Add and items to each level
    private void LevelOne()
    {
        _battle.HeroParty.AddCharacters(Hero, Solider);
        AddItems(_battle.HeroParty.Backpack, food: 2, items: 1, weapons: 1);

        _battle.MonsterParty.AddCharacters(Skeleton, Skeleton, UndeadSolider);
        AddItems(_battle.MonsterParty.Backpack, food: 1, items: 1, weapons: 1);
    }

	private void LevelTwo()
	{
        _battle.MonsterParty.AddCharacters(Skeleton, Skeleton, UndeadSolider);
        AddItems(_battle.MonsterParty.Backpack, food:1);

        _battle.HeroParty.ResetParty();
        _battle.HeroParty.AddCharacters(Solider);
        AddItems(_battle.HeroParty.Backpack, food: 1);

    }

	private void LevelThree()
	{
		_battle.MonsterParty.AddCharacters(Skeleton, Skeleton, Skeleton, UndeadSolider, UndeadSolider);
        AddItems(_battle.MonsterParty.Backpack, food: 2, items: 1, weapons: 2);

        _battle.HeroParty.ResetParty();
		_battle.HeroParty.AddCharacters(Solider, Solider);
        AddItems(_battle.HeroParty.Backpack, food: 2, items: 2);
    }

	private void LevelFour()
	{
		_battle.MonsterParty.AddCharacters(Skeleton, Skeleton, UndeadSolider, UndeadSolider, DarkHand);
        AddItems(_battle.MonsterParty.Backpack, food: 2, items: 1, weapons: 2);

        _battle.HeroParty.ResetParty();
		_battle.HeroParty.AddCharacters(FinnFlecher, Solider);
        AddItems(_battle.HeroParty.Backpack, food: 1, items: 1, weapons: 2);
    }

	private void LevelFive()
	{
		_battle.MonsterParty.AddCharacters(Skeleton, Skeleton, UndeadSolider, UndeadSolider, DarkHand, UncodedOne);
        AddItems(_battle.MonsterParty.Backpack, food: 3, items: 2, weapons: 2);

        _battle.HeroParty.ResetParty();
		_battle.HeroParty.AddCharacters(Margot, Solider, Solider);
        AddItems(_battle.HeroParty.Backpack, food: 1, items: 1, weapons: 1);
    }

	private void AddItems(Backpack partyBackpack, int food = 0, int items = 0, int weapons = 0)
	{
		if (food != 0)
			partyBackpack.AddItem(_itemSpawner.SpawnRandomFood(food));
		if (items != 0)
			partyBackpack.AddItem(_itemSpawner.SpawnRandomItems(items));
		if (weapons != 0)
			partyBackpack.AddItem(_itemSpawner.SpawnRandomWeapons(weapons));
    }
}
