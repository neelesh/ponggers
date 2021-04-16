using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public void Reload() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

	public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);

	public void LoadGameplay()
	{
		SceneManager.LoadScene("Gameplay");
	}

	public void SetSinglePlayer() => GameData.Instance.SetSinglePlayer();
	public void SetTwoPlayer() => GameData.Instance.SetTwoPlayer();
	public void SetAIvsAI() => GameData.Instance.SetAIvsAI();
}
