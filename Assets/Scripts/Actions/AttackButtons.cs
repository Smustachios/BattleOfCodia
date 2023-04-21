using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Updates attack buttons info in the beginning of the turn for a active character.
/// </summary>
public class AttackButtons : MonoBehaviour
{
	public StatsRenderer[] AttackStatRenderers;
	public SpriteRenderer[] AttackButtonFrames;

    private SpriteRenderer[] _attackSprites;
	

	private void Awake()
	{
		GetAttackButtonRenderers();
	}

    // Gets called before character can choose action
    // Update each button sprite to character skill sprite and update cooldown text
    // Update stats renderers and reset attack buttons to inactive color
    public void UpdateAttackButtons(Sprite basicAttack, Sprite specialAttack, int specialAttackCooldown,
        Dictionary<string, int> basicAttackStats, Dictionary<string, int> specialAttackStats)
	{
		_attackSprites[0].sprite = basicAttack;
		_attackSprites[1].sprite = specialAttack;

        AttackStatRenderers[0].UpdateStatsInfo(basicAttackStats);
        AttackStatRenderers[1].UpdateStatsInfo(specialAttackStats);

        // Show text and turn button to inactive if cooldown is active
        if (specialAttackCooldown > 0)
        {
            ColorUtility.TryParseHtmlString("#463D3D", out Color color);
            _attackSprites[1].color = color;
        }
        else
        {
            _attackSprites[1].color = Color.white;
        }

        foreach (SpriteRenderer frame in AttackButtonFrames)
        {
            frame.color = Color.white;
        }
    }

    // Gets renderes from child game objects that have action tag
	private void GetAttackButtonRenderers()
	{
        _attackSprites = new SpriteRenderer[2];

        int indxer = 0;

        foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            if (renderer.CompareTag("Action"))
            {
                _attackSprites[indxer++] = renderer;
            }
        }
    }
}
