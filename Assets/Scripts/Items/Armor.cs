using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour, IItem, IStatsRenderer
{
    public ItemType Type;
    public string Name;

    public int MeleeDamageBonus;
    public int RangeDamageBonus;
    public int MagicDamageBonus;

    public int MeleeDefenceBonus;
    public int RangeDefenceBonus;
    public int MagicDefenceBonus;

    private Backpack _backpack;

	public StatsRenderer StatsInfoRenderer { get; private set; }
	public Dictionary<string, int> Stats { get; private set; }

    public void UseItem()
    {
        Character user = _backpack.Owner.ActiveCharacter;
        _backpack.Owner.ActiveCharacter.EquipItem(this);
        GameManager.UpdateBattleLog.Invoke($"{_backpack.Owner.ActiveCharacter.Name} equiped {Name}!");
        _backpack.RemoveItem(this.gameObject);
        _backpack.Owner.ResetCharacterFrameColor();
        _backpack.Owner.TakeCharacterAction();
    }

    public ItemType ReturnItemType()
    {
        return Type;
    }

    public void DestroyItem()
    {
        Destroy(this.gameObject);
    }

    public void AddStats(Character character)
    {
        character.MeleeDamage += MeleeDamageBonus;
        character.RangeDamage += RangeDamageBonus;
        character.MagicDamage += MagicDamageBonus;

        character.MeleeDefence += MeleeDefenceBonus;
        character.RangeDefence += RangeDefenceBonus;
        character.MagicDefence += MagicDefenceBonus;
    }

    public void RemoveStats(Character character)
    {
        character.MeleeDamage -= MeleeDamageBonus;
        character.RangeDamage -= RangeDamageBonus;
        character.MagicDamage -= MagicDamageBonus;

        character.MeleeDefence -= MeleeDefenceBonus;
        character.RangeDefence -= RangeDefenceBonus;
        character.MagicDefence -= MagicDefenceBonus;
    }

	private void UpdateStatsInfoText() 
	{
		Stats.Add("Melee Dmg", MeleeDamageBonus);
		Stats.Add("Range Dmg", RangeDamageBonus);
		Stats.Add("Magic Dmg", MagicDamageBonus);

		Stats.Add("Melee Defence", MeleeDefenceBonus);
		Stats.Add("Range Defence", RangeDefenceBonus);
		Stats.Add("Magic Defence", MagicDefenceBonus);

		StatsInfoRenderer.UpdateStatsInfo(Stats);
	}

	private void Awake()
    {
        _backpack = GetComponentInParent<Backpack>();
		StatsInfoRenderer = GetComponentInChildren<StatsRenderer>();
		Stats = new Dictionary<string, int>();
	}

	private void Start()
	{
		UpdateStatsInfoText();
	}
}
