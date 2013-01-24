using UnityEngine;
using System.Collections;

public class ZombieBehaviour : BaseEnemyCharacter
{

	void Awake() {
		InitAttributes("Zombie", 2, 10, 5, 4, 15, 0.1f, 5, 1);
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
		
		if(_actionTaken == ActionTaken.Idle || _horisontalLookingDirection == BaseCharacter.HorisontalLookingDirection.Left) {
			animationName += "l";
		} else {
			animationName += "r";
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

