using UnityEngine;

/// <summary>
/// Player controller
/// </summary>
public class PlayerController : Controller
{
	private Camera _camera;

	private void Awake()
	{
		_camera = Camera.main;
	}

	// Take character action
	public override void TurnOnController(Character character)
	{
		_character = character;
		IsControllersTurn = true;
	}

	private void Update()
	{
		// Player chooses action by clicking on button or a item in the backpack
		if (Input.GetMouseButtonDown(0) && IsControllersTurn)
		{
			RaycastHit2D rayHit = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(Input.mousePosition));

			if(rayHit.collider != null) 
			{
				GameObject clickedObject = rayHit.collider.gameObject;

				if (clickedObject.CompareTag("Action"))
                {
					// Check what action player choose and finish with character
					CheckIfAttack(clickedObject);
					CheckIfSpecialAttack(clickedObject);
					CheckIfItem(clickedObject);
                }
            }
		}
	}

	private bool CheckIfAttack(GameObject button)
	{
		Debug.Log(1);
		if (button.GetComponent<AttackButton>() != null)
		{
			button.GetComponent<IAction>().InvokeAction(_character, this);
			return true;
		}
		return false;
	}

	private bool CheckIfSpecialAttack(GameObject button)
	{
		Debug.Log(2);
		if (button.GetComponent<SpecialAttackButton>() != null)
		{
			if (_character.GetComponent<SpecialAttack>().RemainingCooldown <= 0)
			{
				button.GetComponent<IAction>().InvokeAction(_character, this);
				return true;
			}
			return false;
		}
		return false;
	}

	private bool CheckIfItem(GameObject button)
	{
		Debug.Log(3);
		if (button.GetComponent<IItem>() != null)
		{
			if (button.GetComponent<IItem>().Backpack == ControlledParty.Backpack &&
				button.GetComponent<IItem>().IsInBackpack)
			{
				button.GetComponent<IAction>().InvokeAction(_character, this);
				return true;
			}
			return false;
		}
		return false;
	}
}
