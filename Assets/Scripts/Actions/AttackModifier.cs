using UnityEngine;

/// <summary>
/// Takes in attackers stats, enemies stats and returns calculated attack damage
/// </summary>
public static class AttackModifier
{
    public static int CalculateDamage(Character attacker, Character defender, AttackBase attack)
	{
		int attackDamage = 0;

		// No damage if attack misses
		if (Random.value < attack.MissChance)
		{
			GameManager.UpdateBattleLog.Invoke("Attack missed!");
			return 0;
		}
		else
		{
			attackDamage += Random.Range(attack.MinBaseDamage, attack.MaxBaseDamage + 1); // Add base damage

			// Add crit damage if attack is critical
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

			// If target has defence against attack type, substrack defence from damage
			attackDamage -= defender.DefendAttack(attack.AttackType);

			// Cant do negative damage
			if (attackDamage < 0) 
			{
				attackDamage = 0; 
			}

			return attackDamage;
		}
	}
}
