using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
	public GameObject leftAdvantagePosition;
	public GameObject rightAdvantagePosition;

	public float speed = 10f;
	public GameObject target = null;
	public static GameObject originalTransform;

	public bool neutral = true;
	public bool leftAdvantage;
	public bool rightAdvantage;

	void Start()
	{
		originalTransform = new GameObject();
		originalTransform.transform.position = transform.position;
		originalTransform.transform.rotation = transform.rotation;
	}

	void FixedUpdate()
	{
		if (target == null) return;
		float step = speed * Time.deltaTime;

		if (transform.position != target.transform.position) transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
		if (transform.rotation != target.transform.rotation) transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, step);
	}

	public void LeftTargetPosition()
	{
		target = leftAdvantagePosition;
		leftAdvantage = true;
		rightAdvantage = false;
		neutral = false;
	}

	public void RightTargetPosition()
	{
		target = rightAdvantagePosition;
		leftAdvantage = false;
		rightAdvantage = true;
		neutral = false;
	}

	public void NeutralPosition()
	{
		target = originalTransform;
		leftAdvantage = false;
		rightAdvantage = false;
		neutral = true;
	}
}
