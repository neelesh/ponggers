using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
	public GameObject paddleGO;
	public float speed = 6;
	public float maxSpeed = 9;
	public float dashSpeed = 50;
	public Rigidbody2D rb;
	public BoxCollider2D boxCollider;
	public GameObject servePosition;
	Vector2 movement;
	public GameObject ball;
	public Rigidbody2D ballRB;

	public bool leftPaddle = true;
	public bool isAIPlayer = false;

	public bool isServing = false;

	public Vector3 defaultSize;
	public Vector3 bigPaddleSize;
	public Vector3 smallPaddleSize;

	public Vector3 targetScale;

	public GameManager gameManager;

	public GameObject topCircle;
	public GameObject topCirclePosition;
	public GameObject bottomCircle;
	public GameObject bottomCirclePosition;


	public PointEffector2D magnet;

	public Skills skills;

	private TDActions controls;
	private Camera mainCamera;

	public XP xp;

	private void Awake() => controls = new TDActions();
	private void OnEnable() => controls.Enable();
	private void OnDisable() => controls.Disable();


	[SerializeField] private Transform bulletDirection;

	void Start()
	{
		mainCamera = Camera.main;
		// New Input System Stuff
		controls.ActionMap.Primary.performed += _ => Primary();
		controls.ActionMap.Secondary.performed += _ => Secondary();
		controls.ActionMap.Dash.performed += _ => TryDash();

		magnet.enabled = false;

		//skill tree
		skills = new Skills();

		defaultSize = paddleGO.transform.localScale;
		targetScale = defaultSize;

		bigPaddleSize = new Vector3(defaultSize.x, defaultSize.y * 1.5f, defaultSize.z); ;
		smallPaddleSize = new Vector3(defaultSize.x, defaultSize.y / 1.5f, defaultSize.z);

		rb = GetComponent<Rigidbody2D>();
		boxCollider = GetComponentInChildren<BoxCollider2D>();
		ballRB = ball.GetComponent<Rigidbody2D>();

		transform.position = (leftPaddle) ? new Vector2(transform.position.x, -2f) : new Vector2(transform.position.x, 2f);

	}

	void Update()
	{
		SetCirclesPosition();

		if (paddleGO.transform.localScale != targetScale) paddleGO.transform.localScale = Vector3.Lerp(paddleGO.transform.localScale, targetScale, Time.deltaTime * 10);

		if (Time.timeScale == 0) return;

		if (isAIPlayer)
		{
			SimulatePlayer();
			return;
		}

		// Rotate to face mouse
		if (skills.IsSkillUnlocked(Skills.SkillType.Tilting))
		{
			Vector2 mouseScreenPos = controls.ActionMap.MousePosition.ReadValue<Vector2>();
			Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
			Vector3 targetDirection = mouseWorldPos - transform.position;
			float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
			if (angle < -30) angle = -30;
			else if (angle > 30) angle = 30;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0f, 0f, angle)), Time.time * 1);
		}
	}

	void FixedUpdate()
	{
		if (skills.IsSkillUnlocked(Skills.SkillType.Speed) && speed < maxSpeed) speed = maxSpeed;

		if (skills.IsSkillUnlocked(Skills.SkillType.Magnetic) && magnet.enabled == false) magnet.enabled = true;

		// rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
		rb.velocity = movement * speed;
		if (leftPaddle)
		{
			movement.y = controls.ActionMap.Movement.ReadValue<Vector2>().y;
			if (skills.IsSkillUnlocked(Skills.SkillType.Movement))
			{
				movement.x = controls.ActionMap.Movement.ReadValue<Vector2>().x;
				if ((rb.constraints & RigidbodyConstraints2D.FreezePositionX) == RigidbodyConstraints2D.FreezePositionX)
				{
					rb.constraints = RigidbodyConstraints2D.FreezeRotation;
				}
			}
		}
		else if (!isAIPlayer) movement.y = controls.ActionMap.Movement.ReadValue<Vector2>().y;
	}



	void SetCirclesPosition()
	{
		Bounds boxBounds = boxCollider.bounds;
		GameObject closestBall = gameManager.GetClosestBall(gameObject);

		float topY = boxBounds.size.y / 2;
		float bottomY = -boxBounds.size.y / 2;

		topCircle.transform.position = topCirclePosition.transform.position;
		bottomCircle.transform.position = bottomCirclePosition.transform.position;
	}

	private void SimulatePlayer()
	{
		Bounds boxBounds = boxCollider.bounds;
		GameObject closestBall = gameManager.GetClosestBall(gameObject);
		ballRB = closestBall.GetComponent<Rigidbody2D>();

		float topY = boxBounds.center.y + boxBounds.extents.y;
		float bottomY = boxBounds.center.y - boxBounds.extents.y;

		float center = Camera.main.transform.position.y;

		if (isServing)
		{
			BeNeutral();
		}

		// The ball is moving away from us so lets pick a neutral position
		if (leftPaddle && ballRB.velocity.x > 0)
		{
			GoToCenter();
			return;
		}
		else if (!leftPaddle && ballRB.velocity.x < 0)
		{
			GoToCenter();
			return;
		}

		// too far to bother
		if (Mathf.Abs(closestBall.transform.position.x - gameObject.transform.position.x) > 4)
		{
			movement.y = 0;
			return;
		}

		if (closestBall.transform.position.y > topY) movement.y = 1;
		else if (closestBall.transform.position.y < bottomY) movement.y = -1;
		// else movement.y = 0;

		void BeNeutral()
		{
			if (bottomY > center + 1) movement.y = -1;
			else if (topY < center - 1) movement.y = 1;
			return;
		}

		void GoToCenter()
		{
			if (topY > center + 4) movement.y = -1;
			else if (bottomY < center - 4) movement.y = 1;
			else movement.y = 0;
		}
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


	public void PrepareServeBall(Ball ball)
	{
		if (ball.fireball) ball.fireball = false;
		if (ball.firePFX.isPlaying) ball.firePFX.Stop();

		isServing = true;
		ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic; // it's 3am i dunno 
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






	// SKILL TREE SKILLSS
	public bool CanUseSpeed() => skills.IsSkillUnlocked(Skills.SkillType.Speed);
	public bool CanUseMovement() => skills.IsSkillUnlocked(Skills.SkillType.Movement);
	public bool CanUseTilting() => skills.IsSkillUnlocked(Skills.SkillType.Tilting);
	public bool CanUseBall() => skills.IsSkillUnlocked(Skills.SkillType.CurveBall);
	public bool CanUseMagnetic() => skills.IsSkillUnlocked(Skills.SkillType.Magnetic);
	public bool CanUsChargeShot() => skills.IsSkillUnlocked(Skills.SkillType.Magnetic);


	public void Primary()
	{

	}

	public void Secondary()
	{

	}



	private bool canDash = true;
	public void TryDash()
	{
		if (skills.IsSkillUnlocked(Skills.SkillType.Dash) == false | canDash == false) return;

		StartCoroutine(Dash());
	}

	IEnumerator Dash()
	{
		canDash = false;
		float temp = speed;
		speed = dashSpeed;

		yield return new WaitForSeconds(.1f);
		speed = temp;

		canDash = true;
	}
}
