using UnityEngine;

public class ItemSlot : MonoBehaviour
{
	// All item slots in backpack have this script
	public IItem Item { get; private set; }
	public bool HasItem = false;


	public void AddItemToSlot(GameObject item)
	{
		IItem newItem = item.GetComponent<IItem>();

		// Make sure item has Iitem component before adding to slot
		if (newItem != null) 
		{
			Item = newItem;
		}
	}
}
