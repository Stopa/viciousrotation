using UnityEngine;
using System.Collections;

public class CharacterBehaviour : MonoBehaviour {
	
	public GameObject _playerGameObject;
	public CharacterController _playerCharacterController;
	public PlayerCharacter _player;
	
	// Use this for initialization
	void Start () {
		_playerGameObject = GameObject.Find("PlayerCharacterObject");
		_playerCharacterController = (CharacterController)_playerGameObject.GetComponent("CharacterController");
		//_player = new Player(100, 4);
		//InitWeapons();
		//drawCharacterSprite();
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
	/*
	void InitWeapons() {
		_player._weapons = new ArrayList();
		_player._weapons.Add(new Weapon("melee_1", "melee", 5.0f, 5, 1.0f));
		_player._weapons.Add(new Weapon("melee_2", "melee", 4.0f, 10, 1.0f));
		_player._weapons.Add(new Weapon("ranged_1", "ranged", 15.0f, 15, 3.0f));
		_player.Weapon = _player._weapons[0] as Weapon;
		
	}
	
	public void ChangeWeapon() {
		int index = (_player._weapons.IndexOf(_player._curWeapon) + 1)%3;	
		_player._curWeapon = _player._weapons[index] as Weapon;
		Debug.Log("Changed weapon to: " + _player._curWeapon._name + ", damage: " + _player._curWeapon._damage);
	}
	
	public void Attack() {

	}
	*/
	//public void 
	
}