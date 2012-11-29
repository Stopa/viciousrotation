using UnityEngine;
using System.Collections;

public class PlayerCharacter: BaseCharacter {
	
	public CharacterController _playerCharacterController;
	
	private ArrayList _weapons;
	private float _attackTimer;
	
	void Awake(){
		Name = "P. McPlayer";
		Speed = 4;
		MaxHealth = 100;
		Health = 100;
		Weapon = null;
	}
	
	// Use this for initialization
	void Start () {
		_playerCharacterController = (CharacterController)gameObject.GetComponent("CharacterController");
		
		_attackTimer = 0;
		
		InitWeapons();
		DrawCharacterSprite();
	}
	
	// Update is called once per frame
	void Update () {
		//MOVEMENT
		Vector3 moveDirection = Vector3.zero;
		
		if(_playerCharacterController.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	        moveDirection = transform.TransformDirection(moveDirection);
        	moveDirection *= 10;
		}
		moveDirection.y -= 20 * Time.deltaTime;
		_playerCharacterController.Move(moveDirection * Time.deltaTime);
		
		//ATTACK
		if(_attackTimer > 0)
			_attackTimer -= Time.deltaTime;
		//TEMP change weapon		
		if(Input.GetKeyUp(KeyCode.C)) {
			ChangeWeapon();
		}
		//TEMP attack
		if(Input.GetButtonDown("Fire1")) {	
			GameObject target = FindClickTarget();
			
			if(target != null) {
				if(target.tag == "enemy" && _attackTimer <= 0) {
					Debug.Log("Hit enemy: " + target.name);
					Attack(target);
					_attackTimer = Weapon._cooldown;
				}
				else if(target.tag == "friendly") {
					Debug.Log("Hit friendly: " + target.name);
				}
			}	
		}
		
	}
	#region init	
	void InitWeapons() {
		_weapons = new ArrayList();
		_weapons.Add(new Weapon("melee_1", "melee", 5.0f, 5, 1.0f));
		_weapons.Add(new Weapon("melee_2", "melee", 4.0f, 10, 1.0f));
		_weapons.Add(new Weapon("ranged_1", "ranged", 15.0f, 15, 3.0f));
		Weapon = _weapons[0] as Weapon;
		
	}
	#endregion
	
	void DrawCharacterSprite() {
		GameObject refGameObject = GameObject.Find("PlayerCharacterSpriteManager");
		SpriteManager mySpriteManager = (SpriteManager)refGameObject.GetComponent("LinkedSpriteManager");

		Sprite playerSprite = mySpriteManager.AddSprite(gameObject,3f,6f, 0,0,64,128,true);
		UVAnimation animation1 = new UVAnimation();
		Vector2 startPosUV = mySpriteManager.PixelCoordToUVCoord(0, 128);
        Vector2 spriteSize = mySpriteManager.PixelSpaceToUVSpace(64, 128);

        animation1.BuildUVAnim(startPosUV, spriteSize, 8, 1, 8, 8);
        animation1.name = "walk_right";
        animation1.loopCycles = -1;
		
		playerSprite.AddAnimation(animation1);
		playerSprite.PlayAnim("walk_right");
		
	}
	
	void ChangeWeapon() {
		int index = (_weapons.IndexOf(Weapon) + 1)%3;	
		Weapon = _weapons[index] as Weapon;
		Debug.Log("Changed weapon to: " + Weapon._name + ", damage: " + Weapon._damage);
	}
	
	
	GameObject FindClickTarget(){
		Vector3 playerPos = transform.position;
		playerPos.y = 0;
		Plane ground = new Plane(Vector3.up, playerPos);
		
		Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		//wtf is the distance supposed to be??
		float dist = 100.0f;
		
		//if clicked on ground, get clickpoint
		if(ground.Raycast(cameraRay, out dist)){
			Vector3 clickPoint = new Vector3(cameraRay.GetPoint(dist).x, playerPos.y, cameraRay.GetPoint(dist).z);
         	//Debug.Log("clickpoint " + clickPoint);
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
