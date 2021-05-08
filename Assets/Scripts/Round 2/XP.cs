using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XP : MonoBehaviour
{
	public int balance;
	public TextMeshProUGUI balanceDisplay;
	public TextMeshProUGUI skillTreeBalanceDisplay;

	public void Add(int toAdd)
	{
		balance += toAdd;
		UpdateUI();
	}

	public void Subtract(int toSubtract)
	{
		balance -= toSubtract;
		UpdateUI();
	}

	void UpdateUI()
	{
		balanceDisplay.text = balance.ToString();
		skillTreeBalanceDisplay.text = balance.ToString();
	}
}