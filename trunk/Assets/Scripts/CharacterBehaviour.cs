using UnityEngine;
using System.Collections;

public class CharacterBehaviour : MonoBehaviour {
	
	public GameObject playerGameObject;
	public CharacterController playerCharacterController;

	// Use this for initialization
	void Start () {
		playerGameObject = GameObject.Find("PlayerCharacterObject");
		playerCharacterController = (CharacterController)playerGameObject.GetComponent("CharacterController");
		drawCharacterSprite();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 moveDirection = Vector3.zero;
		
		if(playerCharacterController.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	        moveDirection = transform.TransformDirection(moveDirection);
        	moveDirection *= 10;
		}
		moveDirection.y -= 20 * Time.deltaTime;
		playerCharacterController.Move(moveDirection * Time.deltaTime);
		/*if(Input.GetButton("Vertical")) {
			Vector3 direction;
			if(Input.GetAxis("Vertical") > 0) {
				direction = new Vector3(0,0,speed);
			} else {
				direction = new Vector3(0,0,-speed);
			}
			playerCharacterController.Move(direction);
		}
		if(Input.GetButton ("Horizontal")) {
			Vector3 direction;
			if(Input.GetAxis("Horizontal") > 0) {
				direction = new Vector3(speed,0,0);
			} else {
				direction = new Vector3(-speed,0,0);
			}
			playerCharacterController.Move(direction);
		}*/
	}
	
	void drawCharacterSprite() {
		GameObject refGameObject = GameObject.Find("PlayerCharacterSpriteManager");
		SpriteManager mySpriteManager = (SpriteManager)refGameObject.GetComponent("LinkedSpriteManager");
		
		mySpriteManager.AddSprite(playerGameObject,4f,4f, 0,0,64,128,true);
	}
}
