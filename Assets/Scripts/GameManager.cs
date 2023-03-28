using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Party HeroParty;
    public Party MonsterParty;
    private Level _levelBuilder;
    private Battle _currentBattle;
	private int _currentLevel = 0;

    private void Awake()
    {
        _levelBuilder = gameObject.GetComponent<Level>();
        _currentBattle = gameObject.GetComponent<Battle>();
    }

    private void Start()
    {
		_currentLevel++;
        _levelBuilder.LevelOne();
		_currentBattle.StartBattle();
    }

	public void ChangeLevel()
	{
		_currentLevel++;
		_levelBuilder.LevelTwo();
	}
}
