using UnityEngine;

public class PlayerController : Controller
{
	private Camera _camera;

	private void Awake()
	{
		// Get main camera for getting mouse click ray positions
		_camera = Camera.main;
	}

	// This method turns on this controller. Then controller and game starts waiting for
	// player to start giving mouse inputs (choose some action to do)
	public override void TurnOnController()
	{
		Debug.Log($"Its {ControlledParty.ActiveCharacter.name} turn!");
		IsControllersTurn = true;
	}

	private void Update()
	{
		// If player clicks on some action button, invoke that action and action controls,
		// then turn of controller to avoid using more than one action at the time
		if (Input.GetMouseButtonDown(0) && IsControllersTurn)
		{
			RaycastHit2D rayHit = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(Input.mousePosition));

			if(rayHit.collider != null) 
			{
				if (rayHit.collider.gameObject.CompareTag("Action"))
				{
					rayHit.transform.GetComponent<IAction>().InvokeAction();
					IsControllersTurn = false;
				}
			}
		}
	}
}
