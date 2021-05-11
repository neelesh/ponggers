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

	public GameObject LeftSkillTree;
	public GameObject RightSkillTree;

	public void Reload() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);
	public void LoadGameplay() => SceneManager.LoadScene("Gameplay");
	public void SetSinglePlayer() => GameData.Instance.SetSinglePlayer();
	public void SetTwoPlayer() => GameData.Instance.SetTwoPlayer();
	public void SetAIvsAI() => GameData.Instance.SetAIvsAI();

	public void TogglePause()
	{
		if (canPause == false) return;

		if (pauseMenuGO == null) return;

		if (!gameIsPaused)
		{
			timeScaleBeforePause = Time.timeScale;
			pauseMenuGO.SetActive(true);
			Time.timeScale = 0f;
			gameIsPaused = true;

			LeftSkillTree.gameObject.transform.localScale = Vector3.one;
			RightSkillTree.gameObject.transform.localScale = Vector3.one;
		}
		else
		{
			pauseMenuGO.SetActive(false);
			Time.timeScale = timeScaleBeforePause;
			gameIsPaused = false;

			LeftSkillTree.gameObject.transform.localScale = Vector3.zero;
			RightSkillTree.gameObject.transform.localScale = Vector3.zero;
		}
	}

	public void ResumeGame()
	{
		LeftSkillTree.gameObject.transform.localScale = Vector3.zero;
		RightSkillTree.gameObject.transform.localScale = Vector3.zero;

		gameIsPaused = false;
		pauseMenuGO.SetActive(false);
		Time.timeScale = timeScaleBeforePause;
		canPause = true;
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
