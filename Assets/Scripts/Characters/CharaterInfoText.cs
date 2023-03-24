using UnityEngine;
using TMPro;

public class CharaterInfoText : MonoBehaviour
{
	private TextMeshPro[] _infoTexts;
	private Character _parent;

	private void Awake()
	{
		_infoTexts = gameObject.GetComponentsInChildren<TextMeshPro>(); // All text components on character object
		_parent = GetComponentInParent<Character>(); // This is character whos text are to update

		_infoTexts[1].text = _parent.name; // Set name text
		_infoTexts[0].text = $"{_parent.MaxHp}/{_parent.MaxHp}"; // Set inital hp text
	}

	public void UpdateHPText(string newHp)
	{
		_infoTexts[0].text = $"{newHp}/{_parent.MaxHp}";
	}
}
