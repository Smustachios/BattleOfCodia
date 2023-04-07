using UnityEngine;

public class AttackBase : MonoBehaviour
{
	public CharacterType AttackType;
	public Character Attacker;

	public int MinBaseDamage;
	public int MaxBaseDamage;

	public float MissChance;

	public float CritChance;
	public int MinCritBonus;
	public int MaxCritBonus;

	public virtual void StartAttack(Character target, Controller controller) { }
	protected virtual void InflictDamage(Character enemy, int damage)
	{
		enemy.TakeDamage(damage);
	}

	// Check if this attack killed last character in the enemy party, if true move into next level
	protected virtual void FinishAttack()
	{
		Attacker.ParentParty.ResetCharacterFrameColor(); // Before change to next character, change active character frame back to default color
		Attacker.ParentParty.CheckPartyStatus();
	}
}
