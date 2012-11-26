using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
	
	//public GameObject target;
	public float attackTimer;
	public float coolDown;
	private string _attackType;
	
	// Use this for initialization
	void Start () {
		attackTimer = 0;
		coolDown = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(attackTimer > 0)
			attackTimer -= Time.deltaTime;
		
		if(Input.GetButtonDown("Fire1")) {	
			GameObject target = FindClickTarget();
			
			if(target != null) {
				if(target.tag == "enemy" && attackTimer <= 0) {
					Debug.Log("Hit enemy: " + target.name);
					Attack(target);
					attackTimer = coolDown;
				}
				else if(target.tag == "friendly") {
					Debug.Log("Hit friendly: " + target.name);
				}
			}	
		}
	}
	
	private void Attack(GameObject target) {
		float distance = Vector3.Distance(target.transform.position, transform.position);
		Debug.Log(distance);
		
		if(distance < 4){
			PlayerHealth h = (PlayerHealth)target.GetComponent("PlayerHealth");
			h.AdjustCurrentHealth(-10);
		}		
		/*
		float distance = Vector3.Distance(target.transform.position, transform.position);
		//both vectors are 1 unit long	
		Vector3 dir = (target.transform.position - transform.position).normalized;
		float direction = Vector3.Dot(dir, transform.forward);
		//Debug.Log(transform.rotation + " dir: " + dir);
		*/
	}
	
	private GameObject FindClickTarget() {
		Vector3 playerPos = transform.position;
		playerPos.y = 0;
		Plane ground = new Plane(Vector3.up, playerPos);
		
		Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		//wtf is the distance supposed to be??
		float dist = 100.0f;
		
		//if clicked on ground, get clickpoint
		if(ground.Raycast(cameraRay, out dist)){
			Vector3 clickPoint = new Vector3(cameraRay.GetPoint(dist).x, playerPos.y, cameraRay.GetPoint(dist).z);
         	Debug.Log("clickpoint " + clickPoint);
		}
		
		//if clicked on another object
		RaycastHit hit;
		if(Physics.Raycast(cameraRay, out hit, dist)) {
			//Debug.Log(hit.distance);
			return hit.collider.gameObject;
		}
		
		return null;
	}
}
