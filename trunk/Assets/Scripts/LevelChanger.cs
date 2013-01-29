using UnityEngine;
using System.Collections;

public class LevelChanger: MonoBehaviour {
	
	private bool _showConfimation;
	private GUISkin _customSkin;
	private string _char;
	private string _quote;
	private string _level;
	private Texture2D _portrait;
	public Light _light;
	public bool _canExit;
	
	void Start() {
		_showConfimation = false;
		_light.enabled = false;
		transform.collider.isTrigger = false;
		
		_customSkin = Resources.Load("Gui/DialogueSkin") as GUISkin;
	}
	
	void Update() {
		if(_canExit) {
			_light.enabled = true;
			transform.collider.isTrigger = true;
		}
	}
	
	void OnGUI() {
		if(_showConfimation){
			GUI.skin = _customSkin;
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Resources.Load("Gui/black")as Texture2D);
			GUI.DrawTexture(new Rect(Screen.width-Screen.width/3, 0, 256, 512), _portrait);
			GUI.Label(new Rect(50, Screen.width/2, Screen.width/2, 200), _quote+ "\n\t -" +_char);
			
			if (GUI.Button(new Rect(50, Screen.height-100, 150, 50),"Exit " + Application.loadedLevelName)) 
				ChangeLevel(Application.loadedLevelName);
			if (GUI.Button(new Rect(250, Screen.height-100, 150, 50),"Return")) 
				CloseConfirmation();
			GUI.skin = null;
		}
	}
	
    void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player") {
			SetQuote("graveyard");
			_showConfimation = true;
		}
    }
	
	private void ChangeLevel(string curLevel) {
		if(curLevel == "graveyard") {
			Application.LoadLevel("haldjamets");
		}
		else if(curLevel == "doctor") {
			//Application.LoadLevel("haldjamets");
		}
		else if(curLevel == "haldjamets") {
			//Application.LoadLevel("doctor");
		}
	}
	
	private void CloseConfirmation() {
		_showConfimation = false;
	}
	
	private void SetQuote(string curLevel) {
		string[] quotesDigger = new string[3]{"A dead corpse is the best kind of corpse.","I collect shovels.","Zombies... hmm."};
		string[] quotesFairy = new string[3]{"My best friend is Bear Grylls.","I get all my vitamins from piss.","Piss."};
		string[] quotesDoctor = new string[3]{"yolo","swag","no homo"};
		
		int index = Random.Range(0, 3);
		switch (curLevel) {
			case "graveyard":
			_char = "The Gravedigger";
			_quote = quotesDigger[index];
			_portrait = Resources.Load("Portraits/gravedigger")as Texture2D;
			break;
			case "doctor":
			_char = "The Doctor";
			_quote = quotesDoctor[index];
			_portrait = Resources.Load("Portraits/doctor")as Texture2D;
			break;
			case "forest":
			_char = "Piss Fairy";
			_quote = quotesFairy[index];
			_portrait = Resources.Load("Portraits/fairy")as Texture2D;
			break;
		}
	}
}
