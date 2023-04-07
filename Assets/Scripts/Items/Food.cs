using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IItem, IStatsRenderer
{
    public ItemType Type;
    public int HpRegen = 5;

    public Backpack Backpack { get; private set; }
    public bool IsInBackpack { get; private set; }

    public Dictionary<string, int> Stats { get; private set; }
	public StatsRenderer StatsInfoRenderer { get; private set; }


	public void InvokeAction(Character character, Controller controller)
	{
		UseItem(character, controller);
		DestroyItem();
		Backpack.Owner.ResetCharacterFrameColor();
		Backpack.Owner.TakeCharacterAction();
	}

    // Use item and finish turn with active character
    public void UseItem(Character character, Controller controller)
    {
        controller.IsControllersTurn = false;
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

    private void Awake()
    {
        Backpack = GetComponentInParent<Backpack>();
		Stats = new Dictionary<string, int>();
		StatsInfoRenderer = GetComponentInChildren<StatsRenderer>();
        IsInBackpack = true;
    }

	private void Start()
	{
		UpdateStatsInfoText();
	}

	public void UpdateStatsInfoText() 
	{
		Stats.Clear();

		Stats.Add("Hp Regen", HpRegen);

		StatsInfoRenderer.UpdateStatsInfo(Stats);
	}
}
