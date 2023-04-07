using System.Collections.Generic;

/// <summary>
/// Special attack of a character. Has stats and can attack.
/// If attack is finished, character finishes its turn.
/// </summary>
public class SpecialAttack : AttackBase
{
    public int Cooldown;
    public int RemainingCooldown;


	private void Awake()
    {
        Attacker = GetComponent<Character>();
	}

    // Get damage amount from attack modifier and then deal damage to enemy.
    // Then finish character turn
    public override void AttackTarget(Character target, Controller controller)
    {
        RemainingCooldown = Cooldown; // Turn on cooldown

        int attackDamage = AttackModifier.CalculateDamage(Attacker, target, this);
		target.TakeDamage(attackDamage);

        GameManager.UpdateBattleLog.Invoke($"{Attacker.Name} did {attackDamage} damage to {target.Name}");

        FinishAttack(controller);
    }

	// Returns attack stats dictionary to caller
	public Dictionary<string, int> UpdateStatsInfoText()
	{
		return new Dictionary<string, int>
		{
			{ "Min Dmg", MinBaseDamage },
			{ "Max Dmg", MaxBaseDamage },
			{ "Miss %", (int)(MissChance * 10) },
			{ "Crit %", (int)(CritChance * 10) },
			{ "Min Crit", MinCritBonus },
			{ "Max Crit", MaxCritBonus },
			{ "Cooldown", Cooldown }
		};
	}
}
