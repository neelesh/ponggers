using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills
{
	public enum SkillType
	{
		Speed,
		Movement,
		Tilting,
		CurveBall,
		Magnetic,
		Dash,
		Barrier,
		ChargeShot,
		MiniPaddle,
		CatchServe
	}

	public PaddleController paddleController;

	private List<SkillType> unlockedSkillTypeList = new List<SkillType>();

	public void UnlockSkill(SkillType skillType)
	{
		unlockedSkillTypeList.Add(skillType);
		paddleController.ActivateSkill(skillType);
	}
	public bool IsSkillUnlocked(SkillType skillType) => unlockedSkillTypeList.Contains(skillType);
}
