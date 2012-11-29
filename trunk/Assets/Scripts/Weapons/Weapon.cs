using UnityEngine;
using System.Collections;

public class Weapon {
	
	public string _name;
	public string _type;
	public float _range;
	public int _damage;
	public float _cooldown;
	
	public Weapon() {
	
	}
	
	public Weapon(string name, string type, float range, int damage, float cooldown) {
		_name = name;
		_type = type;
		_range = range;
		_damage = damage;
		_cooldown = cooldown;
	}
}
