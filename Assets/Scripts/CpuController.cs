using UnityEngine;
using System.Threading;

public class CpuController : Controller
{
	// Let compu take turns with each character.
	public override void TurnOnController()
	{
		IsControllersTurn = true;
		Debug.Log($"Its {ControlledParty.ActiveCharacter.name} turn!");

		if (IsControllersTurn) 
		{
			ChooseAction();
		}

		IsControllersTurn = false;
		ControlledParty.TakeCharacterAction();
	}

	private void ChooseAction()
	{
		Character enemy = ControlledParty.enemyParty.CharacterList[0];
		ControlledParty.ActiveCharacter.GetComponent<Attack>().StartAttack(enemy);
	}
}
