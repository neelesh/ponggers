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
	public float fireballSpeed = 18;


	public bool fireball = false;



	private Vector2 minSpeedVector;
	public bool serving = true;

	private Vector3 scale;

	public PaddleController lastPlayer;


	public ParticleSystem firePFX;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		// rb.velocity = new Vector2(initialSpeed, initialSpeed);

		scale = transform.localScale;
		firePFX.Stop();

	}

	public void Fireball()
	{
		rb.velocity = rb.velocity.normalized * maxSpeed;
		fireball=true;
		firePFX.Play();
	}

	public void SetVelocity(Vector2 vector)
	{
		rb.velocity = vector;
	}

	public void FixedUpdate()
	{
		if (serving) {
			if(firePFX.isPlaying){ 
				firePFX.Stop();
				fireball=false;
				}
			
			return;
		}

		if (fireball==false & rb.velocity.magnitude > maxSpeed ) rb.velocity = rb.velocity.normalized * maxSpeed;
		if (fireball==true & rb.velocity.magnitude > fireballSpeed ) rb.velocity = rb.velocity.normalized * fireballSpeed;
		if (rb.velocity.magnitude > maxSpeed ) rb.velocity = rb.velocity.normalized * maxSpeed;

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
		if (other.gameObject.tag != "Goal")
		{
			GameObject fx = Instantiate(ballHitFX, other.contacts[0].point, transform.rotation);
			AudioSource sound = fx.GetComponent<AudioSource>();
			if (sound) sound.pitch += rb.velocity.magnitude / maxSpeed;
		}

		if (other.gameObject.tag == "Player") lastPlayer = other.gameObject.GetComponentInParent<PaddleController>();

	}
}