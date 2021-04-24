using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProportionalVolume : MonoBehaviour
{
	public float targetVolume;
	// Start is called before the first frame update
	void Awake()
	{
		targetVolume = GetComponent<AudioSource>().volume;
	}

	public float GetProportionalVolume()
	{
		return targetVolume;
	}
}
