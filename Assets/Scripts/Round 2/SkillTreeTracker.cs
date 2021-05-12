using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillTreeTracker : MonoBehaviour
{
	public PaddleController paddle;
	public List<SkillButton> skillButtons;
	public List<SkillButton> activeButtons;
	public List<SkillButton> lastActiveButtons;

	public TextMeshProUGUI notification;

	private bool justChecked = false;

	private void Update()
	{
		if (justChecked == false) StartCoroutine(CheckForUnlockedSkill());
	}

	IEnumerator CheckForUnlockedSkill()
	{
		justChecked = true;


		activeButtons.Clear();
		foreach (var button in skillButtons)
		{
			if (button.available && !button.purchased) activeButtons.Add(button);
		}

		// We now have all the active buttons

		foreach (var button in activeButtons)
		{
			if (!lastActiveButtons.Contains(button))
			{
				// we have new unlocked buttons!
				// Announce it
				StartCoroutine(Notification("Skills Available"));
				break;
			}
		}

		lastActiveButtons.Clear();
		foreach (var button in activeButtons) lastActiveButtons.Add(button);


		if (paddle.isAIPlayer && activeButtons.Count > 0) StartCoroutine(AiBuyASkill());


		yield return new WaitForSeconds(5);
		justChecked = false;
	}

	IEnumerator AiBuyASkill()
	{
		yield return new WaitForSeconds(3f);
		SkillButton skillToBuy = activeButtons[UnityEngine.Random.Range(0, activeButtons.Count - 1)];
		if (skillToBuy) skillToBuy.gameObject.GetComponent<Button>().onClick.Invoke();
	}

	IEnumerator Notification(string message)
	{
		notification.text = message;
		yield return new WaitForSeconds(5f);
		notification.text = "";
	}
}
