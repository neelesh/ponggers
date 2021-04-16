using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public Ball ball;

	public PaddleController leftPaddle;
	public PaddleController rightPaddle;
	public TMPro.TextMeshProUGUI countDown;

	public GameManager Instance;

	public bool leftHasWon = false;
	public bool rightHasWon = false;

	public Scoreboard scoreboard;

	public GameObject replayMenu;

	void Awake()
	{
		Time.timeScale = 1f;
		if (Instance != null && Instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			Instance = this;
			// DontDestroyOnLoad(this.gameObject);
		}
	}

	void Start()
	{
		if (GameData.Instance.gameMode == GameData.GameMode.SinglePlayer)
		{
			leftPaddle.isAIPlayer = false;
			rightPaddle.isAIPlayer = true;
		}
		if (GameData.Instance.gameMode == GameData.GameMode.TwoPlayer)
		{
			leftPaddle.isAIPlayer = false;
			rightPaddle.isAIPlayer = false;
		}
		if (GameData.Instance.gameMode == GameData.GameMode.AIvsAI)
		{
			leftPaddle.isAIPlayer = true;
			rightPaddle.isAIPlayer = true;
		}
		SetupServeLeft();
	}

	public void SetupServeLeft() => SetupServe(leftPaddle);
	public void SetupServeRight() => SetupServe(rightPaddle);

	public void SetupServe(PaddleController paddle)
	{
		ball.lastPlayer = paddle;
		if (scoreboard.rightScore == 11) StartCoroutine(PlayerWins("BLUE WINS!"));
		else if (scoreboard.leftScore == 11) StartCoroutine(PlayerWins("RED WINS!"));
		else
		{
			ball.serving = true;
			paddle.PrepareServeBall(ball);
			StartCoroutine(ServeBallCo(paddle));
		}
	}

	IEnumerator PlayerWins(String message)
	{
		yield return new WaitForSeconds(.5f);
		Time.timeScale = .1f;
		countDown.text = message;

		replayMenu.SetActive(true);

		// yield return new WaitForSeconds(.1f);

		StartCoroutine(SlowTimeToAStop());
	}

	IEnumerator SlowTimeToAStop()
	{
		while (Time.timeScale > 0)
		{
			yield return new WaitForSeconds(.01f);
			Time.timeScale = Time.timeScale - 0.002f;
		}
	}

	IEnumerator ServeBallCo(PaddleController paddle)
	{
		if (!(scoreboard.leftScore == 0 && scoreboard.rightScore == 0))
		{
			yield return new WaitForSeconds(.5f);
			Time.timeScale = .2f;
			countDown.text = paddle.leftPaddle ? "BLUE SCORED" : "RED SCORED";
			yield return new WaitForSeconds(.3f);

			Time.timeScale = 1;
		}

		countDown.text = "3";
		yield return new WaitForSeconds(.5f);
		countDown.text = "2";
		yield return new WaitForSeconds(.5f);
		countDown.text = "1";
		yield return new WaitForSeconds(.5f);
		countDown.text = "";

		paddle.ServeBall(ball);
	}


}
