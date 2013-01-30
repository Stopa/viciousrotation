using UnityEngine;
using System.Collections;

public class SmokeBeardSprite : BaseSprite {
	
	void Awake() {
		this.spriteManagerGameObject = GameObject.Find("SmokeBeardSpriteManager");
		this.spriteWorldHeight = 8.94f;
		this.spriteWorldWidth = 8.94f;
		this.spriteWidth = 128;
		this.spriteHeight = 128;
	}
	
	protected override void DefineSpriteAnimations() {
		Vector2 spriteUVSize = spriteManager.PixelSpaceToUVSpace(128,128);
		
		AddAnimation ("idle_l", spriteUVSize, 0,   128, 3);
		AddAnimation ("idle_r", spriteUVSize, 384, 128, 3);
		
		AddAnimation ("transform", spriteUVSize, 0, 256, 14, false);
		
		AddAnimation ("walk", spriteUVSize, 0, 384, 8);
		
		AddAnimation ("charge_l", spriteUVSize, 0, 512, 5, false);
		AddAnimation ("discharge_l", spriteUVSize, 640, 512, 6, false);
		AddAnimation ("charge_r", spriteUVSize, 0, 640, 5, false);
		AddAnimation ("discharge_r", spriteUVSize, 640, 640, 6, false);
		AddAnimation ("charge_d", spriteUVSize, 0, 768, 5, false);
		AddAnimation ("discharge_d", spriteUVSize, 640, 768, 6, false);
	}
}
