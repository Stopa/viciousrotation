using UnityEngine;
using System.Collections;

public class FriendlyCharacter: MonoBehaviour {
	private string _name;
	private GameObject _player;
	private float _talkDistance;
	public Texture2D _portrait;
		
	void Start() {
		_player = GameObject.FindGameObjectWithTag("Player");
		_talkDistance = 7.0f;
		
	}
	
	
	void Update() {
		float distance = Vector3.Distance(_player.transform.position, transform.position);
		if(distance <= _talkDistance) {
			//DISPLAY DIALOGUE
		}
		
	}
	
}
