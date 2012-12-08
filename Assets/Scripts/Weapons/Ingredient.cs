using UnityEngine;
using System.Collections;

public class Ingredient : Item {
	
	public int _amount;
	
	public Ingredient(string name) {
		_name = name;
		_type = "ingredient";		
	}
}
