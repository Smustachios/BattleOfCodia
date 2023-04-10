using UnityEngine;

/// <summary>
/// Base for controllers
/// </summary>
public class Controller : MonoBehaviour
{
	public Party ControlledParty;
	public bool IsControllersTurn = false;
	protected Character _character;


	// Start character turn
	public virtual void TurnOnController(Character character) { }
}
