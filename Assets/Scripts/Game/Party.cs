using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour
{
	public AttackButtons AttackButtons;
	public Controller PartyController;
	public Party EnemyParty;
	public Backpack Backpack;
	private Battle _currentBattle;

	public Character ActiveCharacter { get; private set; }
	public List<Character> CharacterList { get; private set; } // All characters who are in the party left
	private List<Character> _deadCharacters;
	private int _activeCharacterTracker = 0; // This is tracking active characters in the list

	public string PartyName;

	// Use these to place characters in the party at right positions
	private float _characterPlacementPos = 0;
	private readonly float _characterPlacementOffset = 4.1f;
	public float PlacementOffSetOperator;


	private void Awake()
	{
		CharacterList = new List<Character>();
		_deadCharacters = new List<Character>();
		_currentBattle = GameObject.Find("GameManager").GetComponent<Battle>();
		Backpack = GetComponentInChildren<Backpack>();
	}

	// When party starts its turn it will reset active characters to the beginning
	// and then take action with that first character
	public void StartPartyTurn()
	{
		GameManager.UpdateBattleLog.Invoke($"Its {PartyName} turn");

		UpdateCooldowns(); // Decrease cooldowns in the beginning of the turn
		_activeCharacterTracker = -1;
		TakeCharacterAction();
	}

	// Use this to add characters into this party
	public void AddCharacters(params GameObject[] characters)
    {
        foreach (GameObject original in characters)
        {
			// Make a copy of the prefab and add it to list of alive characters
			GameObject character = Instantiate(original, transform);
			CharacterList.Add(character.GetComponent<Character>());

			// Place character in the scene and then move postion tracker for next character
			character.transform.position += new Vector3(0, _characterPlacementPos, 0);
            _characterPlacementPos += (_characterPlacementOffset * PlacementOffSetOperator);
        }
    }

	// Decrement next character placement position and then remove character from alive list and add to dead list
	public void RemoveCharacter(Character character)
	{
		_characterPlacementPos -= _characterPlacementOffset;
		CharacterList.Remove(character);
		_deadCharacters.Add(character);
	}

	// In the end of the battle clear all dead characters from the scene
	// and from the dead character list
	public void ClearDeadCharacters()
	{
		foreach (Character character in _deadCharacters)
		{
			Destroy(character.gameObject);
		}

		_deadCharacters.Clear();
		_characterPlacementPos = 0; // Reset character placement tracker
	}

	// Check after each attack if there is any alive characters in the enemy party
	public void CheckPartyStatus()
	{
		if(EnemyParty.CharacterList.Count > 0)
		{
			TakeCharacterAction();
		}
		else
		{
			_currentBattle.FinishBattle(EnemyParty);
		}
	}

	// Used only for player
	// Reset healt, cooldowns and clear all the dead characters
	public void ResetParty()
	{
		foreach (Character character in CharacterList)
		{
			character.ResetHp();
			character.GetComponent<SpecialAttack>().RemainingCooldown = 0;
		}

		ClearDeadCharacters();
		ResetCharacterPos();
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
		// wait for the controller to choose action for that character
		else
		{
			ChangeActiveCharacter();

			// Update "UI" info for new active character
			AttackButtons.UpdateAttackButtons(ActiveCharacter.BasicAttack, ActiveCharacter.SpecialAttack, 
				ActiveCharacter.GetComponent<SpecialAttack>().RemainingCooldown);
			ActiveCharacter.CharacterFrame.color = Color.green;

			GameManager.UpdateBattleLog.Invoke($"Its {ActiveCharacter.Name} turn!");
            PartyController.TurnOnController();
		}
	}

	// After clearing dead characters, move remaining characters back in order in the party and
	// make character pos tracker ready for new characters entering party
	private void ResetCharacterPos()
	{
		foreach(Character character in CharacterList)
		{
			character.transform.position = character.ParentParty.transform.position;
			character.transform.position += new Vector3(0, _characterPlacementPos, 0);
			_characterPlacementPos += (_characterPlacementOffset * PlacementOffSetOperator);
		}
	}

    private void ChangeActiveCharacter()
    {
        // Update list tracker
        _activeCharacterTracker++;

        // Make sure not loop out of list and change to new active character
        if (_activeCharacterTracker < CharacterList.Count)
        {
            ActiveCharacter = CharacterList[_activeCharacterTracker];
        }
    }

	// If special attack is ready skip, else substrack 1 from cooldown
    private void UpdateCooldowns()
	{
		foreach (Character character in CharacterList)
		{
			if (character.GetComponent<SpecialAttack>().RemainingCooldown <= 0)
			{
				continue;
			}
			else
			{
                character.GetComponent<SpecialAttack>().RemainingCooldown -= 1;
            }
		}
	}

	// Reset character frame color back to default after character has done its action
	public void ResetCharacterFrameColor()
	{
		ActiveCharacter.CharacterFrame.color = Color.white;
	}

    // Tell battle this party has finished its turn and to change to other party
    private void FinishPartyTurn()
    {
		_currentBattle.ChangeToNextParty(this);
		_currentBattle.TakePartyTurn();
    }
}
