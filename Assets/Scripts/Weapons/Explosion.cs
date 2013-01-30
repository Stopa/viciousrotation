using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	public int _damage;
	public bool _isEnemy;
	
    void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Enemy" || other.gameObject.tag=="Player"&&_isEnemy || other.gameObject.tag == "BossMonster") {
        	Debug.Log(other.gameObject.name + " received " + _damage + " damage");
			BaseCharacter bc = other.gameObject.GetComponent("BaseCharacter") as BaseCharacter;
			bc.AdjustCurrentHealth(-_damage);
			if(other.rigidbody) {
				other.rigidbody.AddExplosionForce(1000.0f, gameObject.transform.position, 3, 3.0f);
			}
		}
    }
	
}
