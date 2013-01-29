using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	public int _damage;

	void Start () {
	}
	
	void Update () {
	
	}
	
    void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Enemy") {
        	Debug.Log(other.gameObject.name + " received " + _damage + " damage");
			BaseCharacter bc = other.gameObject.GetComponent("BaseCharacter") as BaseCharacter;
			bc.AdjustCurrentHealth(-_damage);
			//other.rigidbody.AddExplosionForce(1000.0f, gameObject.transform.position, 3, 3.0f);
		}
    }
	
}
