using System.Collections.Generic;
using UnityEngine;

public class Attack : AttackBase
{
	public StatsRenderer StatsInfoRenderer { get; private set; }
	public Dictionary<string, int> Stats { get; private set; }

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

		GameManager.UpdateBattleLog.Invoke($"{Attacker.name} did {attackDamage} damage to {target.name}");
		target.CharacterInfo.UpdateHPText(target.CurrentHp.ToString());

		FinishAttack();
	}

	public void UpdateStatsInfoText(StatsRenderer info)
	{
		Stats = new Dictionary<string, int>
		{
			{ "Min Dmg", MinBaseDamage },
			{ "Max Dmg", MaxBaseDamage },
			{ "Miss %", (int)(MissChance * 10) },
			{ "Crit %", (int)(CritChance * 10) },
			{ "Min Crit", MinCritBonus },
			{ "Max Crit", MaxCritBonus }
		};

		info.UpdateStatsInfo(Stats);
	}
}
