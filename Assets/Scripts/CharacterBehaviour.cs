using UnityEngine;
using System.Collections;

public class CharacterBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject refGameObject = GameObject.Find("PlayerCharacterSpriteManager");
		SpriteManager mySpriteManager = (SpriteManager)refGameObject.GetComponent("LinkedSpriteManager");
		
		GameObject playerGameObject = GameObject.Find ("PlayerCharacterObject");
		mySpriteManager.AddSprite(playerGameObject,1f, 1f, 0,0,64,128,true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
