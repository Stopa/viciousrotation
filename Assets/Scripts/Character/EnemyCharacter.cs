using UnityEngine;
using System.Collections;

public class EnemyCharacter: BaseCharacter {
	
	public Transform _target;
	public float _rotationSpeed;
	public float _aggroDistance;
	BaseSprite sprite;
	
	void Awake(){
		InitWeapons();
		InitAttributes("Zombie Cube", 1, 20, 20);
	}

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		_target = go.transform;
		sprite = (BaseSprite)gameObject.GetComponent("BaseSprite");
	}
	
	// Update is called once per frame
	void Update () {
		if(Health <= 0) {
			DropItem();
			Destroy(gameObject);
			sprite.DestroySprite();
			return;
		}
		float distance = Vector3.Distance(_target.position, transform.position);
		if(distance <= Weapon._range) {
			//attack
		}
		else if(distance <= _aggroDistance) {
			//rotate towards player - no longer needed as enemies are now sprites
			//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_target.position - transform.position), _rotationSpeed * Time.deltaTime);	
			//move
			// TODO - y coordinate seems to fuck up somewhere. not sure where.
			Vector3 moveDirection = new Vector3(_target.position.x-transform.position.x,_target.position.y-transform.position.y,0);
			transform.position += moveDirection * Speed * Time.deltaTime;
			SpriteWalkAnimation();
		} else {
			SpriteIdleAnimation();
		}
	}
	
	#region init	
	void InitWeapons() {
		Weapon = new Weapon("melee_fist", "melee", 4.0f, 10, 1.0f);		
	}
	
	void InitAnimations() {	
		
	}
	#endregion
	
	private void DropItem() {
		Vector3 pos = transform.position;
		pos.y -= 1;
		GameObject item = (GameObject)Instantiate(Resources.Load("Prefabs/DroppedItem"), pos, Quaternion.identity);		
	}
	
	#region sprites
	void SpriteWalkAnimation() {
		if(sprite.IsAnimationNotRunning("move")) {
			sprite.PlayAnimation("move");
		}
	}
	
	void SpriteIdleAnimation() {
		if(sprite.IsAnimationNotRunning("idle")) {
			sprite.PlayAnimation("idle");
		}
	}
	#endregion
}
