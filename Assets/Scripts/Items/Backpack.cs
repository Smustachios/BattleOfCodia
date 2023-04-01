using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Backpack : MonoBehaviour
{
	public Party Owner { get; private set; }
	public List<GameObject> Foods { get; private set; }
    public List<GameObject> Gear { get; private set; }
    public List<GameObject> Weapons { get; private set; }

    private List<ItemSlot> _itemSlots;
	private List<GameObject> _items;
	private int _itemTracker;

	private void Awake()
	{
		Owner = GetComponentInParent<Party>();

		_itemSlots = new List<ItemSlot>();
		_items = new List<GameObject>();

		// Get all the itemslots in backpack
		_itemTracker = 0;

		foreach (ItemSlot itemSlot in GetComponentsInChildren<ItemSlot>())
		{
			_itemSlots.Add(itemSlot);
		}
	}

	// Itemspawner will call this to add items in the backpack
	public void AddItem(params GameObject[] items)
	{
		foreach (GameObject addedItem in items)
		{
			if (_itemTracker >= _itemSlots.Count)
			{
				continue;
			}
			else
			{
                GameObject newItem = Instantiate(addedItem, _itemSlots[_itemTracker].transform);
                _itemSlots[_itemTracker].GetComponent<ItemSlot>().AddItemToSlot(newItem);
                _items.Add(newItem);
                _itemTracker++;
            }
		}
	}

	// Remove item and sort backpack
	public void RemoveItem(GameObject item)
	{
		_items.Remove(item);
        _itemTracker--;
		Sort();
	}

	// Places all the items in a row in the backpack after some item has been taken out
	public void Sort()
	{
		for (int i = 0; i < _items.Count; i++)
		{
			_items[i].GetComponent<Transform>().SetParent(_itemSlots[i].transform, false);
		}
	}

	private void SortItemToList(GameObject item)
	{
		
	}
}
