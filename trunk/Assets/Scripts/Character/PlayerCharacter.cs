using UnityEngine;
using System.Collections;

public class PlayerCharacter: BaseCharacter {
	
	public CharacterController _playerCharacterController;
	private float _attackTimer;
	private Vector3 _clickPoint;
	private Sprite playerSprite;
	private SpriteManager characterSpriteManager;
	
	public Inventory _inventory;
	private ArrayList _weapons;
	public Explosive _curBomb;
	
	void Awake(){
		InitAttributes("P. McPlayer", 4, 100,100);
		_inventory = new Inventory();
		InitWeapons();
		InitItems();
	}
	
	// Use this for initialization
	void Start () {
		_playerCharacterController = (CharacterController)gameObject.GetComponent("CharacterController");
		
		_attackTimer = 0;
		
		InitCharacterSprite();
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
		
		string animationName = null;
		//determine animations
		if(moveDirection.z > 0 && moveDirection.x == 0) {
			animationName = "walk_t";
		} else if(moveDirection.z > 0 && moveDirection.x > 0) {
			animationName = "walk_tr";
		} else if(moveDirection.z == 0 && moveDirection.x > 0) {
			animationName = "walk_r";
		} else if(moveDirection.z < 0 && moveDirection.x > 0) {
			animationName = "walk_dr";
		} else if(moveDirection.z < 0 && moveDirection.x == 0) {
			animationName = "walk_d";
		} else if(moveDirection.z < 0 && moveDirection.x < 0) {
			animationName = "walk_dl";
		} else if(moveDirection.z == 0 && moveDirection.x < 0) {
			animationName = "walk_l";
		} else if(moveDirection.z > 0 && moveDirection.x < 0) {
			animationName = "walk_tl";
		}
		
		if(animationName != null) {
			if(playerSprite.GetCurAnim() == null || playerSprite.GetCurAnim().name != animationName) {
				playerSprite.PlayAnim(animationName);
			}
		} else {
			if(playerSprite.GetCurAnim() != null) {
				playerSprite.PlayAnim(playerSprite.GetCurAnim().name.Replace ("walk", "idle"));
			}
		}
		
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
		//THROW BOMB
		else if(Input.GetButtonDown("Fire2")) {	
			if(_curBomb._amount > 0) {
				GameObject target = FindClickTarget();
				GameObject explosion = (GameObject)Instantiate(Resources.Load("Prefabs/MyExplosion"), _clickPoint, Quaternion.identity);
				Explosion expScript = explosion.GetComponent("Explosion") as Explosion;
				expScript._damage = 10;
			}
		}	
	}
	
	#region init	
	private void InitWeapons() {
		_weapons = new ArrayList();
		_weapons.Add(new Weapon("melee_1", "melee", 5.0f, 5, 1.0f));
		_weapons.Add(new Weapon("melee_2", "melee", 4.0f, 10, 1.0f));
		_weapons.Add(new Weapon("ranged_1", "ranged", 15.0f, 15, 3.0f));
		Weapon = _weapons[0] as Weapon;	
		
		Weapon w = _weapons[0] as Weapon;
		w._icon = Resources.Load("Item/Icon/melee_1") as Texture2D;
		w = _weapons[1] as Weapon;
		w._icon = Resources.Load("Item/Icon/melee_2") as Texture2D;
		w = _weapons[2] as Weapon;
		w._icon = Resources.Load("Item/Icon/ranged") as Texture2D;
		
		Explosive e = new Explosive("bomb_1", "bomb", 15.0f, 4.0f, 15, 2.0f);
		e._icon = Resources.Load("Item/Icon/bomb_1") as Texture2D;
		_inventory.AddItem(e);
		
		_curBomb = e;
	}
	
