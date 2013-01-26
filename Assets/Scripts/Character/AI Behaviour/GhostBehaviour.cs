using UnityEngine;
using System.Collections;

public class GhostBehaviour : BaseEnemyCharacter
{

	void Awake() {
		InitAttributes("Ghost", 2, 10, 20, 4, 15, 0.7f, 5, 1);
	}
	
	protected override void UpdateAnimations() {
		string animationName = "";
		
		if(_actionTaken == ActionTaken.MeleeAttack) {
			animationName = "attack_";
		} else if(_actionTaken == ActionTaken.Walk) {
			animationName = "walk_";
		} else if(_actionTaken == ActionTaken.Death) {
			animationName = "death_";
		} else {
			animationName = "idle_";
		}
		
		if(_horisontalLookingDirection == BaseCharacter.HorisontalLookingDirection.Left) {
			animationName += "l";
		} else {
			animationName += "r";
		}
		
		if(sprite.IsAnimationNotRunning(animationName)) {
			if(!_existingNPC) {
				sprite.PlayAnimation("spawn");
				_existingNPC = true;
			} else if(_actionTaken == ActionTaken.Death) {
				sprite.PlayAnimation(animationName);
			} else {
				sprite.PlayAnimationIfCanInterrupt(animationName);
			}
		}
	}
	
}

