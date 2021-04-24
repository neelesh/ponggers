using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{

	public static GameData Instance;

	public enum GameMode { SinglePlayer, TwoPlayer, AIvsAI }
	public GameMode gameMode;

	public float MusicVolume { get; internal set; } = 0.6f;
	public float SfxVolume { get; internal set; } = 0.6f;

	public void SetSinglePlayer() => gameMode = GameMode.SinglePlayer;
	public void SetTwoPlayer() => gameMode = GameMode.TwoPlayer;
	public void SetAIvsAI() => gameMode = GameMode.AIvsAI;

	public GameMode GetGameMode() { return gameMode; }

	void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
	}
}
