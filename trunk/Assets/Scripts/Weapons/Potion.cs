using UnityEngine;
using System.Collections;

public class Potion: Item {

	public int _healAmount;
	
	public Potion(int id, string name, int healAmount) {
		_id = id;
		_name = name;
		_healAmount = healAmount;
	}
}
