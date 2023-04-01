public interface IItem
{
	public void UseItem();
	public void DestroyItem();
	public ItemType ReturnItemType();
}

// These are item types in this game
public enum ItemType { Food, Scroll, Armor, Weapon }
