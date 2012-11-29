using UnityEngine;
using System.Collections;

public class EnemyCharacter: BaseCharacter {
	
	public Transform _target;
	public float _rotationSpeed;
	public float _aggroDistance;
	
	void Awake(){
		Name = "Zombie Cube";
		Speed = 1;
		MaxHealth = 100;
		Health = 100;
		Weapon = new Weapon("melee_fist", "melee", 4.0f, 10, 1.0f);
	}

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		_target = go.transform;
	}
	
	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance(_target.position, transform.position);
		if(distance <= Weapon._range) {
			//attack
		}
		else if(distance <= _aggroDistance) {
			//rotate towards player
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_target.position - transform.position), _rotationSpeed * Time.deltaTime);	
			//move
			transform.position += transform.forward * Speed * Time.deltaTime;	
		}
	}
}
