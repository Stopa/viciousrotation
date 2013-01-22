using UnityEngine;
using System.Collections;

public class DoctorSprite : BaseSprite {
	
	void Awake() {
		this.spriteManagerGameObject = GameObject.Find("DoctorSpriteManager");
		this.spriteWorldHeight = 6.71f;
		this.spriteWorldWidth = 4.47f;
		this.spriteWidth = 64;
		this.spriteHeight = 96;
		this.defaultAnimationName = "idle_l";
	}

	protected override void DefineSpriteAnimations() {
		Vector2 spriteUVSize = spriteManager.PixelSpaceToUVSpace(64,96);
		
		AddAnimation("idle_l", spriteUVSize, 0,   96, 1);
		AddAnimation("idle_r", spriteUVSize, 128, 96, 1);
	}
}
