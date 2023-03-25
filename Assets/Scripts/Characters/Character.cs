using UnityEngine;

public class Character : MonoBehaviour
{
	public string Name;
	public int CurrentHp;
	public int MaxHp;
	public bool IsAlive = true;
	public Sprite BasicAttack;
	public Sprite SpecialAttack;
	public CharaterInfoText CharacterInfo { get; private set; }
	public Party ParentParty { get; private set; }
	// Item EquipedItem
	

	private void Start()
	{
		CurrentHp = MaxHp;
		CharacterInfo = GetComponentInChildren<CharaterInfoText>();
		ParentParty = GetComponentInParent<Party>();
	}
}
