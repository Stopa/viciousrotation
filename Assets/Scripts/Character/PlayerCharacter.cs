using UnityEngine;
using System.Collections;

public class PlayerCharacter: BaseCharacter {
	
	public CharacterController _playerCharacterController;
	private float _attackTimer;
	private Vector3 _clickPoint;
	private bool _canAttack;
	
	public Inventory _inventory;
	private ArrayList _weapons;
	private Weapon _curWeapon;
	public Texture2D _portrait;
	
	public Weapon Weapon {
		get{ return _curWeapon;}
		set{ _curWeapon = value;}	
	}
	
	public bool CanAttack {
		get {return _canAttack;}
		set {_canAttack = value;}
	}
	
	void Awake(){
		DontDestroyOnLoad(gameObject);
		
		InitAttributes("P. McPlayer", 4, 100,100);
		_inventory = new Inventory();
		InitWeapons();
		InitItems();
	}
	
	// Use this for initialization
	void Start () {
		_playerCharacterController = (CharacterController)gameObject.GetComponent("CharacterController");
		
		_attackTimer = 0;
		
		_sprite = (BaseSprite)gameObject.GetComponent("PlayerCharacterSprite");
		
		_canAttack = true;
	}
	
	// Update is called once per frame
	void Update () {
		_actionTaken = ActionTaken.Idle;
		//MOVEMENT
		Vector3 moveDirection = Vector3.zero;
		
		// TODO - need to control in a better way - this control made animations jerky on Unity Terrain
		//if(_playerCharacterController.isGrounded) {
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
    	moveDirection *= 10;
		//}
		moveDirection.y -= 20 * Time.deltaTime;
		_playerCharacterController.Move(moveDirection * Time.deltaTime);
		
		if(moveDirection.x != 0.0 || moveDirection.z != 0.0) {
			//update looking direction
			if(moveDirection.z > 0)
				_verticalLookingDirection = VerticalLookingDirection.Up;
			else if(moveDirection.z < 0)
				_verticalLookingDirection = VerticalLookingDirection.Down;
			else
				_verticalLookingDirection = VerticalLookingDirection.Middle;
			
			if(moveDirection.x > 0)
				_horisontalLookingDirection = HorisontalLookingDirection.Right;
			else if(moveDirection.x < 0)
				_horisontalLookingDirection = HorisontalLookingDirection.Left;
			else
				_horisontalLookingDirection = HorisontalLookingDirection.Middle;
			
			_actionTaken = ActionTaken.Walk;
		}
		
		//ATTACK
		if(_attackTimer > 0)
			_attackTimer -= Time.deltaTime;		
		
		//TEMP attack
		CheckInput();
		
		UpdateAnimations();
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
		e._amount = 3;
		_inventory.AddItem(e);		
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
	 * Add item to player's 'inventory'
	 */
	public void AddItem(Item item) {
		_inventory.AddItem(item);
	}
	

	
	#region graphics
	private void UpdateAnimations() {
		string animationName = "";
		
		if(_actionTaken == ActionTaken.Walk) {
			animationName = "walk_" + DirectionAbbreviation();
		} else if(_actionTaken == ActionTaken.MeleeAttack) {
			string vertical = "";
			string horisontal = "";
			
			if(_verticalLookingDirection == VerticalLookingDirection.Up) {
				vertical = "t";
			} else {
				vertical = "d";
			}
			
			if(_horisontalLookingDirection == HorisontalLookingDirection.Left) {
				horisontal = "l";
			} else if(_horisontalLookingDirection == HorisontalLookingDirection.Right) {
				horisontal = "r";
			}
			
			animationName = "melee_"+vertical+horisontal;
		} else if(_actionTaken == ActionTaken.RangedAttack) {
			animationName = "ranged_";
			
			if(_horisontalLookingDirection == HorisontalLookingDirection.Left) {
				animationName += "l";
			} else {
				animationName += "r";
			}
		} else if(_actionTaken == ActionTaken.Idle) {
			animationName = "idle_" + DirectionAbbreviation();
		}
		
		if(_sprite.IsAnimationNotRunning(animationName)) {
			_sprite.PlayAnimationIfCanInterrupt(animationName);
		}
	}
	
	private string DirectionAbbreviation() {
		string result = "";
		
		if(_verticalLookingDirection == VerticalLookingDirection.Up) {
			result += "t";
		} else if(_verticalLookingDirection == VerticalLookingDirection.Down) {
			result += "d";
		}
		
		if(_horisontalLookingDirection == HorisontalLookingDirection.Left) {
			result += "l";
		} else if(_horisontalLookingDirection == HorisontalLookingDirection.Right) {
			result += "r";
		}
		
		return result;
	}
	
	private void LookTowards(Vector3 position) {
		Vector3 myPosition = gameObject.transform.position;
		Vector2 difference = new Vector2(Mathf.Abs(myPosition.x-position.x), Mathf.Abs (myPosition.z-position.z));
		
		if (difference.y > difference.x*4) {
			_horisontalLookingDirection = HorisontalLookingDirection.Middle;	
		} else if(position.x > myPosition.x) {
			_horisontalLookingDirection = HorisontalLookingDirection.Right;
		} else if(position.x < myPosition.x) {
			_horisontalLookingDirection = HorisontalLookingDirection.Left;
		}
		
		if(position.z > myPosition.z) {
			_verticalLookingDirection = VerticalLookingDirection.Up;
		} else if(position.z < myPosition.z) {
			_verticalLookingDirection = VerticalLookingDirection.Down;
		} else {
			_verticalLookingDirection = VerticalLookingDirection.Middle;
		}
	}
	#endregion
	
	#region interaction
	private void CheckInput() {
		if(_canAttack) {
			if(Input.GetButtonDown("Fire1")) {	
				GameObject target = FindClickTarget();
				
				if(target != null) {
					if(target.tag == "Enemy" && _attackTimer <= 0) {
						Debug.Log("Hit enemy: " + target.name);
						Attack(target);
						_attackTimer = Weapon._cooldown;
					}
					else if(target.tag == "Friendly") {
						BaseNPCChat ch = target.GetComponent("BaseNPCChat") as BaseNPCChat;
						if(Vector3.Distance(target.transform.position, gameObject.transform.position) <= ch.TalkDistance) {
							Debug.Log("asd");
							Time.timeScale = 0;
							_canAttack = false;
							DialogueDisplay disp = gameObject.GetComponent("DialogueDisplay") as DialogueDisplay;
							disp.OpenDisplay(ch);
						}
					}
				}
				//TODO - change the direction of the sprite so that the character looks towards clicked point
				_actionTaken = ActionTaken.MeleeAttack;
			}
	
			else if(Input.GetButtonDown("Fire2")) {	
				Explosive e = _inventory.GetEquippedExplosive();
				if(e != null) {
					_actionTaken = ActionTaken.RangedAttack;
					Debug.Log("Throwing bomb: " + e._name);
					GameObject target = FindClickTarget();
					GameObject explosion = (GameObject)Instantiate(Resources.Load("Prefabs/MyExplosion"), _clickPoint, Quaternion.identity);
					Explosion expScript = explosion.GetComponent("Explosion") as Explosion;
					expScript._damage = 10;
					e._amount--;
				}
			}
		}
				
		if(Input.GetKeyUp(KeyCode.C)) {
			ChangeWeapon();
		}		
		if(Input.GetKeyUp(KeyCode.I)) {
			DisplayManager disp = gameObject.GetComponent("DisplayManager") as DisplayManager;			
			disp.ChangeDisplayState("inventory");
			_canAttack = !_canAttack;
		}
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
			_clickPoint = new Vector3(cameraRay.GetPoint(dist).x, playerPos.y, cameraRay.GetPoint(dist).z);
         	//Debug.Log("clickpoint " + _clickPoint);
			LookTowards(_clickPoint);
		}
		
		//if clicked on another object
		RaycastHit hit;
		if(Physics.Raycast(cameraRay, out hit, dist)) {
			//Debug.Log(hit.distance);
			return hit.collider.gameObject;
		}
		
		return null;
	}
	
	private void Attack(GameObject target) {
		float distance = Vector3.Distance(target.transform.position, transform.position);
		
		if(distance <= _curWeapon._range){	
			Debug.Log("Distance " + distance + ". Damage to " +target.tag + ": " + _curWeapon._damage);
			BaseCharacter bc = (BaseCharacter)target.GetComponent("BaseCharacter");
			bc.AdjustCurrentHealth(-(_curWeapon._damage));
		}		
	}
	
	private void ThrowBomb() {
		
	}
	#endregion
}
