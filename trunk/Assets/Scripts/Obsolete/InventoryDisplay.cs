using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryDisplay : MonoBehaviour {
	
	public bool _showHud;
	public bool _showInventory;
	public bool _showDialogue;	
	public PlayerCharacter _player;
	
	//INVENTORY
	private Inventory _inventory;
	private Rect _inventoryWindow = new Rect(100,200,400,250);
	private bool _canCraftItem;
	private Formula  _selectedFormula;
	private Explosive _selectedExplosive;
	
	//clock 
	public Texture2D _clockBackground;
	public Texture2D _clockHand;
	public float _cycleTime;
    public float _clockAngle = 0;
    Rect _clockRect;
    Vector2 _clockPivot;

	// Use this for initialization
	void Start () {
		_player = gameObject.GetComponent("PlayerCharacter") as PlayerCharacter;
		_inventory = _player._inventory;
		_showInventory = false;
		_canCraftItem = false;
		_showHud = true;
		_cycleTime = 180;
		UpdateClockSettings();
		//images
		//_clockHand = Resources.Load("");
		//_clockBackground = Resources.Load("");
	}
	
	// Update is called once per frame
	void Update () {		
		if(Input.GetKeyUp(KeyCode.I)) {
			_showInventory = !_showInventory;
		}
		_clockAngle += Time.deltaTime * 360 / _cycleTime;
		_clockAngle = _clockAngle - Mathf.Floor(_clockAngle/360)*360;
		
		
	}
	
	void UpdateClockSettings() {
		Vector2 size = new Vector2(128, 128);
    	Vector2 pos = new Vector2(0, 0);
        _clockRect = new Rect(pos.x, pos.y, size.x, size.y);
        _clockPivot = new Vector2(_clockRect.xMin + _clockRect.width * 0.5f, _clockRect.yMin + _clockRect.height * 0.5f);
    }
	
	#region display
	void OnGUI() {	
		if(_showHud) {
			//CLOCK
			//if (Application.isEditor) { UpdateSettings(); } --- DNO WHAT THIS IS LOL
			DisplayClock();
			
			//OTHER
			if(_player.Weapon != null)
				GUI.DrawTexture(new Rect(0,128,64,64), _player.Weapon._icon);
			if(_inventory.GetEquippedExplosive() != null)
				GUI.DrawTexture(new Rect(0,128+64,64,64), _inventory.GetEquippedExplosive()._icon);
			
			if(_showInventory) {
				_inventoryWindow = GUI.Window(1, _inventoryWindow, InventoryWindow, "Inventory");
			}
		}
    }
	
	private void DisplayClock() {
		GUI.DrawTexture(_clockRect, _clockBackground);
		Matrix4x4 matrixBackup = GUI.matrix;
        GUIUtility.RotateAroundPivot(_clockAngle, _clockPivot);
        GUI.DrawTexture(_clockRect, _clockHand);
        GUI.matrix = matrixBackup;
	}
	
    private void InventoryWindow(int id) {
		DisplayItems();		
		
		GUI.enabled = _canCraftItem;
	    if (GUI.Button(new Rect(10, 200, 50, 30), "Craft!"))
             CraftBtnPressed();
		GUI.enabled = true;
		
		if (GUI.Button(new Rect(100, 200, 50, 30), "Equip!"))
             EquipBtnPressed();
	}

	private void DisplayItems() {
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
	}
	
	#endregion

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
	
}
