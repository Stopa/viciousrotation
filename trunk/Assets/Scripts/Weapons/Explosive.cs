using UnityEngine;
using System.Collections;

public class Explosive: Weapon {
	
	public float _area;
	
	public Explosive(string name, string type, float range, float area, int damage, float cooldown) {
		_name = name;
		_type = "explosive";
		_range = range;
		_area = area;
		_damage = damage;
		_cooldown = cooldown;
	}
	
	public void GetExplosionArea(Vector3 position) {
		
	    // get all colliders touching or inside the imaginary sphere:
		/*GameObject mySphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		mySphere.transform.localScale = new Vector3(_area*2, 1, _area*2);
		mySphere.transform.position = position;
		
	    Collider[] colliders = Physics.OverlapSphere(position, _area);
	    foreach(Collider h in colliders) {
	        if (!h) continue; // avoid null references (should not occur, but...)

			if(h.gameObject.tag == "Enemy") {
				Debug.Log(h.gameObject);
				Debug.DrawLine(position, h.transform.position, Color.yellow);
				BaseCharacter bc = (BaseCharacter)h.GetComponent("BaseCharacter");
				bc.AdjustCurrentHealth(-(_damage));
			}
			//if (hit.rigidbody){
	            //hit.rigidbody.AddExplosionForce(power, explosionPos, radius, 3.0);
	    }	
	    */	
	}

}
