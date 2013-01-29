using UnityEngine;
using System.Collections;

public class HealthDisplay : MonoBehaviour {
	
	private int _maxHealth;
	private int _curHealth;
	private BaseCharacter _character;
	//HEALTH BAR
	public float _healthBarLength;
	public int _healthBarYLoc; 
	
	public Texture2D _healthBar;
	
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
		if(gameObject.CompareTag("Player")){
			GUI.DrawTexture(new Rect(150, _healthBarYLoc, _healthBarLength, 20), _healthBar);
			GUI.Label(new Rect(150, _healthBarYLoc, _healthBarLength, 20), _curHealth + "/" + _maxHealth);
		}
		else
			GUI.Box(new Rect(150, _healthBarYLoc, _healthBarLength, 20), _curHealth + "/" + _maxHealth);
    }


}
