using UnityEngine;

public class Character : MonoBehaviour
{
	public string Name;
	public int CurrentHp;
	public int MaxHp;
	public bool IsAlive = true;
	public Sprite BasicAttack;
	public Sprite SpecialAttack;
	public CharaterInfoText CharacterInfo;
	// Item EquipedItem
	

	private void Awake()
	{
		CurrentHp = MaxHp;
		CharacterInfo = GetComponentInChildren<CharaterInfoText>();
	}
}
