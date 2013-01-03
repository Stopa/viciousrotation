using UnityEngine;
using System.Collections;

public class DoctorSprite : BaseSprite {

	protected override void DefineSpriteAnimations() {
		Vector2 spriteUVSize = spriteManager.PixelSpaceToUVSpace(64,96);
		
		AddAnimation("idle_l", spriteUVSize, 0,   96, 1);
		AddAnimation("idle_r", spriteUVSize, 128, 96, 1);
	}
}
