using UnityEngine;

public class Food : MonoBehaviour, IItem
{
	public ItemType Type { get; private set; }
	public int HpRegen = 5;
	private Backpack _backpack;

	// Use item and finish turn with active character
	public void UseItem()
	{
		_backpack.Owner.ActiveCharacter.ConsumeFood(this);
		_backpack.Owner.ResetCharacterFrameColor();
		_backpack.Owner.TakeCharacterAction();
	}

	// Destroy and remove item from backpack
	public void DestroyItem()
	{
		Destroy(gameObject);
		_backpack.RemoveItem(gameObject);
	}

	private void Awake()
	{
		Type = ItemType.Food;
		_backpack = GetComponentInParent<Backpack>();
	}
}
