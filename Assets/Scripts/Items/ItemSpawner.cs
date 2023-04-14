using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Create items randomly.
/// </summary>
public class ItemSpawner : MonoBehaviour
{
	public List<GameObject> FoodPrefabs;
	public List<GameObject> GearPrefabs;
	public List<GameObject> WeaponPrefabs;


	// Spawn random armor items
	public GameObject[] SpawnRandomItems(int nOfItems)
	{
		GameObject[] items = new GameObject[nOfItems];

		for (int i = 0; i < nOfItems; i++)
		{
			GameObject item = GearPrefabs[Random.Range(0, GearPrefabs.Count)];
			items[i] = item;
		}

		return items;
	}

	// Spawn random food items
    public GameObject[] SpawnRandomFood(int nOfFood)
    {
        GameObject[] items = new GameObject[nOfFood];

        for (int i = 0; i < nOfFood; i++)
        {
            GameObject item = FoodPrefabs[Random.Range(0, FoodPrefabs.Count)];
            items[i] = item;
        }

        return items;
    }

	// Spawn random weapon items
    public GameObject[] SpawnRandomWeapons(int nOfWeapons)
    {
        GameObject[] items = new GameObject[nOfWeapons];

        for (int i = 0; i < nOfWeapons; i++)
        {
            GameObject item = WeaponPrefabs[Random.Range(0, WeaponPrefabs.Count)];
            items[i] = item;
        }

        return items;
    }
}
