using UnityEngine;
using System.Collections;

public class SmokeBeardChat : BaseNPCChat {

	void Awake() {
		Name = "Smokebeard";
		_portrait = Resources.Load ("Portraits/smokebeard") as Texture2D;
		TalkDistance = 40f;
		_chatFile = "bossmonster";
	}
	
	public override void GoodEnd() {
		return;
	}
	
	public override void BadEnd() {
		Debug.Log("bad end");
		((SmokeBeardBehaviour)gameObject.GetComponent ("SmokeBeardBehaviour")).DoTransform();
	}
}
