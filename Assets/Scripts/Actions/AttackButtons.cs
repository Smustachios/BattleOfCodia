using UnityEngine;
using TMPro;

public class AttackButtons : MonoBehaviour
{
	private SpriteRenderer[] _attackSprites; // All attack botton renderer components
	[SerializeField] private TextMeshPro _specialAttackCooldown; // This shows special attack cooldown as text in the scene
	public StatsRenderer[] StatsRenderers { get; private set; }

	private void Awake()
	{
		_attackSprites = new SpriteRenderer[2];

		Transform attack = transform.Find("BasicAttack");
		Transform specialAttack = transform.Find("SpecialAttack");

		StatsRenderers = new StatsRenderer[2];
		StatsRenderers[0] = attack.GetComponentInChildren<StatsRenderer>();
		StatsRenderers[1] = specialAttack.GetComponentInChildren<StatsRenderer>();

		// Get all the renderes on the attack buttons object in game and put all
		// attack button sprite renderes in the list
		int counter = 0;
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
		_attackSprites[0].sprite = basicAttack;
		_attackSprites[1].sprite = specialAttack;

		if (specialAttackCooldown == 0) 
		{
			_attackSprites[1].color = Color.white;
			_specialAttackCooldown.text = null;
		}
		else
		{
			ColorUtility.TryParseHtmlString("#463D3D", out Color color);
			_attackSprites[1].color = color;
			_specialAttackCooldown.text = specialAttackCooldown.ToString();
		}
	}
}
