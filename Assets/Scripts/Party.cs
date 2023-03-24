using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Party : MonoBehaviour
{
	public AttackButtons AttackButtons;
	public Controller PartyController;
	public Party enemyParty;
	public Character ActiveCharacter { get; private set; } // This is active character whos turn is do to some action next
	public List<Character> CharacterList { get; private set; } // All characters who are in the party left
	public int _activeCharacterTracker; // This is tracking active characters in the list


	public delegate void OnPartyFinish(Party finishedParty);
	public static OnPartyFinish PartyFinishedTurn;

	public string PartyName;
	public bool HasAliveCharacters = true;


	private void Awake()
	{
		_activeCharacterTracker = 0;
		// Get all characters and set first active character in the party
		CharacterList = gameObject.GetComponentsInChildren<Character>().ToList();
		ActiveCharacter = CharacterList[0];
	}

	public void StartPartyTurn()
	{
		Debug.Log($"Its {PartyName} turn");
		_activeCharacterTracker = -1;
		TakeCharacterAction();
	}
	private void FinishPartyTurn()
	{
		PartyFinishedTurn?.Invoke(this);
	}

	public void TakeCharacterAction()
	{
		if (_activeCharacterTracker >= CharacterList.Count - 1)
		{
			FinishPartyTurn();
		}
		else
		{
			ChangeActiveCharacter();
			AttackButtons.UpdateAttackButtons(ActiveCharacter.BasicAttack, ActiveCharacter.SpecialAttack);
			PartyController.TurnOnController();
		}
	}

	public void ChangeActiveCharacter()
	{
		// Set next active character from the list
		_activeCharacterTracker++;

		// If all characters have taken their turn loop back to beginning of list for next round
		if(_activeCharacterTracker < CharacterList.Count)
		{
			ActiveCharacter = CharacterList[_activeCharacterTracker];
		}
	}

	private void OnDisable()
	{
	}
}
