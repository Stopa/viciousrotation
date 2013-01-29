using UnityEngine;
using System.Collections;

public class DroppedItem : MonoBehaviour {
	
	private Ingredient _ingredient;
	
	void Awake() {
		int id = Random.Range(1, 6);
		Debug.Log(id);
		if(id > 3) 
			Destroy(gameObject);
		else
			CreateRandomItem(id);
	}
	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {		
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player" && _ingredient != null) {
			PlayerCharacter player = other.gameObject.GetComponent("PlayerCharacter") as PlayerCharacter;
			player.AddItem(_ingredient);
			Destroy(gameObject);
		}
    }
	
	void CreateRandomItem(int id) {
			name = "ingredient_" + id;
			_ingredient = new Ingredient(id, name);
			_ingredient._amount = 1;
			_ingredient._icon = Resources.Load("Item/Icon/" + name) as Texture2D;	
	}
}
