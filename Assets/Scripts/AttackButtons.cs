using UnityEngine;

public class AttackButtons : MonoBehaviour
{
	private SpriteRenderer[] _attackSprites;

	private void Awake()
	{
		_attackSprites = new SpriteRenderer[2];
		int c = 0;

		foreach (var r in GetComponentsInChildren<SpriteRenderer>())
		{
			if (r.CompareTag("Action"))
			{
				_attackSprites[c] = r;
				c++;
			}
		}
	}

	public void UpdateAttackButtons(Sprite basicAttack, Sprite specialAttack)
	{
		_attackSprites[0].sprite = specialAttack;
		_attackSprites[1].sprite = basicAttack;
	}
}
