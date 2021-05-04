using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeUI : MonoBehaviour
{
	private PaddleController paddleController;

	public void UnlockSpeed() => paddleController.skills.UnlockSkill(Skills.SkillType.Speed);
	public void UnlockMovement() => paddleController.skills.UnlockSkill(Skills.SkillType.Movement);
	public void UnlockTilting() => paddleController.skills.UnlockSkill(Skills.SkillType.Tilting);
	public void UnlockBall() => paddleController.skills.UnlockSkill(Skills.SkillType.CurveBall);
	public void UnlockMagnetic() => paddleController.skills.UnlockSkill(Skills.SkillType.Magnetic);
	public void UnlockChargeShot() => paddleController.skills.UnlockSkill(Skills.SkillType.ChargeShot);
}
