using UnityEngine;
using System.Threading;

public class CpuController : Controller
{
	// Turn on controller for compu to take action with active character
	public override void TurnOnController()
	{
		IsControllersTurn = true;
		Debug.Log($"Its {ControlledParty.ActiveCharacter.name} turn!");

		if (IsControllersTurn) 
		{
			ChooseAction();
		}

		IsControllersTurn = false;
	}

	// Choose random character to attack and always use special attack if you can
	private void ChooseAction()
	{
		Character enemy = ControlledParty.EnemyParty.CharacterList[Random.Range(0, ControlledParty.EnemyParty.CharacterList.Count - 1)];

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
