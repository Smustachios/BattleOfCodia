using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Party : MonoBehaviour
{
	public AttackButtons AttackButtons;
	public Controller PartyController;
	public Party EnemyParty;
	private Battle _currentBattle;
	public Character ActiveCharacter { get; private set; } // This is active character whos turn is do to some action next
	public List<Character> CharacterList { get; private set; } // All characters who are in the party left
	private List<Character> _deadCharacters;
	public List<GameObject> CharaterPrefabs;
	private int _activeCharacterTracker = 0; // This is tracking active characters in the list

	// Delegate to do party finished turn event for battle to know when to change parties
	public delegate void OnPartyFinish(Party finishedParty);
	public static OnPartyFinish PartyFinishedTurn;

	public string PartyName;
	public bool HasAliveCharacters = true;

	private void Awake()
	{
		CharacterList = new List<Character>();
		_deadCharacters = new List<Character>();
		_currentBattle = GameObject.Find("GameManager").GetComponent<Battle>();
	}

	public void InitPartyCharacters(float offsetOperator, params GameObject[] characters)
    {
		float offset = 0;

        foreach (GameObject original in characters)
        {
			GameObject character = Instantiate(original, transform);
			CharacterList.Add(character.GetComponent<Character>());
			character.transform.position += new Vector3(0, offset, 0);
            offset += (4.1f * offsetOperator);
        }
    }

    // When party starts its turn it will reset active characters to the beginning
    // and then take action with that first character
    public void StartPartyTurn()
	{
		Debug.Log($"Its {PartyName} turn");

		UpdateCooldowns();
		_activeCharacterTracker = -1;
		TakeCharacterAction();
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

	public void RemoveCharacter(Character character)
	{
		CharacterList.Remove(character);
		_deadCharacters.Add(character);
		IsPartyAlive();
	}

	public void DisbandParty()
	{
		foreach (Character character in _deadCharacters)
		{
			Destroy(character.gameObject);
		}
	}

	private void IsPartyAlive()
	{
		if (CharacterList.Count <= 0) 
		{
			_currentBattle.FinishBattle(this);
		}
		else
		{
			return;
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

    // Brodcast to battle that this party has finished its turn
    private void FinishPartyTurn()
    {
        PartyFinishedTurn?.Invoke(this);
    }
}
