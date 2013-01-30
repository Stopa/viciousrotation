using UnityEngine;
using System.Collections;

public class PissFairyChat : BaseNPCChat {

	void Awake() {
		Name = "Piss Fairy";
		_portrait = Resources.Load ("Portraits/Fairy") as Texture2D;
		TalkDistance = 7f;
		_chatFile = "chat";
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
		GameObject player = GameObject.FindWithTag("Player");
		PlayerCharacter pc = player.GetComponent("PlayerCharacter") as PlayerCharacter;
		pc.SetFlag("gotPiss",true);
	}
	
	public override void BadEnd() {
		Debug.Log ("Bad end!");
		_canTalk = false;
		GameObject exit = GameObject.Find("ExitCollider");
		LevelChanger lc = exit.GetComponent("LevelChanger") as LevelChanger;
		lc._canExit = false;
	}
}
