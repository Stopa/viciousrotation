using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryDisplay : MonoBehaviour {
	//INVENTORY
	private Rect _inventoryWindow = new Rect(100,200,400,200);
	public Texture _ingredientsImage;
	private bool _display;
	private bool _canCraftItem;
	
	public PlayerCharacter _player;
	private ArrayList _tempIngredients;
	private Formula  _selectedFormula;
	
	// Use this for initialization
	void Start () {
		_display = false;
		_canCraftItem = false;
	}
	
	// Update is called once per frame
	void Update () {		
		if(Input.GetKeyUp(KeyCode.I)) {
			_display = !_display;
		}
	}
	#region display
	void OnGUI() {	
		_player = gameObject.GetComponent("PlayerCharacter") as PlayerCharacter;
		Texture2D weapIcon = _player.Weapon._icon;
		
		GUI.Box(new Rect(0,0,128,128),"");
		
		if(weapIcon != null)
			GUI.DrawTexture(new Rect(0,128,64,64), weapIcon);
			GUI.Box(new Rect(0,128+ 64 * 1,64,64),"1");
		
		if(_display) {
			_inventoryWindow = GUI.Window(1, _inventoryWindow, InventoryWindow, "Inventory");
		}
    }

    public void InventoryWindow(int id){
		DisplayItems();
		
		//craft item button
		GUI.enabled = _canCraftItem;
	    if (GUI.Button(new Rect(10, 180, 50, 30), "Craft!"))
            CraftItem();
		GUI.enabled = true;
	}
	
	private void DisplayItems() {
		ArrayList bombs = _player._bombs;
		ArrayList formulas = _player._formulas;
		ArrayList ings = _player._ingredients;

		int y = 10;
		int x = 0;
		foreach(Formula f in formulas) {
			if (GUI.Button(new Rect(x, y, 64, 64), f._icon)) {
				_selectedFormula = f;
				CheckIngredients(f._ingredients);
			}
			x+= 70;
		}
		
		y = 60;
		x = 0;
		foreach(Ingredient i in ings) {
			GUI.DrawTexture(new Rect(x ,y, 64, 64), i._icon);
			GUI.Label(new Rect(x+5, y+5, 30, 30), i._amount.ToString());
			x+= 70;
		}
		
		y = 120;
		x = 0;
		foreach(Explosive b in bombs) {
			GUI.DrawTexture(new Rect(x, y, 64, 64), b._icon);
			x+= 70;
		} 
	}
	#endregion
	
	#region itemcreation
	/*
	 * 	Create the explosive
	 */
	private void CraftItem() {
		Debug.Log("Crafting from formula: " + _selectedFormula._name);
		/*_tempIngredients = _player._ingredients;

		Dictionary<Ingredient, int> d = _selectedFormula._ingredients;
		foreach (var pair in d)
		{
			if(!CheckIngredient(pair.Key, pair.Value)) {
				_tempIngredients = null;
				return false;
			}
		}	
		_player._ingredients = _tempIngredients;
		_tempIngredients = null;
		return true;
		*/		
	}
	
	/*
	 * 	Check if the player has the required amount of ingredients needed
	 */
	private void CheckIngredients(Dictionary<Ingredient, int> d) {
		_canCraftItem = true;
		foreach (var pair in d) {
			foreach(Ingredient i in _player._ingredients) {
				if(i._name == pair.Key._name) {
					if(!(i._amount >= pair.Value))
						_canCraftItem = false;	
					Debug.Log(pair.Value + " " + i._amount);
				}
			} 
		}		
	}	
	#endregion
	
}
