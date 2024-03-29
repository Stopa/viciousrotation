using UnityEngine;
using System.Collections;

public class ZombieBehaviour : BaseEnemyCharacter
{

	void Awake() {
		InitAttributes("Zombie", 2, 10, 5, 4, 15, 0.1f, 5, 1);
	}
	
	protected override void UpdateAnimations() {
		BaseEnemyCharacter charBehaviour = (BaseEnemyCharacter)gameObject.GetComponent("BaseEnemyCharacter");
		if(charBehaviour && !charBehaviour.Existing) {
			charBehaviour.Existing = true;
		}
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
		
		if(_sprite.IsAnimationNotRunning(animationName)) {
			if(_actionTaken == ActionTaken.Death) {
				_sprite.PlayAnimation(animationName);
			} else {
				_sprite.PlayAnimationIfCanInterrupt(animationName);
			}
		}
	}
	
}

