using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryDisplay : MonoBehaviour {
	
	private GUISkin _customSkin;
	private Texture2D _backGround;
	private Texture2D _curImage;
	private Queue<Texture2D> _images;
	private Queue<Queue<string>> _texts;
	private string _curText;
	private float _waitTime;
	private bool _finished;
	private string _btnLabel;
	public bool _display;
	public string _story;
	
	// Use this for initialization
	void Start () {
		_customSkin = Resources.Load("Gui/StorySkin") as GUISkin;
		_backGround = Resources.Load("Story/paperBackground") as Texture2D;
		_waitTime = 0;
		_finished = false;
		_display = false;
		_images  = new Queue<Texture2D>();
		_texts = new Queue<Queue<string>>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!_finished && _display) {
			if(_waitTime < 5)
				_waitTime += Time.deltaTime;
			else {	
				NextText();
				_waitTime = 0;
			}
		}
	}
	
	void OnGUI() {	
		if(_display) {
			float height = Screen.height - 100;
			float width = height/10*16;
			float offset = (Screen.width - width)/2;
			if(_curImage != null) {
				GUI.DrawTexture(new Rect(offset, 0, width, Screen.height), _backGround);
				GUI.DrawTexture(new Rect(offset, 0, width, height), _curImage);	
				GUI.skin = _customSkin;
				GUI.Label(new Rect(offset, height, width, 100), _curText);
				GUI.skin = null;
			}
			else {
				
			}
			if(_finished) {
				GUI.skin = _customSkin;
				if(GUI.Button(new Rect(offset + 25, height + 25, 100, 50),_btnLabel))
					ButtonPressed();
				GUI.skin = null;
			}
		}
	}
	
	private void NextImage() {
		if(_images.Count != 0) {
			_curImage = _images.Dequeue();
			_texts.Dequeue();
			_waitTime = 0;
			NextText();
		}
		else
			_finished = true;
	}
	
	private void NextText() {
		Queue<string> q = _texts.Peek();
		if(q.Count != 0)
			_curText = q.Dequeue();
		else
			NextImage();
	}
	
	private void ButtonPressed() {
		if(_story == "start") 
			Application.LoadLevel("graveyard");
		else
			Application.LoadLevel("MainMenu");
		
	}
	
	public void LoadImages() {
		if(_story == "start") {
			_images.Enqueue(Resources.Load("Story/start_1") as Texture2D);
			_images.Enqueue(Resources.Load("Story/start_2") as Texture2D);
			_images.Enqueue(Resources.Load("Story/start_3") as Texture2D);
			_images.Enqueue(Resources.Load("Story/start_4") as Texture2D);
	
			Queue<string> text_1 = new Queue<string>();
			text_1.Enqueue("You are a scientist. You even have a laboratory!");
			text_1.Enqueue("As a man of science you are expected to conduct scientific experiments.");
			_texts.Enqueue(text_1);
			Queue<string> text_2 = new Queue<string>();
			text_2.Enqueue("Unfortunately that means the occasional spill of acid or a housefire...");
			text_2.Enqueue("And sometimes you might just blow yourself up while researching demonic magic.");
			_texts.Enqueue(text_2);
			Queue<string> text_3 = new Queue<string>();
			text_3.Enqueue("All the villagers wept when you were buried.");
			text_3.Enqueue("Little did they know that your science would live on.");
			_texts.Enqueue(text_3);
			Queue<string> text_4 = new Queue<string>();
			text_4.Enqueue("The demonic magic found it's way into your body.");
			text_4.Enqueue("And it will not let you rest.");
			_texts.Enqueue(text_4);
			
			_btnLabel = "Continue";
		}		
		else if(_story == "end") {
			_images.Enqueue(Resources.Load("Story/end_1") as Texture2D);
			_images.Enqueue(Resources.Load("Story/end_2_1") as Texture2D);
			_images.Enqueue(Resources.Load("Story/end_2_2") as Texture2D);
	
			Queue<string> text_1 = new Queue<string>();
			text_1.Enqueue("Rejoice for you are victorious.");
			text_1.Enqueue("You killed your nemesis and buried his remains.");
			_texts.Enqueue(text_1);
			Queue<string> text_2 = new Queue<string>();
			text_2.Enqueue("The curse is gone and you can live a normal life once again.");
			text_2.Enqueue("And what else would a scientist do now?");
			_texts.Enqueue(text_2);
			Queue<string> text_3 = new Queue<string>();
			text_3.Enqueue("But of course continue his work!");
			text_3.Enqueue("Because science never waits.");
			_texts.Enqueue(text_3);
			
			_btnLabel = "Exit";
		}

		_curImage = _images.Dequeue();
		NextText();
		_display = true;
	}
}
