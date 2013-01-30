using UnityEngine;
using System.Collections;

public class SmokeBeardChat : BaseNPCChat {

	void Awake() {
		Name = "Smokebeard";
		_portrait = Resources.Load ("Portraits/smokebeard") as Texture2D;
		TalkDistance = 40f;
		_chatFile = "bossmonster";
		_canTalk = true;
	}
	
	public override void GoodEnd() {
		return;
	}
	
	public override void BadEnd() {
		Debug.Log("bad end");
		((SmokeBeardBehaviour)gameObject.GetComponent ("SmokeBeardBehaviour")).DoTransform();
		_canTalk = false;
		GameObject exit = GameObject.Find("ExitCollider");
		LevelChanger lc = exit.GetComponent("LevelChanger") as LevelChanger;
		lc._canExit = false;
	}
}
