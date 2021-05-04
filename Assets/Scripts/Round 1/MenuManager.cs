using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
	public GameObject MainMenu;
	public GameObject SettingsMenu;
	public GameObject Credits;

	void Awake()
	{
		GoToMainMenu();
	}

	public void GoToMainMenu()
	{
		MainMenu.SetActive(true);
		SettingsMenu.SetActive(false);
		if (Credits) Credits.SetActive(false);
	}

	public void GoToSettingsMenu()
	{
		MainMenu.SetActive(false);
		SettingsMenu.SetActive(true);
		if (Credits) Credits.SetActive(false);
	}

	public void GoToCredits()
	{
		MainMenu.SetActive(false);
		SettingsMenu.SetActive(false);
		if (Credits) Credits.SetActive(true);
	}
}
