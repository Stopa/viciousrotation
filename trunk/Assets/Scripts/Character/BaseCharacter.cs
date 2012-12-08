using UnityEngine;
using System.Collections;

public class BaseCharacter: MonoBehaviour {
	
	private string _name;
	private int _moveSpeed;
	private int _maxHealth = 100;
	private int _curHealth = 100;
	private Weapon _curWeapon;
	
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
	public Weapon Weapon {
		get{ return _curWeapon;}
		set{ _curWeapon = value;}	
	}
	#endregion
	
	void Awake() {
		_name = string.Empty;
		_moveSpeed = 0;
		_maxHealth = 0;
		_curHealth = 0;
		_curWeapon = null;
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
	
	#region attacks
	public void Attack(GameObject target) {
		float distance = Vector3.Distance(target.transform.position, transform.position);
		Debug.Log(distance);
		
		if(distance <= _curWeapon._range){	
			Debug.Log("Damage to " +target.tag + ": " + _curWeapon._damage);
			BaseCharacter bc = (BaseCharacter)target.GetComponent("BaseCharacter");
			bc.AdjustCurrentHealth(-(_curWeapon._damage));
		}		
	}
	#endregion
	
	public void InitAttributes(string name, int speed, int maxHealth, int health) {
		_name = name;
		_moveSpeed = speed;
		_maxHealth = maxHealth;
		_curHealth = health;
	}
}
