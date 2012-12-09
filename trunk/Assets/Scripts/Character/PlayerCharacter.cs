using UnityEngine;
using System.Collections;

public class PlayerCharacter: BaseCharacter {
	
	public CharacterController _playerCharacterController;
	
	private ArrayList _weapons;
	private float _attackTimer;
	private Vector3 _clickPoint;
	private Sprite playerSprite;
	private SpriteManager characterSpriteManager;
	
	public ArrayList _ingredients;
	public ArrayList _formulas;
	public ArrayList _bombs;
	
	void Awake(){
		InitAttributes("P. McPlayer", 4, 100,100);
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
		} else if(moveDirection.z < 0 && moveDirection.x > 0) {
			animationName = "walk_tl";
		}
		
		if(animationName != null) {
			if(playerSprite.getCurAnim() == null || playerSprite.getCurAnim().name != animationName) {
				playerSprite.PlayAnim(animationName);
			}
		} else {
			characterSpriteManager.StopAnimation(playerSprite);
			
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
			else if(Weapon._type == "bomb") {
				Explosive e = Weapon as Explosive;
				Debug.Log("Bomb blast radius" + e._area);
				e.GetExplosionArea(_clickPoint);
				
			}
			
		}
		
	}
	#region init	
	private void InitWeapons() {
		_weapons = new ArrayList();
		_weapons.Add(new Weapon("melee_1", "melee", 5.0f, 5, 1.0f));
		_weapons.Add(new Weapon("melee_2", "melee", 4.0f, 10, 1.0f));
		_weapons.Add(new Weapon("ranged_1", "ranged", 15.0f, 15, 3.0f));
		_weapons.Add(new Explosive("bomb_1", "bomb", 15.0f, 4.0f, 15, 2.0f));
		Weapon = _weapons[0] as Weapon;	
		
		Weapon w = _weapons[0] as Weapon;
		w._icon = Resources.Load("Item/Icon/melee_1") as Texture2D;
		w = _weapons[1] as Weapon;
		w._icon = Resources.Load("Item/Icon/melee_2") as Texture2D;
		w = _weapons[2] as Weapon;
		w._icon = Resources.Load("Item/Icon/ranged") as Texture2D;
		
		_bombs = new ArrayList();
		Explosive e = new Explosive("bomb_1", "bomb", 15.0f, 4.0f, 15, 2.0f);
		e._icon = Resources.Load("Item/Icon/bomb_1") as Texture2D;
		_bombs.Add(e);
	}
	
	private void InitItems() {
		_ingredients = new ArrayList();
		_formulas = new ArrayList();
		
		Ingredient i = new Ingredient("ingredient_1");
		i._icon = Resources.Load("Item/Icon/ingredient_1") as Texture2D;
		i._amount = 1;
		_ingredients.Add(i);
		
		Ingredient i2 = new Ingredient("ingredient_2");
		i2._icon = Resources.Load("Item/Icon/ingredient_2") as Texture2D;
		i2._amount = 2;
		_ingredients.Add(i2);
		
		Formula f = new Formula("formula_1");
		f._icon = Resources.Load("Item/Icon/formula_1") as Texture2D;
		f._ingredients.Add(i, 1);
		_formulas.Add(f);
		
		Formula f2 = new Formula("formula_2");
		f2._icon = Resources.Load("Item/Icon/formula_2") as Texture2D;
		f2._ingredients.Add(i, 3);
		f2._ingredients.Add(i2, 1);
		_formulas.Add(f2);
	}
	#endregion
	
	/*
	 * Change equipped weapon
	 */
	private void ChangeWeapon() {
		int index = (_weapons.IndexOf(Weapon) + 1)%4;	
		_attackTimer = 0;
		Weapon = _weapons[index] as Weapon;
		Debug.Log("Changed weapon to: " + Weapon._name + ", damage: " + Weapon._damage);
	}
	
	/*
	 * Add item to player's 'inventory'
	 */
	public void AddItem(Item item) {
		if(item._type == "ingredient") {
			Ingredient ing = CheckIfContains(_ingredients, item) as Ingredient;
			if(ing != null) 
				ing._amount += (item as Ingredient)._amount;
			else 
				_ingredients.Add(item as Ingredient);
			
			Debug.Log("added ingredient " + item._name);
		}
		else if(item._type == "formula")
			_formulas.Add(item as Formula);
	}
	
	/*
	 * Check if the list contains the given item
	 */
	public Item CheckIfContains(ArrayList list, Item item) {
		foreach(Item i in list) {
			if(i._name == item._name)
				return i;
		}
		return null;
	}
		
	#region graphics
	
	private void InitCharacterSprite() {
		GameObject refGameObject = GameObject.Find("PlayerCharacterSpriteManager");
		characterSpriteManager = (SpriteManager)refGameObject.GetComponent("LinkedSpriteManager");

		playerSprite = characterSpriteManager.AddSprite(gameObject,3f,6f, 0,96,64,96,true);
		
		CreateCharacterAnimations();
	}
	
	private void CreateCharacterAnimations() {
		Vector2 spriteUVSize = characterSpriteManager.PixelSpaceToUVSpace(64,96);
		
		AddAnimation("walk_dl", spriteUVSize, 96);
		AddAnimation("walk_d", spriteUVSize, 192);
		AddAnimation("walk_dr", spriteUVSize, 288);
		AddAnimation("walk_l", spriteUVSize, 384);
		AddAnimation("walk_tl", spriteUVSize, 480);
		AddAnimation("walk_r", spriteUVSize, 576);
		AddAnimation("walk_tr", spriteUVSize, 672);
		AddAnimation("walk_t", spriteUVSize, 768);
	}
	
	private void AddAnimation(string name, Vector2 spriteSize, int bottomLeftPixelCoordinate) {
		UVAnimation animation = new UVAnimation();
		animation.name = name;
		animation.loopCycles = -1;
		animation.BuildUVAnim(characterSpriteManager.PixelCoordToUVCoord(0, bottomLeftPixelCoordinate), spriteSize, 8, 1, 8, 8);
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
