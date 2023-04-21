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
	private int _round = 0;

	private void Awake()
	{
		_gameManager = GetComponent<GameManager>();
	}

	// Start battle with hero party and start looping between both parties from here
	public void StartBattle()
	{
		_round++;
		ActiveParty = HeroParty;
		ActiveParty.StartPartyTurn();
	}

	// Once battle round is finished load new level or show game over
	public void FinishBattle(Party loser)
	{
		if (loser.PartyName == "Monster Party")
		{
			if (_round <= 5)
			{
				MonsterParty.ClearParty();
				_gameManager.ChangeLevel();
			}
			else
			{
				_gameManager.ActivateGameOver("You win!");
			}
		}
		else
		{
			_gameManager.ActivateGameOver("You lose!");
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
