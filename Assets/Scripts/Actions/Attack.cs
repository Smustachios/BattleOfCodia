using UnityEngine;

public class Attack : MonoBehaviour
{
	public int Damage = 1;
	private Character _character;

	private void Awake()
	{
		// This is the character who can use this attack
		_character = GetComponent<Character>();
	}

	// Calculate and then inflick damage to enemy character
	// Then finish character turn and move onto next character
	public void StartAttack(Character target)
	{
		InflictDamage(target);
		Debug.Log($"{_character.name} did {Damage} to {target.name}");
		target.CharacterInfo.UpdateHPText(target.CurrentHp.ToString());
		FinishAttack();
	}

	// Take enemy damage amount of the enemy hp
	private void InflictDamage(Character enemy)
	{
		enemy.CurrentHp -= Damage;
	}

	// To finish attack call party to change characters
	private void FinishAttack()
	{
        _character.ParentParty.TakeCharacterAction();
    }
}
