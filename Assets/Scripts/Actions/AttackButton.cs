using UnityEngine;

public class AttackButton : MonoBehaviour, IAction
{
	public Battle CurrentBattle { get; private set; }
	private bool _enemyChosen;
    private bool _attackChosen;
    private Camera _camera;
	public SpriteRenderer Frame;

    // If player clicks on attack button in game, start waiting for player to click
    // on enemy it wants to attack
	public void InvokeAction(Character character)
	{
        GetEnemy();
		Frame.color = Color.green;
	}

    // Change condition of enemy chosen to false to start waiting for update for
    // player to choose target enemy
	private void GetEnemy()
	{
		_enemyChosen = false;
        _attackChosen = true;
	}

    // If player has clicked on chosen enemy start attacking it
    private void AttackEnemy(Character enemy)
    {
        CurrentBattle.ActiveParty.ActiveCharacter.GetComponent<Attack>().StartAttack(enemy);
    }

    private void Update()
    {
        // Look for clicks on enemy characters while attack action is ongoing
        if (Input.GetMouseButtonDown(0) && !_enemyChosen && _attackChosen)
        {
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(Input.mousePosition));

            if (rayHit.collider != null)
            {
                // You can choose your own characters as targets too! Because some characters
                // might need to heal own party members, its a must. But that also means you can attack
                // yourself or your party members.. 
                if (rayHit.collider.gameObject.CompareTag("Character"))
                {
                    // Get enemy character player clicked on and attack it
                    Character enemy = rayHit.transform.GetComponent<Character>();

                    _enemyChosen = true;
                    _attackChosen = false;

					Frame.color = Color.white;

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
