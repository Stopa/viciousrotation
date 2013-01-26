using UnityEngine;
using System.Collections;

public class PassiveBehaviour : MonoBehaviour
{
	private BaseSprite _sprite;
	
	// Use this for initialization
	void Start ()
	{
		_sprite = (BaseSprite)gameObject.GetComponent("BaseSprite");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(_sprite.IsAnimationNotRunning("idle_l")) { //TODO - define default animation otherwise if needed
			_sprite.PlayAnimation("idle_l");
		}
	}
}

