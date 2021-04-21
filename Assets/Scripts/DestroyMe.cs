using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour
{
	public float countDownSecs = 5f;

	void Start()
	{
		StartCoroutine(SelfDestruct());
	}

	IEnumerator SelfDestruct()
	{
		yield return new WaitForSeconds(countDownSecs);
		Destroy(gameObject);
	}

}
