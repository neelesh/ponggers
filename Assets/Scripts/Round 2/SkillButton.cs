using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillButton : MonoBehaviour
{
	public string skillTipText;
	public TextMeshProUGUI skillTipArea;
	public Button button;
	public TextMeshProUGUI costText;

	public int cost = 1000;
	public List<SkillButton> prerequisites;

	public bool available = false;
	public bool purchased = false;
	public bool activated = false;

	public PaddleController paddleController;

	private bool hasPrerequisite = false;

	public Skills.SkillType skillType;

	public Color notAvailableColor;
	public Color availableColor;
	public Color purchasedColor;

	private ColorBlock colorBlock;

	public GameObject hudIcon;

	private void Awake()
	{
		costText.text = cost.ToString();
		costText.gameObject.SetActive(false);
		hudIcon.SetActive(false);
	}

	void Start()
	{
		button = GetComponent<Button>();
		button.interactable = false;

		colorBlock = button.colors;
		colorBlock.disabledColor = notAvailableColor;
		colorBlock.normalColor = availableColor;

		button.colors = colorBlock;

	}

	void Update()
	{
		if (purchased) return;

		if (hasPrerequisite == false && prerequisites.Count != 0)
		{

			foreach (SkillButton skillButton in prerequisites)
			{
				if (skillButton.purchased)
				{
					hasPrerequisite = true;
					break;
				}
			}
		}
		else hasPrerequisite = true;


		if (paddleController.xp.balance >= cost && hasPrerequisite)
		{
			costText.gameObject.SetActive(true);
			available = true;
			button.interactable = true;
		}
		else
		{
			button.interactable = false;
			costText.gameObject.SetActive(false);
		}
	}

	public void Purchase()
	{
		if (paddleController.xp.balance < cost) return;

		costText.gameObject.SetActive(false);
		paddleController.xp.Subtract(cost);
		purchased = true;
		available = false;
		colorBlock.disabledColor = purchasedColor;
		button.colors = colorBlock;
		button.interactable = false;
		paddleController.skills.UnlockSkill(skillType);

		hudIcon.SetActive(true);
	}

	public void ShowSkillTip()
	{
		skillTipArea.text = skillTipText;
	}

	public void HideSkillTip()
	{
		skillTipArea.text = "";
	}

}
