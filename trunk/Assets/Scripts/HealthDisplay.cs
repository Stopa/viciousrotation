using UnityEngine;
using System.Collections;

public class HealthDisplay : MonoBehaviour {
	private int _maxHealth;
	private int _curHealth;
	private BaseCharacter _character;
	
	public float _healthBarLength;
	public int _healthBarYLoc; 
	
	// Use this for initialization
	void Start () {
		_healthBarLength = Screen.width / 2;

		_character = (BaseCharacter)gameObject.GetComponent("BaseCharacter");			

		_maxHealth = _character.MaxHealth;
		_curHealth = _character.Health;
	}
	
	// Update is called once per frame
	void Update () {
		_curHealth = _character.Health;
		_healthBarLength = (Screen.width / 2) * (_curHealth / (float)_maxHealth);
	}
	
	void OnGUI() {
		GUI.Box(new Rect(10, _healthBarYLoc, _healthBarLength, 20), _curHealth + "/" + _maxHealth);	
	}	
}
