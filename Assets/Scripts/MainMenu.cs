using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	private string _firstLevel = "Graveyard";
	public bool _startGame = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//check if level is loaded, 1=100%
		//if(Application.GetStreamProgressForLevel(_firstLevel) == 1) {
		if(_startGame) {
			Application.LoadLevel(_firstLevel);
		}
		
	}
}
