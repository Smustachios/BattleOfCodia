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

	private void ChooseAction()
	{
		Character enemy = ControlledParty.EnemyParty.CharacterList[0];
		ControlledParty.ActiveCharacter.GetComponent<Attack>().StartAttack(enemy);
	}
}
