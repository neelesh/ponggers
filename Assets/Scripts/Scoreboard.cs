using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Scoreboard : MonoBehaviour
{
	public TextMeshProUGUI leftScoreText;
	public TextMeshProUGUI rightScoreText;

	public int leftScore = 0;
	public int rightScore = 0;

	public GameManager gameManager;

	void Start()
	{
		leftScoreText.text = leftScore.ToString();
		rightScoreText.text = rightScore.ToString();
	}

	public void LeftScoredGoal()
	{
		leftScore += 1;
		leftScoreText.text = leftScore.ToString();
	}

	public void RightScoredGoal()
	{
		rightScore += 1;
		rightScoreText.text = rightScore.ToString();
	}
}
