using UnityEngine;
using System.Collections;

public class ZombieSprite : BaseSprite {
	
	void Awake() {
		this.spriteManagerGameObject = GameObject.Find("ZombieSpriteManager");
		this.spriteWorldHeight = 6.71f;
		this.spriteWorldWidth = 4.47f;
		this.spriteWidth = 64;
		this.spriteHeight = 96;
		this.defaultAnimationName = "idle_l";
	}

	protected override void DefineSpriteAnimations() {
		Vector2 spriteUVSize = spriteManager.PixelSpaceToUVSpace(64,96);
		
		AddAnimation("idle_l", spriteUVSize, 0, 96,  1);
		AddAnimation("idle_r", spriteUVSize, 0, 192, 1);
		
		AddAnimation("walk_l", spriteUVSize, 0, 96,  5);
		AddAnimation("walk_r", spriteUVSize, 0, 192, 5);
		
		AddAnimation("attack_l", spriteUVSize, 0,   288, 3, false);
		AddAnimation("attack_r", spriteUVSize, 192, 288, 3, false);
		
		AddAnimation("death_l", spriteUVSize, 0, 384, 8, false);
		AddAnimation("death_r", spriteUVSize, 0, 480, 8, false);
	}
}
