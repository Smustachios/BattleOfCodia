using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Party HeroParty;
    public Party MonsterParty;
    private Level _levelBuilder;
    private Battle _currentBattle;

    private void Awake()
    {
        _levelBuilder = gameObject.GetComponent<Level>();
        _currentBattle = gameObject.GetComponent<Battle>();
    }

    private void Start()
    {
        _levelBuilder.LevelOne();
    }
}
