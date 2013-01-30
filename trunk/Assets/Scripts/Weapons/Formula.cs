using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Formula : Item {
	
	public Dictionary<int, int> _ingredients;
	public int _productId;
	
	public Formula(int id, string name, int productId) {
		_id = id;
		_name = name;	
		_ingredients = new Dictionary<int, int>();
		_productId = productId;
	}
}
