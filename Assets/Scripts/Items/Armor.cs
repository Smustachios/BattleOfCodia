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

    public Backpack Backpack { get; private set; }
    public bool IsInBackpack { get; private set; }

	public StatsRenderer StatsInfoRenderer { get; private set; }
	public Dictionary<string, int> Stats { get; private set; }

	public void InvokeAction(Character character, Controller controller)
	{
		UseItem(character, controller);

		Backpack.Owner.ResetCharacterFrameColor();
		Backpack.Owner.TakeCharacterAction();
	}

    public void UseItem(Character character, Controller controller)
    {
        controller.IsControllersTurn = false;
		character.EquipItem(this);
		GameManager.UpdateBattleLog.Invoke($"{character.Name} equiped {Name}!");
		Backpack.RemoveItem(this.gameObject);
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

	public void UpdateStatsInfoText() 
	{
		Stats.Add("Melee Dmg", MeleeDamageBonus);
		Stats.Add("Range Dmg", RangeDamageBonus);
		Stats.Add("Magic Dmg", MagicDamageBonus);

		Stats.Add("Melee Defence", MeleeDefenceBonus);
		Stats.Add("Range Defence", RangeDefenceBonus);
		Stats.Add("Magic Defence", MagicDefenceBonus);

		StatsInfoRenderer.UpdateStatsInfo(Stats);
	}

    public void RemoveFromBackpack()
    {
        IsInBackpack = false;
    }

    private void Awake()
    {
        Backpack = GetComponentInParent<Backpack>();
        StatsInfoRenderer = GetComponentInChildren<StatsRenderer>();
        Stats = new Dictionary<string, int>();
        IsInBackpack = true;
    }

    private void Start()
	{
		UpdateStatsInfoText();
	}
}
