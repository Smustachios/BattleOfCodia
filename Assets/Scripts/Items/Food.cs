using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Consumable food in the game that restores hp to character.
/// </summary>
public class Food : MonoBehaviour, IItem
{
    public ItemType Type;
    public int HpRegen = 5;
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
		StatsInfoRenderer.UpdateStatsInfo(GetFoodStats());
	}

	// Invoke character action on food item
	public void InvokeAction(Character character, Controller controller)
	{
		UseItem(character);
		DestroyItem();

		controller.IsControllersTurn = false;
		Backpack.Owner.ResetCharacterFrameColor();
		Backpack.Owner.TakeCharacterAction();
	}

    public void UseItem(Character character)
    {
        character.ConsumeFood(this);

        GameManager.UpdateBattleLog.Invoke($"{character.Name} healed {HpRegen} Hp!");
    }

    // Destroy and remove item from backpack
    public void DestroyItem()
    {
        Destroy(gameObject);
        Backpack.RemoveItem(gameObject);
    }

    public ItemType ReturnItemType()
    {
        return Type;
    }

	// Get food stats dictionary
	public Dictionary<string, int> GetFoodStats()
	{
		return new Dictionary<string, int>
		{
			{ "Hp Regen", HpRegen }
		};
	}
}
