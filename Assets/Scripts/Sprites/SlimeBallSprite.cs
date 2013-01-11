using UnityEngine;
using System.Collections;

public class SlimeBallSprite : BaseSprite {
	
	protected override void DefineSpriteAnimations() {
		Vector2 spriteUVSize = spriteManager.PixelSpaceToUVSpace(32,32);
		
		AddAnimation("move", spriteUVSize, 32, 48, 5);
		AddAnimation("idle", spriteUVSize, 32, 48, 1);
	}
}
