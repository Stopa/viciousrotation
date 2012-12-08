using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	public Explosive _explosive;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "enemy") {
        	Debug.Log(other.gameObject.name);
			//other.rigidbody.AddExplosionForce(1000.0f, gameObject.transform.position, 3, 3.0f);
		}
    }
}
