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


	public float floatingSpeed = 1;
	public float speed = 10;


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

	private Rigidbody2D rigidBody;
	private Vector2 direction;

	void Start()
	{
		rigidBody = GetComponent<Rigidbody2D>();

		SetSprite();
	}

	void SetSprite()
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
			// float smallStep = floatingSpeed * Time.deltaTime;
			// Vector3 up = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
			// transform.position = Vector3.MoveTowards(transform.position, up, smallStep);

			rigidBody.velocity = Vector2.up * floatingSpeed;
			return;
		}

		// direction = (target.transform.position - transform.position).normalized;
		// rigidBody.velocity = direction * speed;
		rigidBody.velocity = Vector2.zero;
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "KillPlane" && beenHit == false)
		{
			Debug.Log("KILL PLANE ON TRIGGER ENTER");
			powerUpSpawner.Recycle(gameObject);
		}
		else
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

		else if (other.gameObject.tag == "Player" && beenHit && canCollideWithPlayer)
		{
			// spawn a particle effect
			// spawn sound effect

			PaddleController paddle = ball.lastPlayer;
			if (paddle == null) return;

			ApplyPowerUp(paddle);

			powerUpSpawner.Recycle(gameObject);
		}

		else if (other.gameObject.tag == "Ceiling" && beenHit && (topLeftAvantage || topRightAvantage))
		{
			Boundary top = other.GetComponentInParent<Boundary>();
			if (topLeftAvantage) top.LeftTargetPosition();
			else if (topRightAvantage) top.RightTargetPosition();

			powerUpSpawner.Recycle(gameObject);
		}
		else if (other.gameObject.tag == "Floor" && beenHit && (bottomLeftAvantage || bottomRightAvantage))
		{
			Boundary bottom = other.GetComponentInParent<Boundary>();
			if (bottomLeftAvantage) bottom.LeftTargetPosition();
			else if (bottomRightAvantage) bottom.RightTargetPosition();

			powerUpSpawner.Recycle(gameObject);
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
		beenHit = false;
		canCollideWithPlayer = false;
		rigidBody.velocity = Vector2.zero;
	}
}
