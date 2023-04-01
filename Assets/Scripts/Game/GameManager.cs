using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Party HeroParty;
    public Party MonsterParty;
    private Level _levelBuilder;
	private int _level = 0;

    private void Awake()
    {
        _levelBuilder = gameObject.GetComponent<Level>();
    }

    private void Start()
    {
		_level++;
		_levelBuilder.StartLevel(_level);
    }

	// Incriment and start new level
	public void ChangeLevel()
	{
		_level++;
		_levelBuilder.StartLevel(_level);
	}
}
