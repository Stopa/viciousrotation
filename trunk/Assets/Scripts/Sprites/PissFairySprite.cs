using UnityEngine;
using System.Collections;

public class PissFairySprite : BaseSprite {
	
	void Awake() {
		this.spriteManagerGameObject = GameObject.Find("PissFairySpriteManager");
		this.spriteWorldHeight = 2f;
		this.spriteWorldWidth = 2f;
		this.spriteWidth = 32;
		this.spriteHeight = 32;
	}
	
	protected override void DefineSpriteAnimations() {
		Vector2 spriteUVSize = spriteManager.PixelSpaceToUVSpace(32,32);
		
		AddAnimation("idle_r", spriteUVSize, 0, 32, 9);
		AddAnimation("idle_l", spriteUVSize, 0, 64, 9);
	}
}
