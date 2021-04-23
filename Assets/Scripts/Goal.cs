using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
	public Scoreboard scoreboard;
	public bool leftGoal = true;
	public GameManager gameManager;

	public GameObject leftGoalPFX;
	public GameObject rightGoalPFX;

	public AudioSource airhorn;

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "Ball")
		{
			airhorn.Play();

			other.gameObject.SetActive(false);
			gameManager.DestroyBallClones();

			if (leftGoal)
			{
				Instantiate(leftGoalPFX, other.contacts[0].point, transform.rotation);
				scoreboard.RightScoredGoal();
				gameManager.SetupServeLeft();
			}
			else
			{
				Instantiate(rightGoalPFX, other.contacts[0].point, transform.rotation);
				scoreboard.LeftScoredGoal();
				gameManager.SetupServeRight();
			}
		}
	}
}
