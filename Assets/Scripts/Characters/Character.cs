using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Types this game uses for characters, items, armor and attacks
/// </summary>
public enum CharacterType { Melee, Range, Magic }


/// <summary>
/// Every character in the game is this class.
/// Has stats and basic character methods.
/// </summary>
public class Character : MonoBehaviour
{
	public string Name;
	public CharacterType Type;

	// Hp stats
	public int CurrentHp;
	public int MaxHp;
	// Attack stats
	public int MeleeDamage;
	public int RangeDamage;
	public int MagicDamage;
	// Defence stats
	public int MeleeDefence;
	public int RangeDefence;
	public int MagicDefence;
	// Components
	public Sprite BasicAttack;
	public Sprite SpecialAttack;
	public SpriteRenderer CharacterFrame;
	public GameObject CharacterItemSlot;

	public CharaterInfoText CharacterInfo { get; private set; }
	public StatsRenderer StatsInfoRenderer { get; private set; }
	public Party ParentParty { get; private set; }
	public Armor EquipedItem { get; private set; }
	

    private void Awake()
    {
        CharacterInfo = GetComponentInChildren<CharaterInfoText>();
        StatsInfoRenderer = GetComponentInChildren<StatsRenderer>();
        ParentParty = GetComponentInParent<Party>();
    }

    private void Start()
	{
		CurrentHp = MaxHp;
		StatsInfoRenderer.UpdateStatsInfo(GetCharacterStats());
	}

    // Item calls this to equip item and use its perks
    public void EquipItem(Armor item)
	{
		item.RemoveFromBackpack();

		if (EquipedItem != null)
		{
			SwapItem(item);
		}
		else
		{
            EquipedItem = item;
            item.gameObject.transform.SetParent(CharacterItemSlot.transform, false);
            item.AddStats(this);
        }

        StatsInfoRenderer.UpdateStatsInfo(GetCharacterStats());
    }

    // Add healt from food
    public void ConsumeFood(Food food)
    {
        CurrentHp += food.HpRegen;

        if (CurrentHp > MaxHp)
        {
            CurrentHp = MaxHp;
        }

		CharacterInfo.UpdateHpBar();
		StartCoroutine(CharacterInfo.ShowHpChange(food.HpRegen, Color.green));
	}

    // Take damage from enemy attack
    public void TakeDamage(int damage)
    {
        if (CurrentHp - damage > 0)
        {
            CurrentHp -= damage;
        }
        else
        {
            KillCharacter();
        }

		CharacterInfo.UpdateHpBar();
        StartCoroutine(CharacterInfo.ShowHpChange(damage, Color.red));
    }

    // Reset hp to max hp
    public void ResetHp()
    {
        CurrentHp = MaxHp;
        CharacterInfo.UpdateHpBar();
    }

    // Add boost to ongoing attack
    public int BoostAttack(CharacterType attackType)
	{
		return attackType switch
		{
			CharacterType.Melee => MeleeDamage,
			CharacterType.Range => RangeDamage,
			CharacterType.Magic => MagicDamage,
			_ => 0
		};
	}

	// Subtrack defence stat from incoming attack
	public int DefendAttack(CharacterType attackType)
	{
		return attackType switch
		{
			CharacterType.Melee => MeleeDefence,
			CharacterType.Range => RangeDefence,
			CharacterType.Magic => MagicDefence,
			_ => 0
		};
	}

    // Return character stats dictionary to caller
    public Dictionary<string, int> GetCharacterStats()
    {
        return new Dictionary<string, int>
        {
            { "Melee Damage", MeleeDamage },
            { "Range Damage", RangeDamage },
            { "Magic Damage", MagicDamage },
            { "Melee Defence", MeleeDefence },
            { "Range Defence", RangeDefence },
            { "Magic Defence", MagicDefence }
        };
    }

    // Kill this character. This does not delete character from the scene yet
    private void KillCharacter()
	{
		CurrentHp = 0;
		gameObject.GetComponent<BoxCollider2D>().enabled = false;
		gameObject.GetComponent<SpriteRenderer>().color = Color.red;

		if (EquipedItem != null)
		{
            EquipedItem.DestroyItem();
        }
		
		ParentParty.RemoveCharacter(this);
	}

    // Put old item back to backpack and equip new item
	private void SwapItem(Armor item)
	{
		ParentParty.Backpack.AddItem(EquipedItem.gameObject);
		EquipedItem.DestroyItem(); // Because add item makes new item old item needs to be destroyed

        EquipedItem = item;
        item.gameObject.transform.SetParent(CharacterItemSlot.transform, false);
        item.AddStats(this);
    }
}