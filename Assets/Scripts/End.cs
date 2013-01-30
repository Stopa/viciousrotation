using UnityEngine;
using System.Collections;

public class End : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StoryDisplay sd = gameObject.GetComponent("StoryDisplay") as StoryDisplay;
		sd._story = "end";
		sd.LoadImages();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
