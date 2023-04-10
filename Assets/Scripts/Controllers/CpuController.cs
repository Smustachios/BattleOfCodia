using UnityEngine;
using System.Collections;

/// <summary>
/// Computer character controller
/// </summary>
public class CpuController : Controller
{
	// Take character action
	public override void TurnOnController(Character character)
	{
		_character = character;
		IsControllersTurn = true;
		StartCoroutine(ChooseAction());
	}

	// Choose computer action randomly
	private IEnumerator ChooseAction()
	{
        yield return new WaitForSeconds(2.5f); // Computer "thinks"

		Backpack backpack = ControlledParty.Backpack;
		int itemCount = backpack.Weapons.Count + backpack.Foods.Count;

		// Computer consume food with 50% chance if hp is less half of max and food is avaible
		if (_character.MaxHp / 2 > _character.CurrentHp && Random.value > 0.5f && backpack.Foods.Count > 0)
		{
			IItem food = backpack.Foods[Random.Range(0, backpack.Foods.Count)].GetComponent<IItem>();

			food.InvokeAction(_character, this);
		}

		// Computer equips item 25% chance if item is avaible and nothing is equiped yet
		else if (itemCount > 0 && Random.value > 0.75f && _character.EquipedItem == null)
		{
			if (Random.Range(0, 2) > 0)
			{
				IItem weapon = backpack.Weapons[Random.Range(0, backpack.Weapons.Count)].GetComponent<IItem>();

				weapon.InvokeAction(_character, this);
			}
			else
			{
				IItem item = backpack.Gear[Random.Range(0, backpack.Gear.Count)].GetComponent<IItem>();

				item.InvokeAction(_character, this);
			}
			
        }

		// Otherwise attack enemy
		else
		{
            Character enemy = ControlledParty.EnemyParty.CharacterList[Random.Range(0, ControlledParty.EnemyParty.CharacterList.Count)];

			// Always use special attack if avaible
            if (ControlledParty.ActiveCharacter.GetComponent<SpecialAttack>().RemainingCooldown <= 0)
            {
                ControlledParty.ActiveCharacter.GetComponent<SpecialAttack>().AttackTarget(enemy, this);
            }
            else
            {
                ControlledParty.ActiveCharacter.GetComponent<Attack>().AttackTarget(enemy, this);
            }
        }
	}
}
