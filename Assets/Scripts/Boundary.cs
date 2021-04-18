using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
	public GameObject leftAdvantagePosition;
	public GameObject rightAdvantagePosition;

	public float speed = 10f;
	public GameObject target = null;
	public static Transform originalTransform;

	void Start()
	{
		originalTransform = transform;
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
	}

	public void RightTargetPosition()
	{
		target = rightAdvantagePosition;
	}
}
