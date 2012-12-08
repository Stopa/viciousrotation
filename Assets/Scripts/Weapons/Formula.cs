using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Formula : Item {
	
	public Dictionary<Ingredient, int> _ingredients;
	
	public Formula(string name) {
		_name = name;
		_type = "formula";		
		_ingredients = new Dictionary<Ingredient, int>();
	}
}
