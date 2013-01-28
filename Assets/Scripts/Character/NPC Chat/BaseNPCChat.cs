using UnityEngine;
using System.Collections;

public abstract class BaseNPCChat: MonoBehaviour {
	private string _name;
	private float _talkDistance;
	public Texture2D _portrait;
	public string _chatFile;
	
	public float TalkDistance {
		get {return _talkDistance;}
		set {_talkDistance = value;}
	}
	
	public string Name {
		get {return _name;}
		set {_name = value;}
	}
	
	public abstract void GoodEnd();
	public abstract void BadEnd();
	
	
	/*void Update() {
		float distance = Vector3.Distance(_player.transform.position, transform.position);
		if(distance <= _talkDistance) {
			//DISPLAY DIALOGUE
		}
		
	}*/
	
}
