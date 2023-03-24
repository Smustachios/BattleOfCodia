using UnityEngine;

public class Battle : MonoBehaviour
{
	// Both party refernces
	public Party HeroParty;
	public Party MonsterParty;
	// Currently active party in the battle
	public Party ActiveParty;
	private int _count = 0;

	private void Start()
	{
		// Subscribe to controller finished taking turns with all its character event
		Party.PartyFinishedTurn += TakePartyTurn;

		StartBattle();
	}

	// Take in what controller just finished taking turns and change active controller to next one
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

	// Starts battle with hero party and calls for that controller to take character turn.
	// Once hero party controller has taken all his turns, it will start looping between both controllers for a gameplay.
	private void StartBattle()
	{
		ActiveParty = HeroParty;
		TakePartyTurn(MonsterParty);
	}

	// This method will be called once controller script finishes taking turns with each of partys characters.
	// Then change to next controller to take its character turns and loop back here to change again.
	private void TakePartyTurn(Party finishedParty)
	{
		ChangeToNextParty(finishedParty);
		ActiveParty.StartPartyTurn();
	}

	private void OnDisable()
	{
		Party.PartyFinishedTurn -= TakePartyTurn;
	}
}
