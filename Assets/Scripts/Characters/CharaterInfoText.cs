using UnityEngine;
using TMPro;
using System.Collections;

/// <summary>
/// Shows character hp and name.
/// Shows hp change on character if character heals or takes damage 
/// </summary>
public class CharaterInfoText : MonoBehaviour
{
	private TextMeshPro[] _infoTexts;
	private Character _parent;


	private void Awake()
	{
		_infoTexts = gameObject.GetComponentsInChildren<TextMeshPro>();
		_parent = GetComponentInParent<Character>();

	}

	private void Start()
	{
		_infoTexts[1].text = _parent.Name;
		_infoTexts[0].text = $"{_parent.MaxHp}/{_parent.MaxHp}";
	}

	// Update hp bar in the game
	public void UpdateHpBar()
	{
		_infoTexts[0].text = $"{_parent.CurrentHp}/{_parent.MaxHp}";
	}

	// Show hp change on character sprite for 1 sec. Heal is green, damage is red
	public IEnumerator ShowHpChange(int hpChange, Color color)
	{
		_infoTexts[2].color = color;
		_infoTexts[2].text = hpChange.ToString();
		yield return new WaitForSeconds(1);
		_infoTexts[2].text = null;
	}
}
