using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
	public GameObject target;

	public bool canCollideWithPlayer = false;
	public bool beenHit = false;

	public float speed = 20;

	public bool grow;
	public bool shrink;
	public bool speedUp;

	public SpriteRenderer spriteRenderer;

	Ball ball;

	public Sprite growSymbol;
	public Sprite shrinkSymbol;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();

		if (grow) spriteRenderer.sprite = growSymbol;
		if (shrink) spriteRenderer.sprite = shrinkSymbol;
	}

	void FixedUpdate()
	{
		if (target == null) return;
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Ball" && beenHit == false)
		{
			beenHit = true;
			canCollideWithPlayer = true;

			ball = other.gameObject.GetComponent<Ball>();
			target = ball.lastPlayer.gameObject;
		}

		if (other.gameObject.tag == "Player" && beenHit && canCollideWithPlayer)
		{
			// spawn a particle effect
			// spawn sound effect

			PaddleController paddle = ball.lastPlayer;
			if (paddle == null) return;

			ApplyPowerUp(paddle);

			gameObject.SetActive(false);
		}
	}

	public void ApplyPowerUp(PaddleController paddle)
	{
		if (grow) paddle.Grow();
		if (shrink) paddle.Shrink();
	}
}
