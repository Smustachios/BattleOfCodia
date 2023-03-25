using UnityEngine;

public class Attack : MonoBehaviour
{
	public int Damage = 1;
	private Character _character;

	private void Awake()
	{
		_character = GetComponent<Character>();
	}

	public void StartAttack(Character target)
	{
		InflictDamage(target);
		Debug.Log($"{_character.name} did {Damage} to {target.name}");
		target.CharacterInfo.UpdateHPText(target.CurrentHp.ToString());
		FinishAttack();
	}

	private void InflictDamage(Character enemy)
	{
		enemy.CurrentHp -= Damage;
	}

	private void FinishAttack()
	{
        _character.ParentParty.TakeCharacterAction();
    }
}
