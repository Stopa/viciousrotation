using UnityEngine;
using System.Collections;

public class BaseSprite : MonoBehaviour {
	
	public GameObject spriteManagerGameObject;
	public float spriteWorldWidth;
	public float spriteWorldHeight;
	public int spriteWidth;
	public int spriteHeight;
	public Sprite sprite;
	
	protected SpriteManager spriteManager;
	protected bool _animationRunning;

	// Use this for initialization
	void Start () {
		spriteManager = (SpriteManager)spriteManagerGameObject.GetComponent("LinkedSpriteManager");
		sprite = spriteManager.AddSprite(gameObject, spriteWorldWidth, spriteWorldHeight, 0, spriteHeight, spriteWidth, spriteHeight, true);
		sprite.hidden = true;
		DefineSpriteAnimations();
		sprite.SetAnimCompleteDelegate(new Sprite.AnimCompleteDelegate(AnimationCompleted));
	}
	
	protected virtual void DefineSpriteAnimations() {}
	
	protected void AddAnimation(string name, Vector2 spriteSize,int startLeft, int startBottom, int animationLength, bool loop = true) {
		UVAnimation animation = new UVAnimation();
		animation.name = name;
		if(loop) {
			animation.loopCycles = -1;
		} else {
			animation.loopCycles = 0;
		}
		animation.BuildUVAnim(spriteManager.PixelCoordToUVCoord(startLeft, startBottom), spriteSize, animationLength, 1, animationLength, 8);
		sprite.AddAnimation(animation);
	}
	
	// use if you don't wish to interrupt currently running non-looping animation (e.g. attack)
	public void PlayAnimationIfCanInterrupt(string animationName) {
		if(!_animationRunning || sprite.GetCurAnim() != null && sprite.GetCurAnim().loopCycles == -1) {
			PlayAnimation (animationName);
		}
	}
	
	public void PlayAnimation(string animationName) {
		if(sprite.hidden) {
			sprite.hidden = false;
		}
		_animationRunning = true;
		sprite.PlayAnim(animationName);
	}
	
	public bool IsAnimationNotRunning(string animationName) {
		return (sprite.GetCurAnim() == null || sprite.GetCurAnim().name != animationName);
	}
	
	public void DestroySprite() {
		spriteManager.StopAnimation(sprite);
		spriteManager.RemoveSprite(sprite);
	}
	
	public UVAnimation GetCurrentAnimation() {
		return sprite.GetCurAnim();
	}
	
	void AnimationCompleted() {
		_animationRunning = false;
	}
}
