using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory {
	
	private ArrayList _ingredients;
	private ArrayList _formulas;
	private ArrayList _bombs;
	
	#region get set
	public ArrayList Ingredients {
		get{ return _ingredients;}
	}
	public ArrayList Formulas {
		get{ return _formulas;}
	}
	public ArrayList Bombs {
		get{ return _bombs;}
	}	
	#endregion	
	
	public Inventory() {
		_ingredients = new ArrayList();
		_formulas = new ArrayList();
		_bombs = new ArrayList();
	}
	
	#region item creation
	public void CraftItem(Formula formula) {
		Debug.Log("Crafting from formula: " + formula._name);
		ArrayList tempIngredients = _ingredients;

		Dictionary<Ingredient, int> d = formula._ingredients;
		foreach (var pair in d){
			foreach(Ingredient i in _ingredients) {
				if(i._name == pair.Key._name) {
					i._amount -= pair.Value;
					break;
				}
			} 
		}	
		_ingredients = tempIngredients;	
		_bombs.Add(CreateExplosive(1));
	}
	
	public bool CheckIngredients(Formula formula) {
		Dictionary<Ingredient, int> d = formula._ingredients;
		foreach (var pair in d) {
			foreach(Ingredient i in _ingredients) {
				if(i._name == pair.Key._name) {
					if(!(i._amount >= pair.Value))
						return false;
					Debug.Log(pair.Value + " - " + pair.Key._name + " - " + i._amount);
				}
			} 
		}	
		return true;
	}
		
	private Explosive CreateExplosive(int id) {
		Explosive e = new Explosive("bomb_2", "bomb", 15.0f, 4.0f, 15, 2.0f);
		e._icon = Resources.Load("Item/Icon/bomb_2") as Texture2D;
		return e;
	}
	#endregion
	
	public void AddItem(Item item) {
		if(item.GetType() == typeof(Formula)) 
			_formulas.Add(item as Formula);
		else if(item.GetType() == typeof(Ingredient)) {
			_ingredients.Add(item as Ingredient);
			
		}
		else if (item.GetType() == typeof(Explosive))
			_bombs.Add(item as Explosive);
	}
	
	public void RemoveItem(Item item) {
		
		
	}
	
	public Item CheckIfContains(ArrayList list, Item item) {
		foreach(Item i in list) {
			if(i._name == item._name)
				return i;
		}
		return null;
	}
}
