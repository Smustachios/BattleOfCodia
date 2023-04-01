using UnityEngine;
using TMPro;

public class BattleLog : MonoBehaviour
{
    private TMPro.TextMeshPro[] _battleTexts;
    private int _nextText;

    private void Awake()
    {
        _battleTexts = GetComponentsInChildren<TextMeshPro>();
        _nextText = _battleTexts.Length - 1;
        GameManager.UpdateBattleLog += UpdateBattleLog;
    }

    public void UpdateBattleLog(string text)
    {
        MoveBattleLog();
        _battleTexts[_battleTexts.Length - 1].text = text;
    }

    private void MoveBattleLog()
    {
        string text = _battleTexts[_battleTexts.Length - 1].text;

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
