using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Formula : Item {
	
	public Dictionary<Ingredient, int> _ingredients;
	
	public Formula(int id, string name) {
		_id = id;
		_name = name;	
		_ingredients = new Dictionary<Ingredient, int>();
	}
}
