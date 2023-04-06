using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Food : MonoBehaviour, IItem, IStatsRenderer
{
    public ItemType Type;
    public int HpRegen = 5;
    private Backpack _backpack;
	public Dictionary<string, int> Stats { get; private set; }
	public StatsRenderer StatsInfoRenderer { get; private set; }

	public void InvokeAction(Character character)
	{
		UseItem(character);
		DestroyItem();
		Debug.Log("invoked food");
		_backpack.Owner.ResetCharacterFrameColor();
		_backpack.Owner.TakeCharacterAction();
	}

    // Use item and finish turn with active character
    public void UseItem(Character character)
    {
        character.ConsumeFood(this);
        GameManager.UpdateBattleLog.Invoke($"{character.Name} healed {HpRegen} Hp!");
    }

    // Destroy and remove item from backpack
    public void DestroyItem()
    {
        Destroy(gameObject);
        _backpack.RemoveItem(gameObject);
    }

    public ItemType ReturnItemType()
    {
        return Type;
    }

    private void Awake()
    {
        _backpack = GetComponentInParent<Backpack>();
		Stats = new Dictionary<string, int>();
		StatsInfoRenderer = GetComponentInChildren<StatsRenderer>();
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
