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

    public void LevelOne()
    {
        _battle.HeroParty.InitPartyCharacters(1.0f, Hero, Solider);

        _battle.MonsterParty.InitPartyCharacters(-1.0f, Skeleton, Skeleton);
    }

	public void LevelTwo()
	{
		_battle.MonsterParty.InitPartyCharacters(-1.0f, Skeleton, Skeleton);
	}
}
