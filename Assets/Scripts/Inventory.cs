using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory {
	
	private ArrayList _ingredients;
	private ArrayList _formulas;
	private ArrayList _bombs;
	
	private int _equippedIndex = -1;
	
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
		ArrayList tempIngredients = _ingredients;
		Dictionary<int, int> d = formula._ingredients;
		
		foreach (var pair in d){
			foreach(Ingredient i in _ingredients) {
				if(i._id == pair.Key) {
					i._amount -= pair.Value;
					break;
				}
			} 
		}	
		_ingredients = tempIngredients;	
		AddItem(CreateItem(formula._productId));
	}
	
	public bool CheckIngredients(Formula formula) {
		Dictionary<int, int> d = formula._ingredients;
		foreach (var pair in d) {
			foreach(Ingredient i in _ingredients) {
				if(i._id == pair.Key) {
					if(!(i._amount >= pair.Value))
						return false;
				}
			} 
		}	
		return true;
	}
		
	private Item CreateItem(int id) {
		Item i = null;
		if(id == 1) {
			i = new Explosive(1,"bomb_1", 15.0f, 4.0f, 10, 3.0f);
			i._icon = Resources.Load("Item/Icon/bomb_1") as Texture2D;
		}
		else if(id == 2) {
			i = new Explosive(2,"bomb_2", 15.0f, 5.0f, 15, 3.0f);
			i._icon = Resources.Load("Item/Icon/bomb_2") as Texture2D;
		}
		else if(id == 3) {
			i = new Potion(3, "potion_1", 20);
			i._icon = Resources.Load("Item/Icon/potion_1") as Texture2D;
		}
		i._amount = 1;
		Debug.Log(i.GetType().ToString());
		return i;
	}
	#endregion
	
	#region addremove
	public void AddItem(Item item) {
		Item existingItem = null;
		if(item.GetType() == typeof(Formula)) 
			_formulas.Add(item as Formula);
		
		else if(item.GetType() == typeof(Ingredient)) {
			existingItem = CheckIfContains(_ingredients, item);
			if(existingItem != null)
				existingItem._amount += item._amount;
			else
				_ingredients.Add(item as Ingredient);		
		}
		
		else if (item.GetType() == typeof(Explosive)) {
			existingItem = CheckIfContains(_bombs, item);
			if(existingItem != null)
				existingItem._amount += item._amount;
			else
				_bombs.Add(item as Explosive);
		}
		else if (item.GetType() == typeof(Potion)) {
			existingItem = CheckIfContains(_bombs, item);
			if(existingItem != null)
				existingItem._amount += item._amount;
			else
				_bombs.Add(item as Potion);
		}
	}
	
	private Item CheckIfContains(ArrayList list, Item item) {
		foreach(Item i in list) {
			if(i._id == item._id)
				return i;
		}
		return null;
	}
	
	private void RemoveItem(Item item) {
		if (item.GetType() == typeof(Explosive)) {
			_bombs.Remove(item);	
		}
		else if (item.GetType() == typeof(Ingredient)) {
			_ingredients.Remove(item);
		}
	}
	#endregion
	
	public void EquipItem(Item item) {		
			_equippedIndex = _bombs.IndexOf(item);
	}

	public Item GetEquippedItem() {
		if(_equippedIndex >= 0) {
			Item i = _bombs[_equippedIndex] as Item;
			if(i._amount > 0)
				return i;
		}
		return null;
	}
	
	public void UseEquippedItem() {
		Item i = _bombs[_equippedIndex] as Item;
		i._amount--;
		if(i._amount == 0)
			_equippedIndex = -1;
	}
	
}
