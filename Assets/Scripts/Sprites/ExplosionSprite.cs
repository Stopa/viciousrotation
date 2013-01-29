using UnityEngine;
using System.Collections;

public class ExplosionSprite : BaseSprite {
	
	void Awake() {
		this.spriteManagerGameObject = GameObject.Find("ExplosionSpriteManager");
		this.spriteWorldHeight = 8f;
		this.spriteWorldWidth = 8f;
		this.spriteWidth = 128;
		this.spriteHeight =128;
	}
	
	protected override void DefineSpriteAnimations() {
		Vector2 spriteUVSize = spriteManager.PixelSpaceToUVSpace(128,128);
		
		AddAnimation("idle", spriteUVSize, 0, 128, 23, false);
	}
}
