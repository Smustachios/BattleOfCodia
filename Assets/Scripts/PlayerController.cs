using UnityEngine;

public class PlayerController : Controller
{
	private Camera _camera;

	private void Awake()
	{
		_camera = Camera.main;
	}

	// Start taking turns of taking actions with each character
	public override void TurnOnController()
	{
		Debug.Log($"Its {ControlledParty.ActiveCharacter.name} turn!");
		IsControllersTurn = true;
	}

	private void Update()
	{
		// If player clicks on action, invoke action and change turn to next character.
		// If all characters have finish their turn, finish this controllers turn.
		// Fire controller change event to be handled by battle class.
		if (Input.GetMouseButtonDown(0) && IsControllersTurn)
		{
			RaycastHit2D rayHit = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(Input.mousePosition));

			if(rayHit.collider != null) 
			{
				if (rayHit.collider.gameObject.CompareTag("Action"))
				{
					rayHit.transform.GetComponent<IAction>().InvokeAction(ControlledParty.ActiveCharacter);
					IsControllersTurn = false;
				}
			}
		}
	}
}
