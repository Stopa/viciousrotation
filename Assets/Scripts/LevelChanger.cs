using UnityEngine;
using System.Collections;

public class LevelChanger: MonoBehaviour {
	
	private DisplayManager _dispManager;
	private GUISkin _customSkin;
	private string _char;
	private string _quote;
	private string _level;
	private Texture2D _portrait;
	public Light _light;
	
	private bool _showConfimation;
	public bool _canExit;
	
	void Awake() {
		if (!GameObject.Find("PlayerCharacterSpriteManager")) {
			Instantiate(Resources.Load("Prefabs/SpriteManagers/PlayerCharacterSpriteManager"), transform.position, Quaternion.identity);
		}
		if (!GameObject.FindGameObjectWithTag("Player")) {
			Instantiate(Resources.Load("Prefabs/PlayerCharacterObject"), transform.position, Quaternion.identity);
		}
		if(!GameObject.FindGameObjectWithTag("GameCamera")) {
			Instantiate(Resources.Load("Prefabs/GameCamera"), transform.position, Quaternion.identity);
		}		
	}
	
	void Start() {
		_showConfimation = false;
		_light.enabled = false;
		transform.collider.isTrigger = false;
		
		_customSkin = Resources.Load("Gui/DialogueSkin") as GUISkin;
		
		GameObject player = GameObject.FindWithTag("Player");
		_dispManager = player.GetComponent("DisplayManager") as DisplayManager;
		
		Vector3 spawnPoint = GameObject.FindWithTag("PlayerSpawn").transform.position;
		float playerHeight = player.transform.lossyScale.y/player.transform.lossyScale.x;
		spawnPoint.y += playerHeight;
		player.transform.position = spawnPoint;
		
		if(Application.loadedLevelName == "doctor") {			
			PlayerCharacter pc = player.GetComponent("PlayerCharacter") as PlayerCharacter;
			if(pc.GetFlag("gotPiss")) {
				spawnPoint = GameObject.Find("SmokeBeardSpawn").transform.position;
				spawnPoint.y += playerHeight;
				Instantiate(Resources.Load("Prefabs/NPCs/Enemies/SmokeBeard/SmokeBeard"), spawnPoint, Quaternion.identity);
			}
			else {
				spawnPoint = GameObject.Find("DoctorSpawn").transform.position;
				spawnPoint.y += playerHeight;
				Instantiate(Resources.Load("Prefabs/NPCs/Friendly/Doctor"), spawnPoint, Quaternion.identity);
				
			}
		}
	}
	
	void Update() {
		CheckExitConditions();
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
			GUI.Label(new Rect(50, Screen.width/3, Screen.width/2, 200), _quote+ "\n\t -" +_char);
			
			if (GUI.Button(new Rect(50, Screen.height-100, 150, 50),"Exit " + Application.loadedLevelName)) 
				ChangeLevel(Application.loadedLevelName);
			if (GUI.Button(new Rect(250, Screen.height-100, 150, 50),"Return")) 
				CloseConfirmation();
			GUI.skin = null;
		}
	}
	
    void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player") {
			SetQuote(Application.loadedLevelName);
			_showConfimation = true;
			Time.timeScale = 0;
			_dispManager.ChangeDisplayState("hud", false);
			_dispManager.ChangeDisplayState("inventory", false);
		}
    }
	
	private void ChangeLevel(string curLevel) {
		Time.timeScale = 1;
		if(curLevel == "graveyard") {
			Application.LoadLevel("doctor");
		}
		else if(curLevel == "doctor") {
			Application.LoadLevel("haldjamets");
		}
		else if(curLevel == "haldjamets") {
			Application.LoadLevel("doctor");
		}
	}
	
	private void CloseConfirmation() {
		_showConfimation = false;
		Time.timeScale = 1;
		_dispManager.ChangeDisplayState("hud", true);
	}
	
	private void SetQuote(string curLevel) {
		
		string[] quotesDigger = new string[2]{"A dead corpse is the best kind of corpse.","I collect shovels."};
		string[] quotesFairy = new string[2]{"My best friend is Bear Grylls.","I get all my vitamins from piss."};
		string[] quotesDoctor = new string[2]{"#YOLO","Who need medicine when you have swag?"};
		
		int index = Random.Range(0, 2);
		switch (curLevel) {
			case "graveyard":
			_char = "The Gravedigger";
			_quote = quotesDigger[index];
			_portrait = Resources.Load("Portraits/gravedigger_plain")as Texture2D;
			break;
			case "doctor":
			_char = "The Doctor";
			_quote = quotesDoctor[index];
			_portrait = Resources.Load("Portraits/doctor_plain")as Texture2D;
			break;
			case "haldjamets":
			_char = "Piss Fairy";
			_quote = quotesFairy[index];
			_portrait = Resources.Load("Portraits/fairy_plain")as Texture2D;
			break;
		}
	}
	
	void CheckExitConditions() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[] actSpawners = GameObject.FindGameObjectsWithTag("EnemySpawner");
		GameObject[] disSpawners = GameObject.FindGameObjectsWithTag("DisabledEnemySpawner");
		if(enemies.Length == 0 && actSpawners.Length == 0 && disSpawners.Length == 0) {
			_canExit = true;
		}
	}
}
