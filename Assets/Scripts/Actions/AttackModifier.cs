using UnityEngine;

public static class AttackModifier
{
    public static int CalculateDamage(Character attacker, Character defender, AttackBase attack)
	{
		int attackDamage = 0;

		// Check if attack will miss
		if (Random.value < attack.MissChance)
		{
			GameManager.UpdateBattleLog.Invoke("Attack missed!");
			return 0;
		}
		// Calculate what damage attack will do
		else
		{
			// Base damage
			attackDamage += Random.Range(attack.MinBaseDamage, attack.MaxBaseDamage + 1);

			// Check if attack will be critical
			if (attack.CritChance > 0)
			{
				if (Random.value < attack.CritChance)
				{
					attackDamage += Random.Range(attack.MinCritBonus, attack.MaxCritBonus + 1);
				}
			}

			// Add character boost to attack if character and attack are same type
			if (attack.AttackType == attacker.Type)
			{
				attackDamage += attacker.BoostAttack(attack.AttackType);
			}

			// Target will try to take of some damage with type defence
			attackDamage -= defender.DefendAttack(attack.AttackType);

			if (attackDamage < 0) 
			{
				attackDamage = 0; 
			}

			return attackDamage;
		}
	}
}
