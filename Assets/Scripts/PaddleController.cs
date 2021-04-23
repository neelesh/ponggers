using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
	public GameObject paddleGO;
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

	private Vector3 defaultSize;
	private Vector3 bigPaddleSize;
	private Vector3 smallPaddleSize;

	private Vector3 targetScale;

	public GameManager gameManager;

	void Start()
	{
		defaultSize = paddleGO.transform.localScale;
		targetScale = defaultSize;

		bigPaddleSize = new Vector3(defaultSize.x, defaultSize.y * 2f, defaultSize.z); ;
		smallPaddleSize = new Vector3(defaultSize.x, defaultSize.y / 1.5f, defaultSize.z);

		rb = GetComponent<Rigidbody2D>();
		boxCollider = GetComponentInChildren<BoxCollider2D>();
		ballRB = ball.GetComponent<Rigidbody2D>();

		transform.position = new Vector2(transform.position.x, Random.Range(-3f, 3f));
	}

	void Update()
	{
		if (paddleGO.transform.localScale != targetScale) paddleGO.transform.localScale = Vector3.Lerp(paddleGO.transform.localScale, targetScale, Time.deltaTime * 10);

		if (Time.timeScale == 0) return;

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
		GameObject closestBall = gameManager.GetClosestBall(gameObject);
		ballRB = closestBall.GetComponent<Rigidbody2D>();

		float topY = boxBounds.center.y + boxBounds.extents.y;
		float bottomY = boxBounds.center.y - boxBounds.extents.y;

		if (isServing)
		{
			if (Camera.main.transform.position.y - 2 > topY) movement.y = 1;
			else if (Camera.main.transform.position.y + 2 < bottomY) movement.y = -1;
			return;
		}

		// The ball is moving away from us so lets pick a neutral position
		else if (leftPaddle && ballRB.velocity.x > 0)
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
		if (Vector2.Distance(closestBall.transform.position, gameObject.transform.position) > 10)
		{
			movement.y = 0;
			return;
		}

		if (closestBall.transform.position.y > topY) movement.y = 1;
		else if (closestBall.transform.position.y < bottomY) movement.y = -1;
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


	public void Grow()
	{
		// if (targetScale == smallPaddleSize) targetScale = defaultSize;
		// else if (targetScale == defaultSize) targetScale = bigPaddleSize;
		targetScale = bigPaddleSize;
	}

	public void Shrink()
	{
		// if (targetScale == defaultSize) targetScale = smallPaddleSize;
		// else if (targetScale == bigPaddleSize) targetScale = defaultSize;
		targetScale = smallPaddleSize;
	}

	public void NormalSize()
	{
		targetScale = defaultSize;
	}
}
