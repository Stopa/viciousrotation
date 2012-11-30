using UnityEngine;
using System.Collections;

public class HealthDisplay : MonoBehaviour {
	private int _maxHealth;
	private int _curHealth;
	private BaseCharacter _character;
	//HEALTH BAR
	public float _healthBarLength;
	public int _healthBarYLoc; 
	
	//DIALOGUE AREA
	public bool _showDialogue;
	
	// Use this for initialization
	void Start () {
		_healthBarLength = Screen.width / 2;

		_character = (BaseCharacter)gameObject.GetComponent("BaseCharacter");			

		_maxHealth = _character.MaxHealth;
		_curHealth = _character.Health;
		_showDialogue = false;
	}
	
	// Update is called once per frame
	void Update () {
		_curHealth = _character.Health;
		_healthBarLength = (Screen.width / 2) * (_curHealth / (float)_maxHealth);
	}
	
	void OnGUI() {
		GUI.Box(new Rect(10, _healthBarYLoc, _healthBarLength, 20), _curHealth + "/" + _maxHealth);	
		
		int selGridInt = 0;
		string[] selStrings = new string[] {"Ambiguous choice 1", "Ambiguous choice 2", "Ambiguous choice 3", "Ambiguous choice 4"};
		if(_showDialogue) {
		    GUI.BeginGroup(new Rect(0, Screen.height - (Screen.height / 5), Screen.width, Screen.height / 5));
		
		    // Draw a box in the new coordinate space defined by the BeginGroup.
		    GUI.Box(new Rect(0,0,200,Screen.height / 5), "PORTRAIT YOU");
			GUI.Box(new Rect(Screen.width - 200,0,200,Screen.height / 5), "PORTRAIT OTHER GUY");
			GUI.Box(new Rect(200,0,Screen.width - 400,100), "HE SAYS THIS");
			selGridInt = GUI.SelectionGrid(new Rect(200, 50, Screen.width - 400, 100), selGridInt, selStrings, 2);	
			GUI.EndGroup();
			
			Debug.Log(selStrings[selGridInt]);
		}
	}	
}
