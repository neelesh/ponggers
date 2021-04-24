using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSFXVolume : MonoBehaviour
{
	AudioSource audioSource;
	// Start is called before the first frame update
	void Start()
	{
		if (!audioSource) audioSource = GetComponent<AudioSource>();
		if (audioSource) audioSource.volume = AudioManager.instance.GetSFXVolume();
	}
}
