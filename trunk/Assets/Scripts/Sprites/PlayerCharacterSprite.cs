using UnityEngine;
using System.Collections;

public class PlayerCharacterSprite : BaseSprite {
	
	void Awake() {
		this.spriteManagerGameObject = GameObject.Find("PlayerCharacterSpriteManager");
		this.spriteWorldHeight = 6.71f;
		this.spriteWorldWidth = 4.47f;
		this.spriteWidth = 64;
		this.spriteHeight = 96;
	}

	protected override void DefineSpriteAnimations() {
		Vector2 spriteUVSize = spriteManager.PixelSpaceToUVSpace(64,96);
		
		AddAnimation("walk_dl",	spriteUVSize, 512, 96,  8);
		AddAnimation("walk_d",	spriteUVSize, 0,   192, 8);
		AddAnimation("walk_dr",	spriteUVSize, 512, 192, 8);
		AddAnimation("walk_r",	spriteUVSize, 0,   288, 8);
		AddAnimation("walk_tr",	spriteUVSize, 512, 288, 8);
		AddAnimation("walk_t",	spriteUVSize, 0,   384, 8);
		AddAnimation("walk_tl",	spriteUVSize, 512, 384, 8);
		AddAnimation("walk_l",	spriteUVSize, 0,   480, 8);
		
		AddAnimation("idle_dl",	spriteUVSize, 0,   96, 1);
		AddAnimation("idle_d",	spriteUVSize, 64,  96, 1);
		AddAnimation("idle_dr",	spriteUVSize, 128, 96, 1);
		AddAnimation("idle_r",	spriteUVSize, 192, 96, 1);
		AddAnimation("idle_tr",	spriteUVSize, 256, 96, 1);
		AddAnimation("idle_t",	spriteUVSize, 320, 96, 1);
		AddAnimation("idle_tl",	spriteUVSize, 384, 96, 1);
		AddAnimation("idle_l",	spriteUVSize, 448, 96, 1);
		
		AddAnimation("melee_dl", spriteUVSize, 512, 480, 4, false);
		AddAnimation("melee_d",  spriteUVSize, 768, 480, 4, false);
		AddAnimation("melee_dr", spriteUVSize, 0,   576, 4, false);
		AddAnimation("melee_tl", spriteUVSize, 256, 576, 4, false);
		AddAnimation("melee_t",  spriteUVSize, 768, 576, 4, false);
		AddAnimation("melee_tr", spriteUVSize, 512, 576, 4, false);
		
		AddAnimation("ranged_l", spriteUVSize, 0,   672, 4, false);
		AddAnimation("ranged_r", spriteUVSize, 256, 672, 4, false);
		AddAnimation("ranged_d", spriteUVSize, 512, 672, 4, false);
		AddAnimation("ranged_t", spriteUVSize, 768, 672, 4, false);

	}
}
