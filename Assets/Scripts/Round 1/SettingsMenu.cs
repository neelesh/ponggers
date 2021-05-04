using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
	// Save save;
	public GameData gameData;

	List<Resolution> resolutions;
	List<string> resolutionOptions;

	public Slider MusicVolume;
	public Slider SFXVolume;

	public Dropdown resolutionDropdown;
	public Dropdown frameRateDropdown;

	public Toggle fullscreenToggle;

	public string musicTag = "Music";
	private string sfxTag = "SFX";

	private static readonly int[] frameRates = { 30, 60, 300 };


	void Start()
	{

		resolutions = Screen.resolutions.Where(resolution => resolution.refreshRate == 60).ToList();
		List<string> resolutionOptions = new List<string>();

		int currentResolutionIndex = 0;

		for (int i = resolutions.Count - 1; i > -1; i--)
		{
			float width = resolutions[i].width;
			float height = resolutions[i].height;
			if (Mathf.Abs((width / height) - (16f / 9f)) > .1) resolutions.RemoveAt(i);
		}

		// Add Screen Res Options
		for (int i = 0; i < resolutions.Count; i++)
		{
			int width = resolutions[i].width;
			int height = resolutions[i].height;

			resolutionOptions.Add(width + "x" + height);

			if (width == Screen.width
				&& height == Screen.height)
			{
				currentResolutionIndex = i;
			}

		}

		resolutionDropdown.onValueChanged.AddListener(delegate
		{
			SetResolution(resolutionDropdown.value);
		});

		fullscreenToggle.onValueChanged.AddListener(delegate
		{
			SetFullscreen(fullscreenToggle.isOn);
		});

		resolutionDropdown.AddOptions(resolutionOptions);
		resolutionDropdown.value = currentResolutionIndex;
		resolutionDropdown.RefreshShownValue();



		// // Frame Rate Options
		// List<string> frameRateOptions = new List<string>();
		// frameRateOptions.Add("30");
		// frameRateOptions.Add("60");
		// frameRateOptions.Add("Uncapped");
		// frameRateDropdown.AddOptions(frameRateOptions);

		// int frameRateIndex = frameRateOptions.IndexOf(gameData.FrameRate.ToString());
		// frameRateDropdown.value = frameRateIndex;

		//MusicVolume.value = gameData.MusicVolume;
		SFXVolume.value = gameData.SfxVolume;
		MusicVolume.value = gameData.MusicVolume;

		fullscreenToggle.isOn = Screen.fullScreen;
	}


	public void SetResolution(int resolutionIndex)
	{
		Resolution resolution = resolutions[resolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	}

	public void SetFrameRate(int frameRateIndex)
	{
		int frameRate = frameRates[frameRateIndex];
		Application.targetFrameRate = frameRate;
	}

	public void SetFullscreen(bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
	}


	public void SetMusicVolume(float volume)
	{
		gameData.MusicVolume = volume;
		AudioManager.instance.SetVolumeOfTaggedAudioSources(musicTag, volume);
	}

	public void SetSFXVolume(float volume)
	{
		gameData.SfxVolume = volume;
		AudioManager.instance.SetVolumeOfTaggedAudioSources(sfxTag, volume);
	}

}
