using UnityEngine;

public class Attack : AttackBase
{
	private void Awake()
	{
		// This is the character who can use this attack
		Attacker = GetComponent<Character>();
	}

	// Calculate and then inflick damage to enemy character
	// Then finish character turn and move onto next character
	public override void StartAttack(Character target)
	{
		int attackDamage = AttackModifier.CalculateDamage(Attacker, target, this); // Considers all the stats to calculate damage
		InflictDamage(target, attackDamage);

		Debug.Log($"{Attacker.name} did {attackDamage} damage to {target.name}");
		target.CharacterInfo.UpdateHPText(target.CurrentHp.ToString());

		FinishAttack();
	}
}
