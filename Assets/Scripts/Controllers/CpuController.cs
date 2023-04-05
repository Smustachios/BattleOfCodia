using UnityEngine;
using System.Collections;

public class CpuController : Controller
{
	// Turn on controller for compu to take action with active character
	public override void TurnOnController()
	{
		IsControllersTurn = true;

		if (IsControllersTurn) 
		{
			StartCoroutine(ChooseAction());
		}

		IsControllersTurn = false;
	}

	// Choose random character to attack and always use special attack if you can
	private IEnumerator ChooseAction()
	{
        yield return new WaitForSeconds(2.5f);

		Character character = ControlledParty.ActiveCharacter;
		Backpack backpack = ControlledParty.Backpack;

		if (character.MaxHp / 2 > character.CurrentHp && Random.value > 0.5f && backpack.Foods.Count > 0)
		{
			IItem food = backpack.Foods[Random.Range(0, backpack.Foods.Count)].GetComponent<IItem>();

			food.UseItem();
			food.DestroyItem();
		}
		else if (backpack.Weapons.Count != 0 && Random.value > 0.75f && backpack.Weapons.Count > 0 && character.EquipedItem == null)
		{
			IItem weapon = backpack.Weapons[Random.Range(0, backpack.Weapons.Count)].GetComponent<IItem>();

			weapon.UseItem();
        }

		else if (backpack.Gear.Count != 0 && Random.value > 0.75f && backpack.Gear.Count > 0 && character.EquipedItem == null)
		{
            IItem item = backpack.Gear[Random.Range(0, backpack.Gear.Count)].GetComponent<IItem>();

            item.UseItem();
        }
		else
		{
            Character enemy = ControlledParty.EnemyParty.CharacterList[Random.Range(0, ControlledParty.EnemyParty.CharacterList.Count)];

            if (ControlledParty.ActiveCharacter.GetComponent<SpecialAttack>().RemainingCooldown <= 0)
            {
                ControlledParty.ActiveCharacter.GetComponent<SpecialAttack>().StartAttack(enemy);
            }
            else
            {
                ControlledParty.ActiveCharacter.GetComponent<Attack>().StartAttack(enemy);
            }
        }
	}
}