	private void InitItems() {
		Ingredient i = new Ingredient("ingredient_1");
		i._icon = Resources.Load("Item/Icon/ingredient_1") as Texture2D;
		i._amount = 1;
		_inventory.AddItem(i);
		
		Ingredient i2 = new Ingredient("ingredient_2");
		i2._icon = Resources.Load("Item/Icon/ingredient_2") as Texture2D;
		i2._amount = 2;
		_inventory.AddItem(i2);
		
		Formula f = new Formula("formula_1");
		f._icon = Resources.Load("Item/Icon/formula_1") as Texture2D;
		f._ingredients.Add(i, 1);
		_inventory.AddItem(f);
		
		Formula f2 = new Formula("formula_2");
		f2._icon = Resources.Load("Item/Icon/formula_2") as Texture2D;
		f2._ingredients.Add(i, 3);
		f2._ingredients.Add(i2, 1);
		_inventory.AddItem(f2);
	}
	#endregion
	
	/*
	 * Change equipped weapon
	 */
	private void ChangeWeapon() {
		int index = (_weapons.IndexOf(Weapon) + 1)%_weapons.Count;	
		_attackTimer = 0;
		Weapon = _weapons[index] as Weapon;
		Debug.Log("Changed weapon to: " + Weapon._name + ", damage: " + Weapon._damage);
	}

	/*
	 * Change equipped bomb
	 */
	private void ChangeBomb(Explosive e) {
		_curBomb = e;	
		Debug.Log("Changed bomb to: " + e._name + ", damage: " + e._damage);
	}
	
	/*
	 * Add item to player's 'inventory'
	 */
	public void AddItem(Item item) {
		_inventory.AddItem(item);
	}
		
	#region graphics
	
	private void InitCharacterSprite() {
		GameObject refGameObject = GameObject.Find("PlayerCharacterSpriteManager");
		characterSpriteManager = (SpriteManager)refGameObject.GetComponent("LinkedSpriteManager");

		playerSprite = characterSpriteManager.AddSprite(gameObject,4.47f,6.71f, 0,96,64,96,true);
		
		CreateCharacterAnimations();
	}
	
	private void CreateCharacterAnimations() {
		Vector2 spriteUVSize = characterSpriteManager.PixelSpaceToUVSpace(64,96);
		
		AddAnimation("walk_dl",	spriteUVSize, 64, 96, 8);
		AddAnimation("walk_d",	spriteUVSize, 64, 192, 8);
		AddAnimation("walk_dr",	spriteUVSize, 64, 288, 8);
		AddAnimation("walk_l",	spriteUVSize, 64, 384, 8);
		AddAnimation("walk_tl",	spriteUVSize, 64, 480, 8);
		AddAnimation("walk_r",	spriteUVSize, 64, 576, 8);
		AddAnimation("walk_tr",	spriteUVSize, 64, 672, 8);
		AddAnimation("walk_t",	spriteUVSize, 64, 768, 8);
		
		AddAnimation("idle_dl",	spriteUVSize, 0, 96,  1);
		AddAnimation("idle_d",	spriteUVSize, 0, 192, 1);
		AddAnimation("idle_dr",	spriteUVSize, 0, 288, 1);
		AddAnimation("idle_l",	spriteUVSize, 0, 384, 1);
		AddAnimation("idle_tl",	spriteUVSize, 0, 480, 1);
		AddAnimation("idle_r",	spriteUVSize, 0, 576, 1);
		AddAnimation("idle_tr",	spriteUVSize, 0, 672, 1);
		AddAnimation("idle_t",	spriteUVSize, 0, 768, 1);
	}
	
	// assumes animation is on one row of the spritesheet
	private void AddAnimation(string name, Vector2 spriteSize,int startLeft, int startBottom, int animationLength) {
		UVAnimation animation = new UVAnimation();
		animation.name = name;
		animation.loopCycles = -1;
		animation.BuildUVAnim(characterSpriteManager.PixelCoordToUVCoord(startLeft, startBottom), spriteSize, animationLength, 1, animationLength, 8);
		playerSprite.AddAnimation(animation);
	}
	#endregion
		
	
	private GameObject FindClickTarget(){
		Vector3 playerPos = transform.position;
		playerPos.y = 0;
		Plane ground = new Plane(Vector3.up, playerPos);
		
		Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		//wtf is the distance supposed to be??
		float dist = 100.0f;
		
		//if clicked on ground, get clickpoint
		if(ground.Raycast(cameraRay, out dist)){
			_clickPoint = new Vector3(cameraRay.GetPoint(dist).x, playerPos.y, cameraRay.GetPoint(dist).z);
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
