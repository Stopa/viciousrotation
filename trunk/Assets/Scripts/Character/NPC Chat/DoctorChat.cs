using UnityEngine;
using System.Collections;

public class DoctorChat: BaseNPCChat {

	void Awake() {
		Name = "Doctor";
		_portrait = Resources.Load ("Portraits/Doctor") as Texture2D;
		TalkDistance = 7f;
		_chatFile = "chat_doctor";
		_canTalk = true;
	}
	
	public override void GoodEnd() {
		Debug.Log ("Good end!");
		GameObject player = GameObject.FindWithTag("Player");
		PlayerCharacter pc = player.GetComponent("PlayerCharacter") as PlayerCharacter;
		pc.SetFlag("talkedDoctor", true);
		_canTalk = false;
	}
	
	public override void BadEnd() {
		Debug.Log ("Bad end!");
	}
}
