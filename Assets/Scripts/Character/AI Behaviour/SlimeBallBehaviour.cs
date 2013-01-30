using UnityEngine;
using System.Collections;

public class SlimeBallBehaviour : BaseEnemyCharacter
{

	void Awake() {
		InitAttributes("SlimeBall", 1, 20, 7, 1, 5, 0.5f, 3, 2);
	}
	
	protected override void UpdateAnimations() {
		BaseEnemyCharacter charBehaviour = (BaseEnemyCharacter)gameObject.GetComponent("BaseEnemyCharacter");
		if(charBehaviour && !charBehaviour.Existing) {
			charBehaviour.Existing = true;
		}
		string animationName = "";
		
		if(_actionTaken == ActionTaken.MeleeAttack || _actionTaken == ActionTaken.Walk) {
			animationName = "move";
		} else if(_actionTaken == ActionTaken.Death) {
			animationName = "death";
		} else {
			animationName = "idle";
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

