using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeTracker : MonoBehaviour
{
	public PaddleController paddle;
	public List<SkillButton> skillButtons;
	public List<SkillButton> activeButtons;
	public List<SkillButton> lastActiveButtons;

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
				Debug.Log("Skill Unlocked");
				break;
			}
		}

		lastActiveButtons.Clear();
		foreach (var button in activeButtons) lastActiveButtons.Add(button);

		try
		{
			if (paddle.isAIPlayer) StartCoroutine(AiBuyASkill());
		}
		catch (Exception e) { }

		yield return new WaitForSeconds(5);
		justChecked = false;
	}

	IEnumerator AiBuyASkill()
	{
		if (activeButtons.Count == 0) yield return new WaitForSeconds(0f);

		yield return new WaitForSeconds(3f);
		SkillButton skillToBuy = activeButtons[UnityEngine.Random.Range(0, activeButtons.Count - 1)];
		skillToBuy.gameObject.GetComponent<Button>().onClick.Invoke();
	}
}
