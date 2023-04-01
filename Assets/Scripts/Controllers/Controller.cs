using UnityEngine;

public class Controller : MonoBehaviour
{
	public Party ControlledParty; // What party this controller controls
	public bool IsControllersTurn = false;

	// This is called when each character starts its turn in the battle
	public virtual void TurnOnController() { }
}
