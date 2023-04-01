using UnityEngine;

public class Character : MonoBehaviour
{
	public string Name;
	public CharacterType Type;
	public int CurrentHp;
	public int MaxHp;

	public int MeleeDamage;
	public int RangeDamage;
	public int MagicDamage;

	public int MeleeDefence;
	public int RangeDefence;
	public int MagicDefence;

	// Set these in the inspector
	public Sprite BasicAttack;
	public Sprite SpecialAttack;
	public SpriteRenderer CharacterFrame;

	public CharaterInfoText CharacterInfo { get; private set; }
	public Party ParentParty { get; private set; }

	public Armor EquipedItem { get; private set; }
	public GameObject CharacterItemSlot;
	

	private void Start()
	{
		CurrentHp = MaxHp;
		CharacterInfo = GetComponentInChildren<CharaterInfoText>();
		ParentParty = GetComponentInParent<Party>();
	}

	// Item calls this to equip item and use its perks
	public void EquipItem(Armor item)
	{
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
	}

	// Add character boost attack stat to ongoing attack
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
	}

	// Add healt from food
	public void ConsumeFood(Food food) 
	{
		CurrentHp += food.HpRegen;

		if (CurrentHp > MaxHp)
		{
			CurrentHp = MaxHp;
		}

		CharacterInfo.UpdateHPText(CurrentHp.ToString());
	}

	// Reset hp to max hp
	public void ResetHp()
	{
		CurrentHp = MaxHp;
		CharacterInfo.UpdateHPText(CurrentHp.ToString());
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

	private void SwapItem(Armor item)
	{
		ParentParty.Backpack.AddItem(EquipedItem.gameObject);
		EquipedItem.DestroyItem();

        EquipedItem = item;
        item.gameObject.transform.SetParent(CharacterItemSlot.transform, false);
        item.AddStats(this);
    }
}

// These are types of stats this game uses
public enum CharacterType { Melee, Range, Magic }