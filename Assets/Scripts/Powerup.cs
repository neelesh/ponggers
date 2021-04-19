using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
	public PowerUpSpawner powerUpSpawner;

	public GameObject target;

	public GameObject top;
	public GameObject bottom;


	public bool canCollideWithPlayer = false;
	public bool beenHit = false;


	public float floatingSpeed = 2;
	public float speed = 20;


	public bool grow;
	public bool shrink;
	public bool speedUp;

	public bool topLeftAvantage;
	public bool bottomLeftAvantage;
	public bool topRightAvantage;
	public bool bottomRightAvantage;

	public SpriteRenderer spriteRenderer;

	Ball ball;

	public Sprite growSymbol;
	public Sprite shrinkSymbol;

	public Sprite topLeftAvantageSprite;
	public Sprite bottomLeftAvantageSprite;
	public Sprite topRightAvantageSprite;
	public Sprite bottomRightAvantageSprite;

	void Start()
	{

		if (grow) spriteRenderer.sprite = growSymbol;
		if (shrink) spriteRenderer.sprite = shrinkSymbol;

		if (topLeftAvantage) spriteRenderer.sprite = topLeftAvantageSprite;
		if (bottomLeftAvantage) spriteRenderer.sprite = bottomLeftAvantageSprite;
		if (topRightAvantage) spriteRenderer.sprite = topRightAvantageSprite;
		if (bottomRightAvantage) spriteRenderer.sprite = bottomRightAvantageSprite;
	}

	void FixedUpdate()
	{
		if (target == null)
		{
			float smallStep = floatingSpeed * Time.deltaTime;
			Vector3 up = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
			transform.position = Vector3.MoveTowards(transform.position, up, smallStep);
			return;
		}
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "RespawnPowerUp") powerUpSpawner.Recycle(this.gameObject);

		if (other.gameObject.tag == "Ball" && beenHit == false)
		{
			// The powerup has been hit by the ball

			beenHit = true;

			if (grow || shrink)
			{
				canCollideWithPlayer = true;
				ball = other.gameObject.GetComponent<Ball>();
				target = ball.lastPlayer.gameObject;
			}

			else if (topLeftAvantage || topRightAvantage)
			{
				target = top;
			}
			else if (bottomLeftAvantage || bottomRightAvantage)
			{
				target = bottom;
			}
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

		if (other.gameObject.tag == "Ceiling" && beenHit && (topLeftAvantage || topRightAvantage))
		{
			Boundary top = other.GetComponentInParent<Boundary>();
			if (topLeftAvantage) top.LeftTargetPosition();
			else if (topRightAvantage) top.RightTargetPosition();

			gameObject.SetActive(false);
		}
		else if (other.gameObject.tag == "Floor" && beenHit && (bottomLeftAvantage || bottomRightAvantage))
		{
			Boundary bottom = other.GetComponentInParent<Boundary>();
			if (bottomLeftAvantage) bottom.LeftTargetPosition();
			else if (bottomRightAvantage) bottom.RightTargetPosition();

			gameObject.SetActive(false);
		}
	}

	public void ApplyPowerUp(PaddleController paddle)
	{
		if (grow) paddle.Grow();
		if (shrink) paddle.Shrink();
	}

	public void Reset()
	{
		target = null;
	}
}
