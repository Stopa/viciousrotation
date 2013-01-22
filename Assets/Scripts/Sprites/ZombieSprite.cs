using UnityEngine;
using System.Collections;

public class ZombieSprite : BaseSprite {

	protected override void DefineSpriteAnimations() {
		Vector2 spriteUVSize = spriteManager.PixelSpaceToUVSpace(64,96);
		
		AddAnimation("idle_l", spriteUVSize, 0,   96, 1);
		AddAnimation("idle_r", spriteUVSize, 320, 96, 1);
		
		AddAnimation("walk_l", spriteUVSize, 0,   96, 5);
		AddAnimation("walk_r", spriteUVSize, 320, 96, 5);
		
		AddAnimation("melee_l", spriteUVSize, 128, 192, 3, false);
		AddAnimation("melee_r", spriteUVSize, 320, 192, 3, false);
		
		AddAnimation("death_l", spriteUVSize, 0, 288, 8, false);
		AddAnimation("death_r", spriteUVSize, 0, 384, 8, false);
	}
}
