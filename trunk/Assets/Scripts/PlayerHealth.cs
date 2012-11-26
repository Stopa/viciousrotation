using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public int _maxHealth = 100;
	public int _curHealth = 100;
	
	public float _healthBarLength;
	public int _healthBarYLoc; 
	
	// Use this for initialization
	void Start () {
		_healthBarLength = Screen.width / 2;
	}
	
	// Update is called once per frame
	void Update () {
		AdjustCurrentHealth(0);
	}
	
	void OnGUI() {
		GUI.Box(new Rect(10, _healthBarYLoc, _healthBarLength, 20), _curHealth + "/" + _maxHealth);	
		
	}
	
	public void AdjustCurrentHealth(int adj) {
		_curHealth += adj;
		
		if(_curHealth < 0)
			_curHealth = 0;
		if(_curHealth > _maxHealth)
			_curHealth = _maxHealth;
		if(_maxHealth < 1)
			_maxHealth = 1;
		
		_healthBarLength = (Screen.width / 2) * (_curHealth / (float)_maxHealth);
	}
}
