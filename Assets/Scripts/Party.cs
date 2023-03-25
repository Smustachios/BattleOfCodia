using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Party : MonoBehaviour
{
	public AttackButtons AttackButtons;
	public Controller PartyController;
	public Party EnemyParty;
	public Character ActiveCharacter { get; private set; } // This is active character whos turn is do to some action next
	public List<Character> CharacterList { get; private set; } // All characters who are in the party left
	private int _activeCharacterTracker = 0; // This is tracking active characters in the list

	// Delegate to do party finished turn event for battle to know when to change parties
	public delegate void OnPartyFinish(Party finishedParty);
	public static OnPartyFinish PartyFinishedTurn;

	public string PartyName;
	public bool HasAliveCharacters = true;


	private void Awake()
	{
		// Put all active characters to a list and set first active character in the party
		CharacterList = gameObject.GetComponentsInChildren<Character>().ToList();
		ActiveCharacter = CharacterList[0];
	}

	// When party starts its turn it will reset active characters to the beginning
	// and then take action with that first character
	public void StartPartyTurn()
	{
		Debug.Log($"Its {PartyName} turn");

		_activeCharacterTracker = -1;
		TakeCharacterAction();
	}

	// Brodcast to battle that this party has finished its turn
	private void FinishPartyTurn()
	{
		PartyFinishedTurn?.Invoke(this);
	}

	// Call this for each character in the party
	public void TakeCharacterAction()
	{
		// If all characters are finished, finish party turn
		if (_activeCharacterTracker >= CharacterList.Count - 1)
		{
			FinishPartyTurn();
		}
		// Else change to next character, update attack buttons to new characters spells and
		// start waiting for player to choose action for that character
		else
		{
			ChangeActiveCharacter();
			AttackButtons.UpdateAttackButtons(ActiveCharacter.BasicAttack, ActiveCharacter.SpecialAttack);
			PartyController.TurnOnController();
		}
	}

	public void ChangeActiveCharacter()
	{
		// Update list tracker
		_activeCharacterTracker++;

		// Make sure not loop out of list and change to new active character
		if(_activeCharacterTracker < CharacterList.Count)
		{
			ActiveCharacter = CharacterList[_activeCharacterTracker];
		}
	}
}
