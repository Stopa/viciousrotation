using UnityEngine;
using System.Collections;

public class SlimeBallSprite : BaseSprite {
	
	void Awake() {
		this.spriteManagerGameObject = GameObject.Find("SlimeBallSpriteManager");
		this.spriteWorldHeight = 2f;
		this.spriteWorldWidth = 2f;
		this.spriteWidth = 32;
		this.spriteHeight = 32;
		this.defaultAnimationName = "idle";
	}
	
	protected override void DefineSpriteAnimations() {
		Vector2 spriteUVSize = spriteManager.PixelSpaceToUVSpace(32,32);
		
		AddAnimation("move", spriteUVSize, 32, 48, 5);
		AddAnimation("idle", spriteUVSize, 32, 48, 1);
		AddAnimation("death", spriteUVSize, 32, 48, 1, false);
	}
}
