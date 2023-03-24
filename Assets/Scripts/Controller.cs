using System;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	public DoNothingAction DoNothingAction; // Avaible actions to controller to choose from

	public Party ControlledParty; // Party ref this controller controls
	public bool IsControllersTurn = false;
	public delegate void OnTurnFinish();
	public static OnTurnFinish ControllerFinishedTurn;
	protected int _characterCount;

	// This is called in the beginning of the turn to start characters take turn to do some action
	public virtual void TurnOnController() { }
}
