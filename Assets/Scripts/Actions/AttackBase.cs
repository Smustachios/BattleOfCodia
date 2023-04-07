using UnityEngine;

/// <summary>
/// Base for all attacks.
/// Has basic stats, attack and finish attack mehtods.
/// </summary>
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


	public virtual void AttackTarget(Character target, Controller controller) { }

	// Check if this attack killed last character in the enemy party, if true move into next level
	protected virtual void FinishAttack(Controller controller)
	{
		controller.IsControllersTurn = false;
		Attacker.ParentParty.CheckPartyStatus();
	}
}
