using UnityEngine;
using System.Collections;

public class BaseSprite : MonoBehaviour {
	
	public GameObject spriteManagerGameObject;
	public float spriteWorldWidth;
	public float spriteWorldHeight;
	public int spriteWidth;
	public int spriteHeight;
	public string defaultAnimationName;
	
	protected Sprite sprite;
	protected SpriteManager spriteManager;

	// Use this for initialization
	void Start () {
		spriteManager = (SpriteManager)spriteManagerGameObject.GetComponent("LinkedSpriteManager");
		sprite = spriteManager.AddSprite(gameObject, spriteWorldWidth, spriteWorldHeight, 0, spriteHeight, spriteWidth, spriteHeight, true);
		DefineSpriteAnimations();
		if(defaultAnimationName.Length > 0) {
			sprite.PlayAnim(defaultAnimationName);
		}
	}
	
	protected virtual void DefineSpriteAnimations() {}
	
	protected void AddAnimation(string name, Vector2 spriteSize,int startLeft, int startBottom, int animationLength) {
		UVAnimation animation = new UVAnimation();
		animation.name = name;
		animation.loopCycles = -1;
		animation.BuildUVAnim(spriteManager.PixelCoordToUVCoord(startLeft, startBottom), spriteSize, animationLength, 1, animationLength, 8);
		sprite.AddAnimation(animation);
	}
	
	public void PlayAnimation(string animationName) {
		sprite.PlayAnim(animationName);
	}
	
	public bool IsAnimationNotRunning(string animationName) {
		return (sprite.GetCurAnim() == null || sprite.GetCurAnim().name != animationName);
	}
	
	public void DestroySprite() {
		spriteManager.StopAnimation(sprite);
		spriteManager.RemoveSprite(sprite);
	}
	
	public UVAnimation CurrentAnimation() {
		return sprite.GetCurAnim();
	}
}
