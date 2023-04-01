using UnityEngine;

public class Food : MonoBehaviour, IItem
{
    public ItemType Type;
    public int HpRegen = 5;
    private Backpack _backpack;

    // Use item and finish turn with active character
    public void UseItem()
    {
        _backpack.Owner.ActiveCharacter.ConsumeFood(this);
        GameManager.UpdateBattleLog.Invoke($"{_backpack.Owner.ActiveCharacter.Name} healed {HpRegen} Hp!");
        _backpack.Owner.ResetCharacterFrameColor();
        _backpack.Owner.TakeCharacterAction();
    }

    // Destroy and remove item from backpack
    public void DestroyItem()
    {
        Destroy(gameObject);
        _backpack.RemoveItem(gameObject);
    }

    public ItemType ReturnItemType()
    {
        return Type;
    }

    private void Awake()
    {
        _backpack = GetComponentInParent<Backpack>();
    }
}