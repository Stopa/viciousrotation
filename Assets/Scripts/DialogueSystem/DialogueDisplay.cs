using UnityEngine;
using System.Collections;

public class DialogueDisplay : MonoBehaviour {
	public bool _showDialogue;
	public string[] selStrings;
	// Use this for initialization
	void Start () {
		_showDialogue = false;
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
		    GUI.BeginGroup(new Rect(0, Screen.height - (Screen.height / 5), Screen.width, Screen.height / 5));
		
		    // Draw a box in the new coordinate space defined by the BeginGroup.
			GUI.Box(new Rect(Screen.width - 200,0,200,Screen.height / 5), "PORTRAIT OTHER GUY");
			GUI.Box(new Rect(200,0,Screen.width - 400,100), "HE SAYS THIS");
			selGridInt = GUI.SelectionGrid(new Rect(200, 50, Screen.width - 400, 100), selGridInt, selStrings, 2);	
			GUI.EndGroup();
			CheckSelection(selGridInt);
			
		}
	}
	
	private void CheckSelection(int selGridInt) {
		if(selGridInt != -1)
			Debug.Log(selStrings[selGridInt]);
	}
	
	private void GetNextDialogue() {
		
		
	}
	
	
}
