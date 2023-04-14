using UnityEngine;

/// <summary>
/// Hold items in the backpack.
/// </summary>
public class ItemSlot : MonoBehaviour
{
	public IItem Item { get; private set; }
	public bool HasItem = false;


	public void AddItemToSlot(GameObject item)
	{
		// Make sure item has Iitem component before adding to slot
		if (item.TryGetComponent<IItem>(out var newItem)) 
		{
			Item = newItem;
		}
	}
}
