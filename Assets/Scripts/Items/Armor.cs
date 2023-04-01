using UnityEngine;

public class Armor : MonoBehaviour, IItem
{
    public ItemType Type;
    public string Name;

    public int MeleeDamageBonus;
    public int RangeDamageBonus;
    public int MagicDamageBonus;

    public int MeleeDefenceBonus;
    public int RangeDefenceBonus;
    public int MagicDefenceBonus;

    private Backpack _backpack;

    public void UseItem()
    {
        Character user = _backpack.Owner.ActiveCharacter;
        _backpack.Owner.ActiveCharacter.EquipItem(this);
        GameManager.UpdateBattleLog.Invoke($"{_backpack.Owner.ActiveCharacter.Name} equiped {Name}!");
        _backpack.RemoveItem(this.gameObject);
        _backpack.Owner.ResetCharacterFrameColor();
        _backpack.Owner.TakeCharacterAction();
    }

    public ItemType ReturnItemType()
    {
        return Type;
    }

    public void DestroyItem()
    {
        Destroy(this.gameObject);
    }

    public void AddStats(Character character)
    {
        character.MeleeDamage += MeleeDamageBonus;
        character.RangeDamage += RangeDamageBonus;
        character.MagicDamage += MagicDamageBonus;

        character.MeleeDefence += MeleeDefenceBonus;
        character.RangeDefence += RangeDamageBonus;
        character.MagicDefence += MagicDamageBonus;
    }

    public void RemoveStats(Character character)
    {
        character.MeleeDamage -= MeleeDamageBonus;
        character.RangeDamage -= RangeDamageBonus;
        character.MagicDamage -= MagicDamageBonus;

        character.MeleeDefence -= MeleeDefenceBonus;
        character.RangeDefence -= RangeDamageBonus;
        character.MagicDefence -= MagicDamageBonus;
    }

    private void Awake()
    {
        _backpack = GetComponentInParent<Backpack>();
    }
}
