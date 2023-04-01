public interface IItem
{
	// All items have a specific type
	ItemType Type { get; }

	public void UseItem();
	public void DestroyItem();
}

// These are item types in this game
public enum ItemType { Empty, Food, Scroll, Armor, Weapon }
