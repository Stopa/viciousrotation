using UnityEngine;
using System.Collections;

public class GraveDiggerChat : BaseNPCChat {

	void Awake() {
		Name = "Grave Digger";
		_portrait = Resources.Load ("Portraits/gravedigger") as Texture2D;
		TalkDistance = 7f;
		_chatFile = "chat_digger";
		_canTalk = true;
	}
	
	public override void GoodEnd() {
		GameObject[] spawners = GameObject.FindGameObjectsWithTag("DisabledEnemySpawner");
		Debug.Log (spawners.Length);
		for(int i = 0;i < spawners.Length;i++) {
			BaseSpawner spawner = spawners[i].GetComponent("BaseSpawner") as BaseSpawner;
			spawner._fauxActive=true;
		}
		Debug.Log ("Good end!");
		_canTalk = false;
		
	}
	
	public override void BadEnd() {
		Debug.Log ("Bad end!");
	}
}
