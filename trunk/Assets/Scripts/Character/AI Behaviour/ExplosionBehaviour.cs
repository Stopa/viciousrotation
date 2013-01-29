using UnityEngine;
using System.Collections;

public class ExplosionBehaviour : MonoBehaviour
{
	private BaseSprite _sprite;
	private bool _delegateSet;
	
	// Use this for initialization
	void Start ()
	{
		_sprite = (BaseSprite)gameObject.GetComponent("BaseSprite");
		_delegateSet = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!_delegateSet) {
			//_sprite.sprite.SetAnimCompleteDelegate(new Sprite.AnimCompleteDelegate(ExplosionDeath));
			_delegateSet = true;
		}
		if(_sprite.IsAnimationNotRunning("idle")) { //TODO - define default animation otherwise if needed
			_sprite.PlayAnimation("idle");
		}
	}
	
	void ExplosionDeath() {
		_sprite.DestroySprite();
		Destroy (gameObject);
	}
}

