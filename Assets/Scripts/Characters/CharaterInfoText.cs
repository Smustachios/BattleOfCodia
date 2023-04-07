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
		_infoTexts = gameObject.GetComponentsInChildren<TextMeshPro>(); // All text components on character object
		_parent = GetComponentInParent<Character>(); // This is character whos text are to update

		_infoTexts[1].text = _parent.Name; // Set name text
		_infoTexts[0].text = $"{_parent.MaxHp}/{_parent.MaxHp}"; // Set inital hp text
	}

	// Take in updated hp and update hp text box in the game
	public void UpdateHPText(string newHp)
	{
		_infoTexts[0].text = $"{newHp}/{_parent.MaxHp}";
	}

	public IEnumerator UpdateHpChangeText(int value, Color color)
	{
		_infoTexts[2].color = color;
		_infoTexts[2].text = value.ToString();
		yield return new WaitForSeconds(1);
		_infoTexts[2].text = null;
	}
}
