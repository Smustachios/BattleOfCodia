using UnityEngine;

/// <summary>
/// Invokes attack button sequence. If player clicks on enemy after sequence start
/// starts attacking enemy. If player clicks somewhere else then sequence will canceled.
/// </summary>
public class AttackButton : MonoBehaviour, IAction
{
    public SpriteRenderer Frame;

    private Controller _controller;
    private Camera _camera;

    private bool _enemyChosen;
    private bool _attackChosen;


    private void Awake()
    {
        _camera = Camera.main;
    }

    // Start sequence
    public void InvokeAction(Character character, Controller controller)
	{
        _controller = controller;

        _enemyChosen = false;
        _attackChosen = true;
		Frame.color = Color.green; // Turn attack button to active color
	}

    // Wait for player to click something
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_enemyChosen && _attackChosen)
        {
            RaycastHit2D clickedObject = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(Input.mousePosition));

            if (clickedObject.collider != null)
            {
                if (clickedObject.collider.CompareTag("Character") &&
                    clickedObject.collider.GetComponent<Character>().ParentParty != _controller.ControlledParty)
                {
                    _enemyChosen = true;
                    _attackChosen = false;
					Frame.color = Color.white; // Turn attack button to inactive color

                    AttackEnemy(clickedObject.collider.GetComponent<Character>(), _controller);
                }
                else
                {
                    _enemyChosen = false;
                    _attackChosen = false;
                    Frame.color = Color.white; // Turn attack button to inactive color
                }
			}
        }
    }

    private void AttackEnemy(Character enemy, Controller controller)
    {
        _controller.ControlledParty.ActiveCharacter.GetComponent<Attack>().AttackTarget(enemy, controller);
    }
}
