using UnityEngine;
using System.Collections;

public class DroppedItem : MonoBehaviour {
	
	private Ingredient _ingredient;
	
	// Use this for initialization
	void Start () {	
		CreateRandomItem();
	}
	
	// Update is called once per frame
	void Update () {	
		
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player") {
			PlayerCharacter pc = other.gameObject.GetComponent("PlayerCharacter") as PlayerCharacter;
			pc.AddItem(_ingredient);
			Destroy(gameObject);
		}
    }
	
	void CreateRandomItem() {
		_ingredient = new Ingredient("ingredient_1");
		_ingredient._amount = 2;
		_ingredient._icon = Resources.Load("Item/Icon/ingredient_1") as Texture2D;	
	}
}
