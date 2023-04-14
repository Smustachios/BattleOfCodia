using UnityEngine;

/// <summary>
/// Controls flow of the battle
/// </summary>
public class Battle : MonoBehaviour
{
	public Party HeroParty;
	public Party MonsterParty;

	public Party ActiveParty { get; private set; }
	private GameManager _gameManager;

	private void Awake()
	{
		_gameManager = GetComponent<GameManager>();
	}

	// Start battle with hero party and start looping between both parties from here
	public void StartBattle()
	{
		ActiveParty = HeroParty;
		ActiveParty.StartPartyTurn();
	}

	// If player kills all the characters in monster party, load new level
	// Otherwise game is lost
	public void FinishBattle(Party loser)
	{
		if (loser.PartyName == "Monster Party")
		{
			MonsterParty.ClearParty();
			_gameManager.ChangeLevel();
		}
		else
		{
			Debug.Log("YOU LOSE");
		}
	}

    // Take what party just finished its turn and change to next party
    public void ChangeToNextParty(Party finishedParty)
    {
        if (finishedParty == HeroParty)
        {
            ActiveParty = MonsterParty;
        }
        else
        {
            ActiveParty = HeroParty;
        }

		ActiveParty.StartPartyTurn();
	}
}
