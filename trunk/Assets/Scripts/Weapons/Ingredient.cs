using UnityEngine;
using System.Collections;

public class Ingredient : Item {
	
	public Ingredient(string name) {
		_name = name;
		_type = "ingredient";		
	}
}
