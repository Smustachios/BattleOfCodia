using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents backpack each party has.
/// </summary>
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
		Foods = new List<GameObject>();
		Gear = new List<GameObject>();
		Weapons = new List<GameObject>();
	}

	private void Start()
	{
		_itemTracker = 0;
		GetItemSlots();
	}

	// To add new item in the backpack you need to create it into it
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
				ReturnItemTypedList(newItem).Add(newItem); // Add into specific type list
                _itemTracker++;
            }
		}
	}

	// Remove item and sort backpack
	public void RemoveItem(GameObject item)
	{
		_items.Remove(item);
		ReturnItemTypedList(item).Remove(item);
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

	// Clear backpack at the end of the battle
	public void ClearBackpack()
	{
		foreach (GameObject item in _items)
		{
			Destroy(item);
		}

		_items.Clear();
		Foods.Clear();
		Gear.Clear();
		Weapons.Clear();
		_itemTracker = 0;
	}

	// Check what type of list a item should go
	private List<GameObject> ReturnItemTypedList(GameObject item)
	{
		return item.GetComponent<IItem>().ReturnItemType() switch
		{
			ItemType.Food => Foods,
			ItemType.Armor => Gear,
			ItemType.Weapon => Weapons,
			_ => null
		};
	}

	private void GetItemSlots()
	{
		foreach (ItemSlot itemSlot in GetComponentsInChildren<ItemSlot>())
		{
			_itemSlots.Add(itemSlot);
		}
	}
}
