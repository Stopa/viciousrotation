using UnityEngine;
using System.Collections;

public class PissFairySprite : BaseSprite {
	
	protected override void DefineSpriteAnimations() {
		Vector2 spriteUVSize = spriteManager.PixelSpaceToUVSpace(32,32);
		
		AddAnimation("look_r", spriteUVSize, 0, 32, 9);
		AddAnimation("look_l", spriteUVSize, 0, 64, 9);
	}
}
