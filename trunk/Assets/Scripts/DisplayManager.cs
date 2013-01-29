using UnityEngine;
using System.Collections;

public class DisplayManager : MonoBehaviour {
	
	private bool _showHud;
	private bool _showInventory;
	private bool _showDialogue;
	
	//HUD stuff
	private int _maxHealth;
	private int _curHealth;
	private PlayerCharacter _player;
	private float _healthBarLength;
	private int _healthBarYLoc;
	private Rect _healthBarPos;
	private Texture2D _healthBar;
	
	//CLOCK
	private Texture2D _clockBackground;
	private Texture2D _clockHand;
	public float _cycleTime;
    public float _clockAngle;
    private Rect _clockPos;
    private Vector2 _clockPivot;
	
	//INVENTORY
	private Inventory _inventory;
	private Rect _inventoryWindow = new Rect(100,200,400,250);
	private bool _canCraftItem;
	private Formula  _selectedFormula;
	private Explosive _selectedExplosive;
	
	// Use this for initialization
	void Start () {
		_showDialogue = false;
		_showHud = true;
		_showInventory = false;
		
		_player = gameObject.GetComponent("PlayerCharacter") as PlayerCharacter;
		
		_healthBarLength = Screen.width / 2;		
		_maxHealth = _player.MaxHealth;
		_curHealth = _player.Health;
		_cycleTime = 180;
		UpdateClockSettings();
		
		_inventory = _player._inventory;
		_canCraftItem = false;
		
		_clockHand = Resources.Load("GUI/clockhand") as Texture2D;
		_clockBackground = Resources.Load("GUI/clock") as Texture2D;
		_healthBar = Resources.Load("GUI/healthbar") as Texture2D;
	}
	
	// Update is called once per frame
	void Update () {
		_curHealth = _player.Health;
		_healthBarLength = (Screen.width / 2) * (_curHealth / (float)_maxHealth);
		
		_clockAngle += Time.deltaTime * 360 / _cycleTime;
		_clockAngle = _clockAngle - Mathf.Floor(_clockAngle/360)*360;
	}
	
	void OnGUI () {
		if(_showHud) {
			ShowHud();		
		}
		if(_showInventory) {
			_inventoryWindow = GUI.Window(1, _inventoryWindow, ShowInventory, "Inventory");			
		}
		if(_showDialogue) {
			
		}
	}
	
	public void ChangeDisplayState(string showObject) {
		if(showObject == "inventory")
			_showInventory = !_showInventory;
		else if(showObject == "hud")
			_showHud = !_showHud;
		else if(showObject == "dialogue")
			_showDialogue = !_showDialogue;
	}
	
	#region HUD	
	private void UpdateClockSettings() {
        _clockPos = new Rect(0, 0, 128, 128);
        _clockPivot = new Vector2(_clockPos.xMin + _clockPos.width * 0.5f, _clockPos.yMin + _clockPos.height * 0.5f);
		_clockAngle = 0;
    }
	
	private void ShowHud() {	
		//HEALTH BAR
		GUI.DrawTexture(new Rect(150, _healthBarYLoc, _healthBarLength, 20), _healthBar);
		GUI.Label(new Rect(150, _healthBarYLoc, _healthBarLength, 20), _curHealth + "/" + _maxHealth);
		
		//WEAPONS
		if(_player.Weapon != null)
			GUI.DrawTexture(new Rect(0,128,64,64), _player.Weapon._icon);
		if(_inventory.GetEquippedExplosive() != null)
			GUI.DrawTexture(new Rect(0,128+64,64,64), _inventory.GetEquippedExplosive()._icon);
		
		//CLOCK
		//if (Application.isEditor) { UpdateSettings(); } --- DNO WHAT THIS IS LOL
		GUI.DrawTexture(_clockPos, _clockBackground);
		Matrix4x4 matrixBackup = GUI.matrix;
        GUIUtility.RotateAroundPivot(_clockAngle, _clockPivot);
        GUI.DrawTexture(_clockPos, _clockHand);
        GUI.matrix = matrixBackup;
	}
	
	#endregion
	
	#region inventory
	private void ShowInventory(int id) {
		ArrayList bombs = _inventory.Bombs;
		ArrayList formulas = _inventory.Formulas;
		ArrayList ings = _inventory.Ingredients;

		int y = 10;
		int x = 0;
		foreach(Formula f in formulas) {
			if (GUI.Button(new Rect(x, y, 64, 64), f._icon))
				FormulaBtnPressed(f);
			x+= 70;
		}
		
		y += 60;
		x = 0;
		foreach(Ingredient i in ings) {
			GUI.DrawTexture(new Rect(x ,y, 64, 64), i._icon);
			GUI.Label(new Rect(x+5, y+10, 30, 30), i._amount.ToString());
			x+= 70;
		}
		
		y += 60;
		x = 0;
		foreach(Explosive e in bombs) {
			if (GUI.Button(new Rect(x, y, 64, 64), e._icon)) {
				_selectedExplosive = e;
			}
			GUI.Label(new Rect(x+5, y+10, 30, 30), e._amount.ToString());
			x+= 70;
		} 
		
		//BUTTONS
		GUI.enabled = _canCraftItem;
	    if (GUI.Button(new Rect(10, 200, 50, 30), "Craft!"))
             CraftBtnPressed();
		GUI.enabled = true;
		
		if (GUI.Button(new Rect(100, 200, 50, 30), "Equip!"))
             EquipBtnPressed();
		
		GUI.enabled = true;
	}
	
	private void CraftBtnPressed() {
		_inventory.CraftItem(_selectedFormula);
		_canCraftItem = _inventory.CheckIngredients(_selectedFormula);
	}
	
	private void EquipBtnPressed() {
		if(_selectedExplosive != null)
			_inventory.EquipItem(_selectedExplosive);
	}
	
	private void FormulaBtnPressed(Formula f) {
		_selectedFormula = f;
		_canCraftItem = _inventory.CheckIngredients(_selectedFormula);
	}
	#endregion
	
	private void ShowDialogue() {
		
	}
}
