using JetBrains.Annotations;
using UnityEngine;

public class AttackButton : MonoBehaviour, IAction
{
	public Battle CurrentBattle;

	public void InvokeAction(params Character[] characters)
	{
		CurrentBattle.ActiveParty.ActiveCharacter.GetComponent<Attack>().StartAttack(characters[0]);
	}

	private void Awake()
	{
		CurrentBattle = GameObject.Find("GameManager").GetComponent<Battle>();
	}
}
