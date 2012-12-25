using UnityEngine;
using System.Collections;

public class PlayerCharacterSprite : BaseSprite {

	protected override void DefineSpriteAnimations() {
		Vector2 spriteUVSize = spriteManager.PixelSpaceToUVSpace(64,96);
		
		AddAnimation("walk_dl",	spriteUVSize, 64, 96, 8);
		AddAnimation("walk_d",	spriteUVSize, 64, 192, 8);
		AddAnimation("walk_dr",	spriteUVSize, 64, 288, 8);
		AddAnimation("walk_l",	spriteUVSize, 64, 384, 8);
		AddAnimation("walk_tl",	spriteUVSize, 64, 480, 8);
		AddAnimation("walk_r",	spriteUVSize, 64, 576, 8);
		AddAnimation("walk_tr",	spriteUVSize, 64, 672, 8);
		AddAnimation("walk_t",	spriteUVSize, 64, 768, 8);
		
		AddAnimation("idle_dl",	spriteUVSize, 0, 96,  1);
		AddAnimation("idle_d",	spriteUVSize, 0, 192, 1);
		AddAnimation("idle_dr",	spriteUVSize, 0, 288, 1);
		AddAnimation("idle_l",	spriteUVSize, 0, 384, 1);
		AddAnimation("idle_tl",	spriteUVSize, 0, 480, 1);
		AddAnimation("idle_r",	spriteUVSize, 0, 576, 1);
		AddAnimation("idle_tr",	spriteUVSize, 0, 672, 1);
		AddAnimation("idle_t",	spriteUVSize, 0, 768, 1);
	}
	
}
