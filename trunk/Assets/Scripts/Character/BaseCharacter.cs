using UnityEngine;
using System.Collections;

public class BaseCharacter: MonoBehaviour {
	
	private string _name;
	private int _moveSpeed;
	private int _maxHealth = 100;
	private int _curHealth = 100;
	protected HorisontalLookingDirection _horisontalLookingDirection;
	protected VerticalLookingDirection _verticalLookingDirection;
	protected ActionTaken _actionTaken;
	protected BaseSprite _sprite;
	
	#region enums
	//feel free to refactor it to something better if you get an idea
	protected enum HorisontalLookingDirection {Left, Middle, Right};
	protected enum VerticalLookingDirection {Down, Middle, Up};
	protected enum ActionTaken {Idle, Walk, MeleeAttack, RangedAttack, Death} // add other possible actions
	#endregion
	
	#region get set
	public string Name {
		get{ return _name;}
		set{ _name = value;}	
	}
	public int Speed {
		get{ return _moveSpeed;}
		set{ _moveSpeed = value;}	
	}
	public int Health {
		get{ return _curHealth;}
		set{ _curHealth = value;}	
	}
	public int MaxHealth {
		get{ return _maxHealth;}
		set{ _maxHealth = value;}	
	}
	#endregion
	
	void Awake() {
		_name = string.Empty;
		_moveSpeed = 0;
		_maxHealth = 0;
		_curHealth = 0;
	}
	
	void Start() {	
	}
	
	void Update() {		
	}
	
	#region health
	public void AdjustCurrentHealth(int adj) {
		_curHealth += adj;
		
		if(_curHealth < 0)
			_curHealth = 0;
		if(_curHealth > _maxHealth)
			_curHealth = _maxHealth;
		if(_maxHealth < 1)
			_maxHealth = 1;		
	}
	
	#endregion
	
	public void InitAttributes(string name, int speed, int maxHealth, int health) {
		_name = name;
		_moveSpeed = speed;
		_maxHealth = maxHealth;
		_curHealth = health;
	}
}
