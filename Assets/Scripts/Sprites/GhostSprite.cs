using UnityEngine;
using System.Collections;

public class GhostSprite : BaseSprite
{
	
	void Awake() {
		this.spriteManagerGameObject = GameObject.Find("GhostSpriteManager");
		this.spriteWorldHeight = 6.71f;
		this.spriteWorldWidth = 4.47f;
		this.spriteWidth = 64;
		this.spriteHeight = 96;
	}
	
	protected override void DefineSpriteAnimations() {
		Vector2 spriteUVSize = spriteManager.PixelSpaceToUVSpace(64,96);
		
		AddAnimation("idle_l", spriteUVSize, 0,   96, 8, false);
		AddAnimation("idle_r", spriteUVSize, 512, 96, 8, false);
		
		AddAnimation("walk_l", spriteUVSize, 0,   96, 8);
		AddAnimation("walk_r", spriteUVSize, 512, 96, 8);
		
		AddAnimation("attack_l", spriteUVSize, 0,   192, 6, false);
		AddAnimation("attack_r", spriteUVSize, 384, 192, 6, false);
		
		AddAnimation("death_l", spriteUVSize, 0, 288, 10, false);
		AddAnimation("death_r", spriteUVSize, 0, 384, 10, false);
		
		AddAnimation("spawn", spriteUVSize, 0, 480, 10, false);
	}
}

