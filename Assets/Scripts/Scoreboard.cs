using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Scoreboard : MonoBehaviour
{
	public TextMeshProUGUI leftScoreText;
	public TextMeshProUGUI rightScoreText;

	private int leftScore = 0;
	private int rightScore = 0;

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
