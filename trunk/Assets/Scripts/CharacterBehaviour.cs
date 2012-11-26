using UnityEngine;
using System.Collections;

public class CharacterBehaviour : MonoBehaviour {
	
	public GameObject _playerGameObject;
	public CharacterController _playerCharacterController;

	// Use this for initialization
	void Start () {
		_playerGameObject = GameObject.Find("PlayerCharacterObject");
		_playerCharacterController = (CharacterController)_playerGameObject.GetComponent("CharacterController");
		drawCharacterSprite();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 moveDirection = Vector3.zero;
		
		if(_playerCharacterController.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	        moveDirection = transform.TransformDirection(moveDirection);
        	moveDirection *= 10;
		}
		moveDirection.y -= 20 * Time.deltaTime;
		_playerCharacterController.Move(moveDirection * Time.deltaTime);
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

		Sprite playerSprite = mySpriteManager.AddSprite(_playerGameObject,3f,6f, 0,0,64,128,true);
		UVAnimation animation1 = new UVAnimation();
		Vector2 startPosUV = mySpriteManager.PixelCoordToUVCoord(0, 128);
        Vector2 spriteSize = mySpriteManager.PixelSpaceToUVSpace(64, 128);

        animation1.BuildUVAnim(startPosUV, spriteSize, 8, 1, 8, 8);
        animation1.name = "walk_right";
        animation1.loopCycles = -1;
		
		playerSprite.AddAnimation(animation1);
		playerSprite.PlayAnim("walk_right");
		
	}
}
