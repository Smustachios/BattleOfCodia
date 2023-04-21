using UnityEngine;
using TMPro;
using System.Collections;

/// <summary>
/// Shows and updates battle log text.
/// </summary>
public class BattleLog : MonoBehaviour
{
	public TextMeshPro ActionText;
    private TextMeshPro[] _battleTexts;


    private void Awake()
    {
        _battleTexts = GetComponentsInChildren<TextMeshPro>();
    }

	private void Start()
	{
		GameManager.UpdateBattleLog += UpdateBattleLog;
		GameManager.UpdateActionText += ShowActionText;
	}

	// Battle log is updated when event is fired
	public void UpdateBattleLog(string text)
    {
        MoveBattleLog();
        _battleTexts[^1].text = text;
    }

	// Show action text on the scene
	public IEnumerable ShowActionText(string text)
	{
		ActionText.text = text;
		yield return new WaitForSeconds(1);
	}

	// Moves all old text along battle log, so last action would always
	// be last text in the log
    private void MoveBattleLog()
    {
        string text = _battleTexts[^1].text;

        for (int i = _battleTexts.Length - 1; i > 0; i--)
        {
            string nextText = _battleTexts[i - 1].text;
            _battleTexts[i - 1].text = text;
            text = nextText;
        }
    }

    private void OnDisable()
    {
        GameManager.UpdateBattleLog -= UpdateBattleLog;
		GameManager.UpdateActionText -= ShowActionText;
	}
}
