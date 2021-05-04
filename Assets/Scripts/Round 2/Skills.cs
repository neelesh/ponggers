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
		ChargeShot
	}

	private List<SkillType> unlockedSkillTypeList;

	public void PlayerSkills() => unlockedSkillTypeList = new List<SkillType>();


	public void UnlockSkill(SkillType skillType) => unlockedSkillTypeList.Add(skillType);

	public bool IsSkillUnlocked(SkillType skillType) => unlockedSkillTypeList.Contains(skillType);
}
