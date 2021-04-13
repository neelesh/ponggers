using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
	public Scoreboard scoreboard;
	public bool leftGoal = true;
	public GameManager gameManager;

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "Ball")
		{
			other.gameObject.SetActive(false);

			if (leftGoal)
			{
				scoreboard.RightScoredGoal();
				gameManager.SetupServeLeft();
			}
			else
			{
				scoreboard.LeftScoredGoal();
				gameManager.SetupServeRight();
			}
		}
	}
}
