using JetBrains.Annotations;
using UnityEngine;

public class AttackButton : MonoBehaviour, IAction
{
	public Battle CurrentBattle { get; private set; }
    private Character _enemy;
	private bool _enemyChosen;
    private Camera _camera;

    // If player clicks on attack button in game, start waiting for player to click
    // on enemy it wants to attack
	public void InvokeAction()
	{
        GetEnemy();
	}

    // Change condition of enemy chosen to false to start waiting for update for
    // player clicks on enemy characters
	private void GetEnemy()
	{
		_enemyChosen = false;
	}

    // If player has clicked on chosen enemy start attacking it
    private void AttackEnemy(Character enemy)
    {
        CurrentBattle.ActiveParty.ActiveCharacter.GetComponent<Attack>().StartAttack(enemy);
    }

    private void Update()
    {
        // Look for clicks on enemy characters while attack action is ongoing
        if (Input.GetMouseButtonDown(0) && !_enemyChosen)
        {
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(Input.mousePosition));

            if (rayHit.collider != null)
            {
                // You can choose your own characters as targets too! Because some characters
                // might need to heal own party members, its a must. But that also means you can attack
                // yourself or your party members.. 
                if (rayHit.collider.gameObject.CompareTag("Character"))
                {
                    // Update enemy if player has clicked on one and attack it
                    Character enemy = rayHit.transform.GetComponent<Character>();
                    _enemyChosen = true;
                    AttackEnemy(enemy);
                }
            }
        }
    }

    private void Awake()
	{
		CurrentBattle = GameObject.Find("GameManager").GetComponent<Battle>();
        _camera = Camera.main;
	}
}
