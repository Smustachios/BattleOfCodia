public interface IItem : IAction
{
	public void UseItem(Character character);
	public void DestroyItem();
	public ItemType ReturnItemType();
}

// These are item types in this game
public enum ItemType { Food, Scroll, Armor, Weapon }
