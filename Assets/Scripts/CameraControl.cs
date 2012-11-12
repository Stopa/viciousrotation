using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	
	public GameObject _player;
	
	void Start(){
		_player = GameObject.Find("PlayerCharacterObject");
	}
	void LateUpdate () {
		Vector3 pos = _player.transform.position;
		pos.y = 0;
		//TO DO 
		//constraints so camera wouldn't move over level edge
		//...
		
		transform.position = _player.transform.position;
	}
}
