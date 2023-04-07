using UnityEngine;

/// <summary>
/// Invokes special attack button sequence. If player clicks on enemy after sequence start
/// starts attacking enemy. If player clicks somewhere else then sequence will canceled.
/// </summary>
public class SpecialAttackButton : MonoBehaviour, IAction
{
    public SpriteRenderer Frame;

    private Camera _camera;
    private Controller _controller;

    private bool _enemyChosen;
    private bool _specialAttackChosen;


    private void Awake()
    {
        _camera = Camera.main;
    }

    // Start sequence
    public void InvokeAction(Character character, Controller controller)
    {
        _controller = controller;

        _enemyChosen = false;
        _specialAttackChosen = true;
        Frame.color = Color.green; // Turn attack button to active color
    }

    // Wait for player to click something
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_enemyChosen && _specialAttackChosen)
        {
            RaycastHit2D clickedObject = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(Input.mousePosition));

            if (clickedObject.collider != null)
            {
                // Clicked on enemy
                if (clickedObject.collider.CompareTag("Character") &&
                    clickedObject.collider.GetComponent<Character>().ParentParty != _controller.ControlledParty)
                {
                    _enemyChosen = true;
                    _specialAttackChosen = false;
                    Frame.color = Color.white; // Turn attack button to inactive color

                    AttackEnemy(clickedObject.collider.GetComponent<Character>(), _controller);
                }
                // Cancel sequence
                else
                {
                    _enemyChosen = false;
                    _specialAttackChosen = false;
                    Frame.color = Color.white; // Turn attack button to inactive color
                }
            }
        }
    }

    // If player has clicked on chosen enemy start attacking it
    private void AttackEnemy(Character enemy, Controller controller)
    {
        _controller.ControlledParty.ActiveCharacter.GetComponent<SpecialAttack>().AttackTarget(enemy, controller);
    }
}
