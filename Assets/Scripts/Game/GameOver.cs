using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	public TextMeshPro GameOverText;

	private string[] _gameOverText = { "Game Over..", "", "Do you want to play another game?" };


	private void Awake()
	{
		gameObject.SetActive(false);
	}


	public void ShowGameOver(string gameResult)
	{
		gameObject.SetActive(true);

		_gameOverText[1] = gameResult;

		GameOverText.text = "";
		GameOverText.text = $"{_gameOverText[0]}\n{_gameOverText[1]}\n{_gameOverText[2]}";
	}

	private void OnMouseDown()
	{
		SceneManager.LoadScene("MainScene");
	}
}
