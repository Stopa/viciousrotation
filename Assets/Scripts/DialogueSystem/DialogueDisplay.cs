using UnityEngine;
using System.Collections;

public class DialogueDisplay : MonoBehaviour {
	public bool _showDialogue;
	public string[] selStrings;
	private PlayerCharacter _player;
	private FriendlyCharacter _partner;
		
	private GUISkin _customSkin;
	
	// Use this for initialization
	void Start () {
		_showDialogue = false;
		_player = gameObject.GetComponent("PlayerCharacter") as PlayerCharacter;
		_customSkin = Resources.Load("Gui/DialogueSkin") as GUISkin;
	}
	
	void Update() {
		if(Input.GetKeyUp(KeyCode.B)) {
			_showDialogue = !_showDialogue;
		}
	}
	
	// Update is called once per frame
	void OnGUI () {
		int selGridInt = -1;
		selStrings = new string[] {"Ambiguous choice 1", "Ambiguous choice 2", "Ambiguous choice 3", "Ambiguous choice 4"};
		if(_showDialogue) {
			GUI.skin = _customSkin;
			
		    GUI.BeginGroup(new Rect(0, Screen.height - 200, Screen.width, 200));		
		    // Draw a box in the new coordinate space defined by the BeginGroup.
			GUI.Box(new Rect(0,0,180,200), "PORTRAIT YOU");
			GUI.DrawTexture(new Rect(0,0,180,200), _player._portrait);
			GUI.Box(new Rect(Screen.width - 180,0,180,200), "PORTRAIT OTHER GUY");
			GUI.DrawTexture(new Rect(Screen.width - 180,0,180,200), _partner._portrait);
			
			GUI.Box(new Rect(180,0,Screen.width - 180*2,100), "HE SAYS THIS TEXT");
			selGridInt = GUI.SelectionGrid(new Rect(180, 100, Screen.width - 180*2, 100), selGridInt, selStrings, 1);	
			GUI.EndGroup();			
			CheckSelection(selGridInt);
			
			GUI.skin = null;			
		}
	}
	
	private void CheckSelection(int selGridInt) {
		if(selGridInt != -1)
			Debug.Log(selStrings[selGridInt]);
	}
	
	private void GetNextDialogue() {
		
		
	}
	
	public void OpenDisplay(FriendlyCharacter partner) {
		_partner = partner;	
		_showDialogue = true;
	}
	
	public void CloseDisplay() {
		_partner = null;	
		_showDialogue = false;
	}	
	
}
