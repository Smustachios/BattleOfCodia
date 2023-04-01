using UnityEngine;
using TMPro;

public class AttackButtons : MonoBehaviour
{
	private SpriteRenderer[] _attackSprites; // All attack botton renderer components
	private TextMeshPro _specialAttackCooldown; // This shows special attack cooldown as text in the scene

	private void Awake()
	{
		_attackSprites = new SpriteRenderer[2];
		_specialAttackCooldown = GetComponentInChildren<TextMeshPro>();

		int counter = 0;

		// Get all the renderes on the attack buttons object in game and put all
		// attack button sprite renderes in the list
		foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
		{
			if (renderer.CompareTag("Action"))
			{
				_attackSprites[counter] = renderer;
				counter++;
			}
		}
	}

	// Take character spell sprites and update attack buttons object sprites to
	// character specific sprites and update cooldown text
	public void UpdateAttackButtons(Sprite basicAttack, Sprite specialAttack, int specialAttackCooldown)
	{
		_attackSprites[0].sprite = specialAttack;
		_attackSprites[1].sprite = basicAttack;
		_specialAttackCooldown.text = specialAttackCooldown.ToString();
	}
}
