using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	public GameObject ballHitFX;

	Rigidbody2D rb;
	public float initialSpeed = 5;
	public float maxSpeed = 20;
	public float minSpeed = 2;


	private Vector2 minSpeedVector;
	public bool serving = true;

	private Vector3 scale;

	public PaddleController lastPlayer;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		// rb.velocity = new Vector2(initialSpeed, initialSpeed);

		scale = transform.localScale;

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

	public void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag != "Goal") Instantiate(ballHitFX, other.contacts[0].point, transform.rotation);

		if (other.gameObject.tag == "Player") lastPlayer = other.gameObject.GetComponentInParent<PaddleController>();
	}
}