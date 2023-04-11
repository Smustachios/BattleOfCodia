using UnityEngine;
using TMPro;

/// <summary>
/// Shows and updates battle log text.
/// </summary>
public class BattleLog : MonoBehaviour
{
    private TextMeshPro[] _battleTexts;


    private void Awake()
    {
        _battleTexts = GetComponentsInChildren<TextMeshPro>();
        
    }

	private void Start()
	{
		GameManager.UpdateBattleLog += UpdateBattleLog;
	}

	// Battle log is updated when event is fired
	public void UpdateBattleLog(string text)
    {
        MoveBattleLog();
        _battleTexts[^1].text = text;
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
    }
}
