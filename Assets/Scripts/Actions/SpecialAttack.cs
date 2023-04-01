using UnityEngine;

public class SpecialAttack : AttackBase
{
    public int Cooldown;
    public int RemainingCooldown;

    private void Awake()
    {
        // This is the character who can use this attack
        Attacker = GetComponent<Character>();
    }

    // Calculate and then inflick damage to enemy character
    // Then finish character turn and move onto next character
    public override void StartAttack(Character target)
    {
        int attackDamage = AttackModifier.CalculateDamage(Attacker, target, this);
        InflictDamage(target, attackDamage);

        GameManager.UpdateBattleLog.Invoke($"{Attacker.name} did {attackDamage} damage to {target.name}");
        target.CharacterInfo.UpdateHPText(target.CurrentHp.ToString());

        RemainingCooldown = Cooldown; // Restart special attack cooldown counter
        FinishAttack();
    }
}
