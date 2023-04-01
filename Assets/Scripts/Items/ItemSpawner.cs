using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
	public List<GameObject> FoodPrefabs;
	public List<GameObject> GearPrefabs;
	public List<GameObject> WeaponPrefabs;

	private GameManager _gameManager;
	private Backpack _heroBackpack;
	private Backpack _monsterBackpack;

	private void Awake()
	{
		_gameManager = GetComponent<GameManager>();
	}

	private void Start()
	{
		_heroBackpack = _gameManager.HeroParty.GetComponentInChildren<Backpack>();
		_monsterBackpack = _gameManager.MonsterParty.GetComponentInChildren<Backpack>();
	}

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
