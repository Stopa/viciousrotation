using UnityEngine;
using System.Collections;

public class SmokeBeardBehaviour : BaseCharacter
{
	public Transform _target;
	public bool _transformed;
	
	private bool _busy;
	private float _attackCooldown;
	private float _attackTimer;
	private PlayerCharacter _player;
	
	// Use this for initialization
	void Awake ()
	{
		MaxHealth = 50;
		Health = MaxHealth;
		Name = "SmokeBeard";
		Speed = 0.1f;
		_transformed = false;
		_busy = false;
		_attackCooldown = 2f;
		_attackTimer = _attackCooldown;
		
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		_target = go.transform;
		_player = go.GetComponent("PlayerCharacter") as PlayerCharacter;
		_sprite = (BaseSprite)gameObject.GetComponent("BaseSprite");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(_busy) {
			UpdateAnimations();
			return;
		}
		
		if(Health <= 0) {
			Debug.Log ("OK he is dead now but what do?");
			return;
		}
		
		if(_transformed) {
			if(_attackTimer <= 0) {
				_actionTaken = ActionTaken.RangedAttack;
				_attackTimer = _attackCooldown;
				
				Vector3 player = _target.position;
				Vector3 me = transform.position;
				
				if(player.z >= me.z) {
					_verticalLookingDirection = VerticalLookingDirection.Middle;
				} else {
					_verticalLookingDirection = VerticalLookingDirection.Down;
				}
				
				if(player.x > me.x) {
					_horisontalLookingDirection = HorisontalLookingDirection.Right;
				} else {
					_horisontalLookingDirection = HorisontalLookingDirection.Left;
				}
			} else {
				_attackTimer = _attackTimer - Time.deltaTime;
				_actionTaken = ActionTaken.Walk;
				
				Vector3 moveDirection = new Vector3(_target.position.x-transform.position.x,0,_target.position.z-transform.position.z);
				transform.position += moveDirection * Speed * Time.deltaTime;
			}
		} else {
			_actionTaken = ActionTaken.Idle;
		}
		
		UpdateAnimations();
	}
	
	void UpdateAnimations() {
		BaseEnemyCharacter charBehaviour = (BaseEnemyCharacter)gameObject.GetComponent("BaseEnemyCharacter");
		if(charBehaviour && !charBehaviour.Existing) {
			charBehaviour.Existing = true;
		}
		
		string animationName = "";
		
		if(_actionTaken == ActionTaken.Idle || _actionTaken == ActionTaken.Walk) {
			if(_transformed) {
				animationName = "walk";
			} else {
				animationName = "idle_l";
			}
		} else if(_actionTaken == ActionTaken.RangedAttack) {
			animationName = "charge_";
			animationName += AttackDirection();
			_busy = true;
			_sprite.sprite.SetAnimCompleteDelegate(new Sprite.AnimCompleteDelegate(AttackCharged));
		} else if(_actionTaken == ActionTaken.MeleeAttack) {
			animationName = "discharge_";
			animationName += AttackDirection();
			_sprite.sprite.SetAnimCompleteDelegate(new Sprite.AnimCompleteDelegate(AttackComplete));
		} else if(_actionTaken == ActionTaken.Death) {
			animationName = "transform";
			_sprite.sprite.SetAnimCompleteDelegate(new Sprite.AnimCompleteDelegate(TransformationComplete));
		}
		
		if(_sprite.IsAnimationNotRunning(animationName)) {
			if(_actionTaken == ActionTaken.Death) {
				_sprite.PlayAnimation(animationName);
			} else {
				_sprite.PlayAnimationIfCanInterrupt(animationName);
			}
		}
	}
	
	void AttackCharged() {
		_actionTaken = ActionTaken.MeleeAttack;
		_sprite._animationRunning = false;
		Vector3 targetPosition;
		targetPosition.y = transform.position.y;
		
		if(_verticalLookingDirection == VerticalLookingDirection.Down) {
			targetPosition.x = transform.position.x;
			targetPosition.z = transform.position.z-8;
		} else {
			targetPosition.z = transform.position.z;
			if(_horisontalLookingDirection == HorisontalLookingDirection.Left) {
				targetPosition.x = transform.position.x-8;
			} else {
				targetPosition.x = transform.position.x+8;
			}
		}
		
		Instantiate (Resources.Load ("Prefabs/NPCs/Enemies/SmokeBeard/SmokeBeardBullet"), targetPosition, Quaternion.identity);
	}
	
	void AttackComplete() {
		_busy = false;
		_sprite._animationRunning = false;
		_sprite.ResetDelegate();
	}
	
	public void DoTransform() {
		_busy = true;
		_actionTaken = ActionTaken.Death; // it's so deep
		_sprite.sprite.SetAnimCompleteDelegate (new Sprite.AnimCompleteDelegate(TransformationComplete));
	}
	
	void TransformationComplete() {
		_busy = false;
		_transformed = true;
		_sprite._animationRunning = false;
	}
	
	string AttackDirection() {
		if(_horisontalLookingDirection == HorisontalLookingDirection.Left) {
			return "l";
		} else if(_horisontalLookingDirection == HorisontalLookingDirection.Right) {
			return "r";
		} else {
			return "d";
		}
	}
}

