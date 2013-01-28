using UnityEngine;
using System.Collections;

public class EvilPissFairyBehaviour : BaseEnemyCharacter
{

	void Awake() {
		InitAttributes("Evil Piss Fairy", 2, 10, 20, 4, 15, 0.1f, 5, 1);
	}
	
	protected override void UpdateAnimations() {
		BaseEnemyCharacter charBehaviour = (BaseEnemyCharacter)gameObject.GetComponent("BaseEnemyCharacter");
		if(charBehaviour && !charBehaviour.Existing) {
			charBehaviour.Existing = true;
		}
		string animationName = "";
		
		if(_actionTaken == ActionTaken.Death) {
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

