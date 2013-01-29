using UnityEngine;
using System.Collections;
using MiniJSON;

public class DialogueDisplay : MonoBehaviour {
	public bool _showDialogue;
	private PlayerCharacter _player;
	private BaseNPCChat _partner;
	int _selGridInt;
		
	private GUISkin _customSkin;
	
	private IList _chatObjects;
	private int _currentChatObject;
	
	// Use this for initialization
	void Start () {
		_showDialogue = false;
		_player = gameObject.GetComponent("PlayerCharacter") as PlayerCharacter;
		_customSkin = Resources.Load("Gui/DialogueSkin") as GUISkin;
	}
	
	void Update() {
		/*if(Input.GetKeyUp(KeyCode.B)) {
			_showDialogue = !_showDialogue;
		}*/
	}
	
	// Update is called once per frame
	void OnGUI () {
		
		if(_showDialogue) {
			IList optionsList = (IList)((IDictionary)_chatObjects[_currentChatObject])["options"];
			string[] selStrings = new string[optionsList.Count];
			ArrayList options = new ArrayList();
			for(var i = 0;i<optionsList.Count;i++) {
				selStrings[i] = ((IDictionary)optionsList[i])["text"] as string;
			}
			GUI.skin = _customSkin;
			
		    GUI.BeginGroup(new Rect(0, Screen.height - 200, Screen.width, 200));		
		    // Draw a box in the new coordinate space defined by the BeginGroup.
			GUI.Box(new Rect(0,0,180,200), "");
			GUI.DrawTexture(new Rect(0,0,180,200), _player._portrait);
			GUI.Box(new Rect(Screen.width - 180,0,180,200), _partner.Name);
			GUI.DrawTexture(new Rect(Screen.width - 180,0,180,200), _partner._portrait);
			
			GUI.Box(new Rect(180,0,Screen.width - 180*2,100), (string)((IDictionary)_chatObjects[_currentChatObject])["text"]);
			_selGridInt = GUI.SelectionGrid(new Rect(180, 100, Screen.width - 180*2, 100), _selGridInt, selStrings, 1);	
			GUI.EndGroup();			
			CheckSelection();
			
			GUI.skin = null;			
		}
	}
	
	private void CheckSelection() {
		if(_selGridInt != -1) {
			IDictionary currentChat = _chatObjects[_currentChatObject] as IDictionary;
			IList currentOptions = currentChat["options"] as IList;
			IDictionary currentOption = currentOptions[_selGridInt] as IDictionary;
			int gotoint = int.Parse(currentOption["goto"].ToString());
			switch(gotoint) {
				case -1:
					_partner.GoodEnd();
					CloseDisplay();
					break;
				case -2:
					_partner.BadEnd();
					CloseDisplay();
					break;
				default:
					_currentChatObject = gotoint;
					break;
			}
		}
		
		_selGridInt = -1;
	}
	
	private void GetNextDialogue() {
		
		
	}
	
	public void OpenDisplay(BaseNPCChat partner) {
		Debug.Log("opening dialogue");
		_partner = partner;	
		_showDialogue = true;
		_currentChatObject = 0;
		_selGridInt = -1;
		TextAsset jsonfile = Resources.Load("Chat/"+_partner._chatFile) as TextAsset;
		IDictionary json = (IDictionary) MiniJSON.Json.Deserialize(jsonfile.text);
		_chatObjects = (IList) json["chat"];		
		Time.timeScale = 0;
	}
	
	public void CloseDisplay() {
		_partner = null;	
		_showDialogue = false;
		_player.CanAttack = true;
		Time.timeScale = 1;
	}	
	
}
