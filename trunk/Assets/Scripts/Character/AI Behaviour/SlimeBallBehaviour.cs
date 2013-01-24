using UnityEngine;
using System.Collections;

public class SlimeBallBehaviour : BaseEnemyCharacter
{

	void Awake() {
		InitAttributes("SlimeBall", 1, 20, 7, 1, 5, 0.5f, 3, 2);
	}
	
	protected override void UpdateAnimations() {
		string animationName = "";
		
		if(_actionTaken == ActionTaken.MeleeAttack || _actionTaken == ActionTaken.Walk) {
			animationName = "move";
		} else if(_actionTaken == ActionTaken.Death) {
			animationName = "death";
		} else {
			animationName = "idle";
		}
		
		if(sprite.IsAnimationNotRunning(animationName)) {
			if(_actionTaken == ActionTaken.Death) {
				sprite.PlayAnimation(animationName);
			} else {
				sprite.PlayAnimationIfCanInterrupt(animationName);
			}
		}
	}
	
}

