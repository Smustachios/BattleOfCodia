using JetBrains.Annotations;
using UnityEngine;

public class AttackButton : MonoBehaviour, IAction
{
	public Battle CurrentBattle { get; private set; }
    private Character _enemy;
	private bool _enemyChosen;
    private Camera _camera;

	public void InvokeAction(params Character[] characters)
	{
        GetEnemy();
	}

	private void GetEnemy()
	{
		_enemyChosen = false;
	}

    private void AttackEnemy(Character enemy)
    {
        CurrentBattle.ActiveParty.ActiveCharacter.GetComponent<Attack>().StartAttack(enemy);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_enemyChosen)
        {
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(Input.mousePosition));

            if (rayHit.collider != null)
            {
                if (rayHit.collider.gameObject.CompareTag("Character") &&
                    rayHit.collider.gameObject.GetComponent<Party>() != CurrentBattle)
                {
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
