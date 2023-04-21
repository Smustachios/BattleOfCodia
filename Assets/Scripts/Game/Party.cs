using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls flow of party actions
/// </summary>
public class Party : MonoBehaviour
{
	public string PartyName;
	public AttackButtons AttackButtons;
	public Controller PartyController;
	public Party EnemyParty;
	public Backpack Backpack;

	private Battle _currentBattle;

	public Character ActiveCharacter { get; private set; }
	public List<Character> CharacterList { get; private set; }
	private List<Character> _deadCharacters;
	private int _activeCharacterTracker = 0;

	// Use these to place characters in the party at right positions
	public float PlacementOffSetOperator;
	private float _characterPlacementPos = 0;
	private readonly float _characterPlacementOffset = 4.1f;


	private void Awake()
	{
		CharacterList = new List<Character>();
		_deadCharacters = new List<Character>();
		_currentBattle = GameObject.Find("GameManager").GetComponent<Battle>();
		Backpack = GetComponentInChildren<Backpack>();
	}

	// Reset active character, update info and take turn with first character
	public void StartPartyTurn()
	{
		GameManager.UpdateBattleLog.Invoke($"Its {PartyName} turn");
		GameManager.UpdateActionText.Invoke($"Its {PartyName} turn");

		UpdateCooldowns(); // Decrease cooldowns in the beginning of the turn

		_activeCharacterTracker = 0;
        ActiveCharacter = CharacterList[_activeCharacterTracker];

		UpdateAttackButtons();
		ActiveCharacter.CharacterFrame.color = Color.green;

		GameManager.UpdateBattleLog.Invoke($"Its {ActiveCharacter.Name} turn!");
		GameManager.UpdateActionText.Invoke($"Its {ActiveCharacter.Name} turn!");

		PartyController.TurnOnController(ActiveCharacter);
	}

	// Take a turn with a next character
	public void TakeCharacterAction()
	{
		// If all characters are finished, finish party turn
		if (_activeCharacterTracker >= CharacterList.Count - 1)
		{
			FinishPartyTurn();
		}
		else
		{
			ChangeActiveCharacter();

			UpdateAttackButtons();
			ActiveCharacter.CharacterFrame.color = Color.green;

			GameManager.UpdateBattleLog.Invoke($"Its {ActiveCharacter.Name} turn!");

			PartyController.TurnOnController(ActiveCharacter);
		}
	}

	// Gets and makes characters in the party at the beginning of level
	public void AddCharacters(params GameObject[] characters)
    {
        foreach (GameObject original in characters)
        {
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
			//character.ResetHp();
			character.GetComponent<SpecialAttack>().RemainingCooldown = 0;
		}

		ClearDeadCharacters();
		ResetCharacterPos();
	}

	// Reset character frame color back to default after character has done its action
	public void ResetCharacterFrameColor()
	{
		ActiveCharacter.CharacterFrame.color = Color.white;
	}

	// Clear party after battle is finished
	public void ClearParty()
	{
		ClearDeadCharacters();
		Backpack.ClearBackpack();

		foreach (Character character in CharacterList)
		{
			Destroy(character.gameObject);
		}

		CharacterList.Clear();
		_characterPlacementPos = 0;
	}

	// Clear dead characters from the party
	private void ClearDeadCharacters()
	{
		foreach (Character character in _deadCharacters)
		{
			Destroy(character.gameObject);
		}

		_deadCharacters.Clear();
	}

	// After clearing dead characters, move remaining characters back in order in the party and
	// make character pos tracker ready for new characters entering party
	private void ResetCharacterPos()
	{
		_characterPlacementPos = 0;

		foreach (Character character in CharacterList)
		{
			character.transform.position = character.ParentParty.transform.position;
			character.transform.position += new Vector3(0, _characterPlacementPos, 0);
			_characterPlacementPos += (_characterPlacementOffset * PlacementOffSetOperator);
		}
	}

	// Change to next character in the party
    private void ChangeActiveCharacter()
    {
        _activeCharacterTracker++;

        if (_activeCharacterTracker < CharacterList.Count)
        {
			ActiveCharacter.CharacterFrame.color = Color.white; // Reset last characters frame to inactive
            ActiveCharacter = CharacterList[_activeCharacterTracker];
        }
    }

	// Update attack buttons info
	private void UpdateAttackButtons()
	{
        AttackButtons.UpdateAttackButtons(ActiveCharacter.BasicAttack, ActiveCharacter.SpecialAttack,
            ActiveCharacter.GetComponent<SpecialAttack>().RemainingCooldown,
            ActiveCharacter.GetComponent<Attack>().GetAttackStats(),
            ActiveCharacter.GetComponent<SpecialAttack>().GetSpecialAttackStats());
    }

	// If cooldowns not ready decrease cooldown
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

    // Finish this partys turn
    private void FinishPartyTurn()
    {
		ResetCharacterFrameColor();
		_currentBattle.ChangeToNextParty(this);
    }
}
