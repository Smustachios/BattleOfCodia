/// <summary>
/// Every item needs to have a backpack to be in. Every item should be usable and destroyable.
/// </summary>
public interface IItem : IAction
{
	public Backpack Backpack { get; }
	public bool IsInBackpack { get; }

	public void UseItem(Character character);
	public void DestroyItem();
	public ItemType ReturnItemType();
}

// These are item types in this game
public enum ItemType { Food, Scroll, Armor, Weapon }
