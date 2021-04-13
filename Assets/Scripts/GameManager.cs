using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public Ball ball;

	public PaddleController leftPaddle;
	public PaddleController rightPaddle;
	public TMPro.TextMeshProUGUI countDown;

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
		ball.serving = true;
		paddle.PrepareServeBall(ball);
		StartCoroutine(ServeBallCo(paddle));
	}

	IEnumerator ServeBallCo(PaddleController paddle)
	{
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
