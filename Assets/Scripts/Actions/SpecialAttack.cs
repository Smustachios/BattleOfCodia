using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public int BaseDamage = 1;
    public float MissChance = 0;
    public float CritChance = 0.1f;
    public int CritRate = 1;
    public int Cooldown = 3;
    public int RemainingCooldown = 0;

    private int _attackDamage;
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
        _attackDamage = CalcDamage();
        InflictDamage(target, _attackDamage);
        Debug.Log($"{_character.name} did {_attackDamage} damage to {target.name}");
        target.CharacterInfo.UpdateHPText(target.CurrentHp.ToString());
        RemainingCooldown = Cooldown;
        FinishAttack();
    }

    // Take enemy damage amount of the enemy hp
    private void InflictDamage(Character enemy, int damage)
    {
        enemy.CurrentHp -= damage;
    }

    private int CalcDamage(int enemyDefence = 0)
    {
        if (Random.Range(0, 2) < MissChance)
        {
            return 0;
        }
        else if (Random.Range(0, 2) < CritChance)
        {
            return CritRate * BaseDamage;
        }
        else
        {
            return BaseDamage;
        }
    }

    // To finish attack call party to change characters
    private void FinishAttack()
    {
        _character.ParentParty.TakeCharacterAction();
    }
}