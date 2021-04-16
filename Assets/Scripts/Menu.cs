using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

	public bool canPause = true;
	public bool gameIsPaused = false;
	public GameObject pauseMenuGO;

	private float timeScaleBeforePause = 1;

	public void Reload() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);
	public void LoadGameplay() => SceneManager.LoadScene("Gameplay");
	public void SetSinglePlayer() => GameData.Instance.SetSinglePlayer();
	public void SetTwoPlayer() => GameData.Instance.SetTwoPlayer();
	public void SetAIvsAI() => GameData.Instance.SetAIvsAI();


	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (pauseMenuGO == null) return;
			if (canPause == false) return;

			if (!gameIsPaused)
			{
				timeScaleBeforePause = Time.timeScale;
				pauseMenuGO.SetActive(true);
				Time.timeScale = 0f;
				gameIsPaused = true;
			}
			else
			{
				pauseMenuGO.SetActive(false);
				Time.timeScale = timeScaleBeforePause;
				gameIsPaused = false;
			}
		}
	}

	public void ResumeGame()
	{
		gameIsPaused = false;
	}

	public void SetCanPause(bool canPause)
	{
		this.canPause = canPause;
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
