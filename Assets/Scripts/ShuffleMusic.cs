using UnityEngine;
using System.Collections;

public class ShuffleMusic : MonoBehaviour
{
	public AudioClip[] soundtrack;
	public AudioSource audioSource;

	// Use this for initialization
	void Start()
	{
		if (!audioSource.playOnAwake)
		{
			audioSource.clip = soundtrack[Random.Range(0, soundtrack.Length)];
			audioSource.Play();
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (!audioSource.isPlaying)
		{
			audioSource.clip = soundtrack[Random.Range(0, soundtrack.Length)];
			audioSource.Play();
		}
	}
}