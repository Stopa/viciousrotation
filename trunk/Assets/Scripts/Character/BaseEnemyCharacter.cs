using UnityEngine;
using System;
using System.Collections;

public class BaseEnemyCharacter: BaseCharacter {
	
	public Transform _target;
	public float _aggroDistance;
	public int _minimumDamage;
	public int _maximumDamage;
	public float _accuracyPercent;
	public float _attackRange;
	public float _attackSpeed;
	
	protected bool _existingNPC;
	
	private float _attackTimer;
	private System.Random _random;
	private PlayerCharacter _player;
	private bool dead;
	
	public bool Existing {
		get{ return _existingNPC; }
		set{ _existingNPC = value; }
	}
	
	void Awake(){
		base.InitAttributes("Zombie Cube", 1, 20, 20);
	}

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		_target = go.transform;
		_player = go.GetComponent("PlayerCharacter") as PlayerCharacter;
		_sprite = (BaseSprite)gameObject.GetComponent("BaseSprite");
		_attackTimer = 0;
		_random = new System.Random();
	}
	
	// Update is called once per frame
	void Update () {
		if(_attackTimer > 0) {
			_attackTimer -= Time.deltaTime;
		}
		if(Health <= 0) {
			if(!dead) {
				_sprite.sprite.SetAnimCompleteDelegate(new Sprite.AnimCompleteDelegate(DeathAnimationComplete));
				_actionTaken = ActionTaken.Death;
				dead = true;
			}
			UpdateAnimations();
			return;
		}
		if(!_existingNPC) {
			UpdateAnimations();
			return;
		}
		float distance = Vector3.Distance(_target.position, transform.position);
		if(distance <= _attackRange && _attackTimer <= 0) {
			_attackTimer = _attackSpeed;
			float accuracyLuck = (float)_random.NextDouble();
			if(accuracyLuck < _accuracyPercent) {
				int damage = _random.Next(_minimumDamage, _maximumDamage);
				_player.Health = _player.Health - damage;
			} else {
				//Missed
			}
			if(_target.position.x < transform.position.x) {
				_horisontalLookingDirection = BaseCharacter.HorisontalLookingDirection.Left;
			} else {
				_horisontalLookingDirection = BaseCharacter.HorisontalLookingDirection.Right;
			}
			_actionTaken = ActionTaken.MeleeAttack;
			//attack
		} else if(distance <= _aggroDistance) {
			//move
			// TODO - y coordinate seems to fuck up somewhere. not sure where.
			Vector3 moveDirection = new Vector3(_target.position.x-transform.position.x,0,_target.position.z-transform.position.z);
			transform.position += moveDirection * Speed * Time.deltaTime;
			if(moveDirection.x < 0) {
				_horisontalLookingDirection = BaseCharacter.HorisontalLookingDirection.Left;
			} else {
				_horisontalLookingDirection = BaseCharacter.HorisontalLookingDirection.Right;
			}
			_actionTaken = ActionTaken.Walk;
		} else {
			double directionRandom = _random.NextDouble();
			if(directionRandom > .5) {
				_horisontalLookingDirection = BaseCharacter.HorisontalLookingDirection.Left;
			} else {
				_horisontalLookingDirection = BaseCharacter.HorisontalLookingDirection.Right;
			}
			_actionTaken = ActionTaken.Idle;
		}
		UpdateAnimations();
	}
	
	private void DropItem() {
		Vector3 pos = transform.position;
		pos.y = 1;
		GameObject item = (GameObject)Instantiate(Resources.Load("Prefabs/DroppedItem"), pos, Quaternion.identity);		
	}
	
	#region sprites
	protected virtual void UpdateAnimations() {}
	
	void DeathAnimationComplete() {
		DropItem();
		Destroy(gameObject);
		_sprite.DestroySprite();
	}
	#endregion
	
	public void InitAttributes(string name, int speed, int maxHealth, float aggroDistance, int minDmg, int maxDmg, float accuracy, float atkRange, float atkSpd) {
		this.name = name;
		Speed = speed;
		MaxHealth = maxHealth;
		Health = maxHealth;
		this._aggroDistance = aggroDistance;
		this._minimumDamage = minDmg;
		this._maximumDamage = maxDmg;
		this._accuracyPercent = accuracy;
		this._attackRange = atkRange;
		this._attackSpeed = atkSpd;
	}
}
