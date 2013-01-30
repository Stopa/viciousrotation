using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	private GUISkin _customSkin;
	private Texture2D _backGround;
	private bool _showMenu;
	private bool _showCredits;
	
	// Use this for initialization
	void Start() {
		_customSkin = Resources.Load("Gui/DialogueSkin") as GUISkin;
		_backGround = Resources.Load("Story/paperBackground") as Texture2D;
		_showMenu = true;
		_showCredits = false;
	}
	
	// Update is called once per frame
	void Update() {	
	}
	
	void OnGUI() {
		//MAIN MENU
		if(_showMenu){
			GUI.skin = _customSkin;			
			if (GUI.Button(new Rect(Screen.width/4, Screen.height/3, Screen.width/4, 50),"Start")) 
				StartGame();
			if (GUI.Button(new Rect(Screen.width/4, Screen.height/3 + 100, Screen.width/4, 50),"Creators")) {
				_showCredits = true;
				_showMenu = false;
			}
			if (GUI.Button(new Rect(Screen.width/4, Screen.height/3 + 200, Screen.width/4, 50),"Exit")) 
				Application.Quit();
			GUI.skin = null;
		}
		//CREDITS
		else if(_showCredits) {
			GUI.skin = _customSkin;
			if (GUI.Button(new Rect(25, Screen.height-100, 100, 50),"Return")) {
				_showCredits = false;
				_showMenu = true;
			}
			GUI.skin = null;
		}
		
	}
	
	private void StartGame() {
		//check if level is loaded, 1=100%
		//if(Application.GetStreamProgressForLevel(_firstLevel) == 1) {
		_showMenu = false;
		StoryDisplay sd = gameObject.GetComponent("StoryDisplay") as StoryDisplay;
		sd._story = "start";
		sd.LoadImages();
	}
	
	private void ExitGame() {
		
	}
}
