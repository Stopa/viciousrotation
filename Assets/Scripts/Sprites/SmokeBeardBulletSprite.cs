using UnityEngine;
using System.Collections;

public class SmokeBeardBulletSprite : BaseSprite {
	
	void Awake() {
		this.spriteManagerGameObject = GameObject.Find("SmokeBeardBulletSpriteManager");
		this.spriteWorldHeight = 4.47f;
		this.spriteWorldWidth = 4.47f;
		this.spriteWidth = 64;
		this.spriteHeight = 64;
	}
	
	protected override void DefineSpriteAnimations() {
		Vector2 spriteUVSize = spriteManager.PixelSpaceToUVSpace(64, 64);
		
		AddAnimation ("idle", spriteUVSize, 0, 64, 2);
		sprite.hidden = false;
		PlayAnimation ("idle");
	}
}
