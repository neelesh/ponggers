using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
	public float speed = 5;
	public Rigidbody2D rb;
	public BoxCollider2D boxCollider;
	public GameObject servePosition;
	Vector2 movement;
	public GameObject ball;
	public Rigidbody2D ballRB;

	public bool leftPaddle = true;
	public bool isAIPlayer = false;

	public bool isServing = false;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		boxCollider = GetComponentInChildren<BoxCollider2D>();
		ballRB = ball.GetComponent<Rigidbody2D>();

	}

	void Update()
	{

		if (isAIPlayer)
		{
			SimulatePlayer();
			return;
		}

		if (leftPaddle) movement.y = Input.GetAxisRaw("Vertical");
		else movement.y = Input.GetAxisRaw("Vertical2");
	}

	private void SimulatePlayer()
	{
		Bounds boxBounds = boxCollider.bounds;

		float topY = boxBounds.center.y + boxBounds.extents.y;
		float bottomY = boxBounds.center.y - boxBounds.extents.y;

		if (isServing)
		{
			if (Camera.main.transform.position.y - 2 > topY) movement.y = 1;
			else if (Camera.main.transform.position.y + 2 < bottomY) movement.y = -1;
			return;
		}

		// The ball is moving away from us so lets pick a neutral position
		if (leftPaddle && ballRB.velocity.x > 0)
		{
			if (Camera.main.transform.position.y > topY) movement.y = 1;
			else if (Camera.main.transform.position.y < bottomY) movement.y = -1;
			else movement.y = 0;
			return;
		}
		else if (!leftPaddle && ballRB.velocity.x < 0)
		{
			if (Camera.main.transform.position.y > topY) movement.y = 1;
			else if (Camera.main.transform.position.y < bottomY) movement.y = -1;
			else movement.y = 0;
			return;
		}

		// too far to bother
		if (Vector2.Distance(ball.transform.position, gameObject.transform.position) > 10)
		{
			movement.y = 0;
			return;
		}

		if (ball.transform.position.y > topY) movement.y = 1;
		else if (ball.transform.position.y < bottomY) movement.y = -1;
		// else movement.y = 0;
	}

	void OnDrawGizmos()
	{
		if (!isAIPlayer) return;

		// Get the top and bottom positions of the paddle
		Bounds boxBounds = boxCollider.bounds;

		float topY = boxBounds.center.y + boxBounds.extents.y;
		float bottomY = boxBounds.center.y - boxBounds.extents.y;

		Gizmos.color = Color.blue;
		Gizmos.DrawLine(new Vector3(transform.position.x, topY, 0), new Vector3(0, topY, 0));
		Gizmos.DrawLine(new Vector3(transform.position.x, bottomY, 0), new Vector3(0, bottomY, 0));
	}

	void FixedUpdate()
	{
		// rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
		rb.velocity = movement * speed;
	}

	public void PrepareServeBall(Ball ball)
	{
		isServing = true;
		ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
		ball.transform.parent = servePosition.transform;
		ball.transform.position = servePosition.transform.position;
		ball.SetVelocity(Vector2.zero);
		ball.gameObject.SetActive(true);
	}

	public void ServeBall(Ball ball)
	{
		ball.serving = false;
		ball.gameObject.transform.parent = null;
		ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

		float yVelocity = -speed * .5f;
		if (rb.velocity.y != 0) yVelocity = rb.velocity.y * .5f;

		if (leftPaddle) ball.SetVelocity(new Vector2(ball.initialSpeed, yVelocity));
		else ball.SetVelocity(new Vector2(-ball.initialSpeed, yVelocity));

		isServing = false;
	}
}
