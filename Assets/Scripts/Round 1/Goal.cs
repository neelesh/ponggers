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

	public PaddleController left;
	public PaddleController right;


	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.tag == "Ball")
		{
			Ball ball = other.gameObject.GetComponent<Ball>();

			if (leftGoal)
			{
				right.xp.Add(50);
			}
			else
			{
				left.xp.Add(50);
			}

			if (ball.fireball) ball.fireball = false;
			if (ball.firePFX.isPlaying) ball.firePFX.Stop();

			airhorn.pitch = Random.Range(.7f, 1.3f);
			airhorn.Play();

			other.gameObject.SetActive(false);
			gameManager.DestroyBallClones();

			if (leftGoal)
			{
				Instantiate(leftGoalPFX, other.transform.position, transform.rotation);
				scoreboard.RightScoredGoal();
				gameManager.SetupServeLeft();
			}
			else
			{
				Instantiate(rightGoalPFX, other.transform.position, transform.rotation);
				scoreboard.LeftScoredGoal();
				gameManager.SetupServeRight();
			}
		}
	}
}
