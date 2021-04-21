using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
	public PowerUpSpawner powerUpSpawner;

	public GameObject target;

	public GameObject top;
	public GameObject bottom;

	public Boundary topBoundary;
	public Boundary bottomBoundary;

	public PaddleController left;
	public PaddleController right;

	public Color rightColor;
	public Color leftColor;

	public bool canCollideWithPlayer = false;
	public bool beenHit = false;


	public float floatingSpeed = 1;
	public float speed = 10;


	public bool grow;
	public bool shrink;
	public bool reset;

	public bool wallTilting;
	public bool topLeftAvantage;
	public bool bottomLeftAvantage;
	public bool topRightAvantage;
	public bool bottomRightAvantage;

	public SpriteRenderer spriteRenderer;

	Ball ball;

	public Sprite growSymbol;
	public Sprite shrinkSymbol;
	public Sprite resetSymbol;

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
		if (reset) spriteRenderer.sprite = resetSymbol;


		if (topLeftAvantage) spriteRenderer.sprite = topLeftAvantageSprite;
		if (bottomLeftAvantage) spriteRenderer.sprite = bottomLeftAvantageSprite;
		if (topRightAvantage) spriteRenderer.sprite = topRightAvantageSprite;
		if (bottomRightAvantage) spriteRenderer.sprite = bottomRightAvantageSprite;

		if (topLeftAvantage | bottomLeftAvantage) spriteRenderer.color = leftColor;
		if (topRightAvantage | bottomRightAvantage) spriteRenderer.color = rightColor;
	}

	void Update()
	{
		// If one boundary is already tilted that way, switch to the other way.

		if (wallTilting && topBoundary.neutral && bottomBoundary.neutral && !(topLeftAvantage | topRightAvantage | bottomLeftAvantage | bottomRightAvantage))
		{
			int number = Random.Range(1, 4);

			switch (number)
			{
				case 1:
					topLeftAvantage = true;
					topRightAvantage = false;
					bottomLeftAvantage = false;
					bottomRightAvantage = false;
					break;
				case 2:
					topLeftAvantage = false;
					topRightAvantage = true;
					bottomLeftAvantage = false;
					bottomRightAvantage = false;
					break;
				case 3:
					topLeftAvantage = false;
					topRightAvantage = false;
					bottomLeftAvantage = true;
					bottomRightAvantage = false;
					break;
				case 4:
					topLeftAvantage = false;
					topRightAvantage = false;
					bottomLeftAvantage = false;
					bottomRightAvantage = true;
					break;
				default:
					topLeftAvantage = true;
					topRightAvantage = false;
					bottomLeftAvantage = false;
					bottomRightAvantage = false;
					break;
			}

			SetSprite();
		}

		if (topLeftAvantage && topBoundary.leftAdvantage)
		{
			topLeftAvantage = false;
			topRightAvantage = true;
			spriteRenderer.color = rightColor;
			SetSprite();
		}

		else if (topRightAvantage && topBoundary.rightAdvantage)
		{
			topLeftAvantage = true;
			topRightAvantage = false;
			spriteRenderer.color = leftColor;
			SetSprite();
		}


		else if (bottomLeftAvantage && bottomBoundary.leftAdvantage)
		{
			bottomLeftAvantage = false;
			bottomRightAvantage = true;
			spriteRenderer.color = rightColor;
			SetSprite();
		}

		else if (bottomRightAvantage && bottomBoundary.rightAdvantage)
		{
			bottomLeftAvantage = true;
			bottomRightAvantage = false;
			spriteRenderer.color = leftColor;
			SetSprite();
		}
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

		direction = (target.transform.position - transform.position);
		direction.Normalize();
		rigidBody.velocity = direction * speed;
		// rigidBody.velocity = Vector2.zero;
		// float step = speed * Time.deltaTime;
		// transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
	}

	public void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "KillPlane" && beenHit == false)
		{
			powerUpSpawner.Recycle(gameObject);
		}
		else if (other.gameObject.tag == "Ball" && beenHit == false)
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

			else if (reset)
			{
				ResetLevel();
			}
		}

		else if (other.gameObject.tag == "Player" && beenHit && canCollideWithPlayer)
		{

			Debug.Log("SIZE POWERUP");

			PaddleController paddle = other.gameObject.GetComponentInParent<PaddleController>();

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


	public void ResetLevel()
	{
		bottomBoundary.NeutralPosition();
		topBoundary.NeutralPosition();
		left.NormalSize();
		right.NormalSize();

		// Remove all the powerups on the screen
		var powerUps = GameObject.FindGameObjectsWithTag("PowerUp");
		foreach (GameObject go in powerUps)
		{
			if (go == this.gameObject) continue;

			Powerup p = go.GetComponent<Powerup>();
			p.powerUpSpawner.Recycle(go);
		}

		powerUpSpawner.Recycle(gameObject);
	}

	// This Resets The Powerup
	public void Reset()
	{
		target = null;
		beenHit = false;
		canCollideWithPlayer = false;
		rigidBody.velocity = Vector2.zero;
	}


}
