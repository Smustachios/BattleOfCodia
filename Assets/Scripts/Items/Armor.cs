using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All weapons and gear are armor type.
/// Holds stats and basic methods of them.
/// </summary>
public class Armor : MonoBehaviour, IItem
{
    public ItemType Type;
    public string Name;

    public int MeleeDamageBonus;
    public int RangeDamageBonus;
    public int MagicDamageBonus;

    public int MeleeDefenceBonus;
    public int RangeDefenceBonus;
    public int MagicDefenceBonus;

    public Backpack Backpack { get; private set; }
    public bool IsInBackpack { get; private set; }
	public StatsRenderer StatsInfoRenderer { get; private set; }


	private void Awake()
	{
		Backpack = GetComponentInParent<Backpack>();
		StatsInfoRenderer = GetComponentInChildren<StatsRenderer>();
		IsInBackpack = true;
	}

	private void Start()
	{
		StatsInfoRenderer.UpdateStatsInfo(GetItemStats());
	}

	// Using armor action
	public void InvokeAction(Character character, Controller controller)
	{
		UseItem(character);

		// Finish character action

		controller.IsControllersTurn = false;
		Backpack.Owner.ResetCharacterFrameColor();
		Backpack.Owner.TakeCharacterAction();
	}

	// Equip item on character
    public void UseItem(Character character)
    {
		character.EquipItem(this);
		Backpack.RemoveItem(this.gameObject);

		GameManager.UpdateBattleLog.Invoke($"{character.Name} equiped {Name}!");
	}

	// Get item type for keeping track of typed list
    public ItemType ReturnItemType()
    {
        return Type;
    }

    public void DestroyItem()
    {
        Destroy(this.gameObject);
    }

	// Character calls to add stats from equiped item to its stats
    public void AddStats(Character character)
    {
        character.MeleeDamage += MeleeDamageBonus;
        character.RangeDamage += RangeDamageBonus;
        character.MagicDamage += MagicDamageBonus;

        character.MeleeDefence += MeleeDefenceBonus;
        character.RangeDefence += RangeDefenceBonus;
        character.MagicDefence += MagicDefenceBonus;
    }

	// If character chances item or dies remove item perks from character
    public void RemoveStats(Character character)
    {
        character.MeleeDamage -= MeleeDamageBonus;
        character.RangeDamage -= RangeDamageBonus;
        character.MagicDamage -= MagicDamageBonus;

        character.MeleeDefence -= MeleeDefenceBonus;
        character.RangeDefence -= RangeDefenceBonus;
        character.MagicDefence -= MagicDefenceBonus;
    }

	// Get stats dictionary
	public Dictionary<string, int> GetItemStats() 
	{
		return new Dictionary<string, int>
		{
			{ "Melee Dmg", MeleeDamageBonus },
			{ "Range Dmg", RangeDamageBonus },
			{ "Magic Dmg", MagicDamageBonus },
			{ "Melee Defence", MeleeDefenceBonus },
			{ "Range Defence", RangeDefenceBonus },
			{ "Magic Defence", MagicDefenceBonus }
		};
	}

    public void RemoveFromBackpack()
    {
        IsInBackpack = false;
    }
}
