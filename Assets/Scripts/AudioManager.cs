using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;
	// Save save;
	public GameData gameData;

	public GameObject[] musicObjects;
	public GameObject[] sfxObjects;
	public List<GameObject> soundObjects;

	public string musicTag = "Music";
	public string sfxTag = "SFX";

	// Start is called before the first frame update
	void Start()
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}


		// // Set the SFX Volume based on the save file.
		// save = Save.instance;
		// gameData = save.GetGameData();
		// if (save)
		// {

		musicObjects = GameObject.FindGameObjectsWithTag(musicTag);
		sfxObjects = GameObject.FindGameObjectsWithTag(sfxTag);

		foreach (GameObject go in musicObjects) soundObjects.Add(go);
		foreach (GameObject go in sfxObjects) soundObjects.Add(go);

		float sfxVolume = gameData.SfxVolume;
		AddProportionalVolumeComponent(sfxTag);
		SetVolumeOfTaggedAudioSources(sfxTag, sfxVolume);
		SetVolumeOfTaggedAudioSources(musicTag, sfxVolume);
		// }
		// else
		// {
		// 	Debug.Log("SettingsMenu.cs can not find the Save Singleton Instance.");
		// }
	}

	public void SetVolumeOfTaggedAudioSources(string tag, float volume)
	{
		foreach (GameObject sound in soundObjects)
		{
			if (sound.tag != tag) continue;
			if (!sound.GetComponent<ProportionalVolume>()) Debug.Log(sound.name + " has tag " + tag + " but no ProportionalVolume script");

			AudioSource audioSource = sound.GetComponent<AudioSource>();
			float propVol = sound.GetComponent<ProportionalVolume>().GetProportionalVolume();


			if (propVol != 0) audioSource.volume = volume * propVol;
		}
	}

	public void AddProportionalVolumeComponent(string tag)
	{

		foreach (GameObject sound in soundObjects)
		{
			AudioSource audioSource = sound.GetComponent<AudioSource>();

			if (audioSource)
				sound.AddComponent<ProportionalVolume>();
		}
	}

	public float GetSFXVolume()
	{
		return gameData.SfxVolume;
	}
}
