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
				GameObject hittedObject = rayHit.collider.gameObject;

				// This is true if player chooses special attack
                if (hittedObject.CompareTag("Action"))
                {
					if (hittedObject.GetComponent<AttackButton>() != null)
					{
						hittedObject.GetComponent<IAction>().InvokeAction(ControlledParty.ActiveCharacter, this);
					}
					else if (hittedObject.GetComponent<SpecialAttackButton>() != null)
					{
                        if (ControlledParty.ActiveCharacter.GetComponent<SpecialAttack>().RemainingCooldown <= 0)
                        {
                            hittedObject.GetComponent<IAction>().InvokeAction(ControlledParty.ActiveCharacter, this);
                        }
                    }
					else if (hittedObject.GetComponent<IItem>() != null)
					{
						if (hittedObject.GetComponent<IItem>().Backpack == ControlledParty.Backpack &&
							hittedObject.GetComponent<IItem>().IsInBackpack)
						{
							hittedObject.GetComponent<IAction>().InvokeAction(ControlledParty.ActiveCharacter, this);
						}
					}
                }
            }
		}
	}
}
