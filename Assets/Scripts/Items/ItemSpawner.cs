using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
	public List<GameObject> FoodPrefabs;
	// Other item lists

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
		SpawnStartingItems();
	}

	// Spawn some items for both party in the beginning of first battle
	public void SpawnStartingItems()
	{
		for (int i = 0; i < 3; i++)
		{
			_heroBackpack.AddItem(FoodPrefabs[Random.Range(0, FoodPrefabs.Count)]);
		}
		
		_monsterBackpack.AddItem(FoodPrefabs[0]);
	}
}
