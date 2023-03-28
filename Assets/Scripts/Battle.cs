using UnityEngine;

public class Battle : MonoBehaviour
{
	// Both party in the battle
	public Party HeroParty;
	public Party MonsterParty;
	// Currently active party in the battle
	public Party ActiveParty { get; private set; }
	private GameManager _gameManager;

	private void Awake()
	{
		// After party finishes its turn this event is called
		Party.PartyFinishedTurn += TakePartyTurn;
		_gameManager = GetComponent<GameManager>();
	}

	// Start battle with hero party and start looping between both parties from here
	public void StartBattle()
	{
		ActiveParty = HeroParty;
		TakePartyTurn(MonsterParty);
	}

	public void FinishBattle(Party loser)
	{
		if (loser.PartyName == "Monster Party")
		{
			loser.DisbandParty();
			_gameManager.ChangeLevel();
		}
		else
		{
			Debug.Log("YOU LOSE");
		}
	}

    // Take what party just finished its turn and change to next party
    private void ChangeToNextParty(Party finishedParty)
    {
        if (finishedParty == HeroParty)
        {
            ActiveParty = MonsterParty;
        }
        else
        {
            ActiveParty = HeroParty;
        }
    }

    // This method will be called once party brodcasts its ending turn event
    // It will change party and calls new active party start turn method
    private void TakePartyTurn(Party finishedParty)
	{
		ChangeToNextParty(finishedParty);
		ActiveParty.StartPartyTurn();
	}

	// Unsubscribe from event
	private void OnDisable()
	{
		Party.PartyFinishedTurn -= TakePartyTurn;
	}
}
