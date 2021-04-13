using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	Rigidbody2D rb;
	public float initialSpeed = 5;
	public float maxSpeed = 20;
	public float minSpeed = 2;

	private Vector2 minSpeedVector;
	public bool serving = true;
	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		// rb.velocity = new Vector2(initialSpeed, initialSpeed);
	}

	public void SetVelocity(Vector2 vector)
	{
		rb.velocity = vector;
	}

	public void FixedUpdate()
	{
		if (serving) return;
		if (rb.velocity.magnitude > maxSpeed) rb.velocity = rb.velocity.normalized * maxSpeed;
		if (Mathf.Abs(rb.velocity.x) < minSpeed)
		{
			if (rb.velocity.x < 0) rb.velocity = new Vector2(-minSpeed, rb.velocity.y);
			else rb.velocity = new Vector2(minSpeed, rb.velocity.y);
		}

		if (Mathf.Abs(rb.velocity.y) < minSpeed)
		{
			if (rb.velocity.y < 0) rb.velocity = new Vector2(rb.velocity.x, -minSpeed);
			else rb.velocity = new Vector2(rb.velocity.x, minSpeed);
		}
	}

	// public void OnCollisionEnter2D(Collision2D other)
	// {
	// 	if (other.transform.tag == "Player")
	// 	{
	// 		bool isLeft = other.transform.GetComponent<PaddleController>().leftPaddle;

	// 		if (isLeft && rb.velocity.x < 0) rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
	// 		if (!isLeft && rb.velocity.x > 0) rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
	// 	}
	// }
}
