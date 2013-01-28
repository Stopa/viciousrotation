using UnityEngine;
using System.Collections;

public class PissFairyChat : BaseNPCChat {

	void Awake() {
		Name = "Piss Fairy";
		_portrait = Resources.Load ("Portraits/Fairy") as Texture2D;
		TalkDistance = 7f;
		_chatFile = "chat";
	}
	
	public override void GoodEnd() {
		Debug.Log ("Good end!");
	}
	
	public override void BadEnd() {
		Debug.Log ("Bad end!");
	}
}
