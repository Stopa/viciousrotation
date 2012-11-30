using UnityEngine;
using System.Collections;

public class DayCycle : MonoBehaviour {
	
	public GameObject[] _suns;
	public float _cycleLength;
	public float _cycleTime;
	private int _currentSun;
	
	// Use this for initialization
	void Start () {
		_cycleLength = 20.0f;
		_cycleTime = _cycleLength;
	}
	
	// Update is called once per frame
	void Update () {
		if(_cycleTime > 0)
			_cycleTime -= Time.deltaTime;
		if(_cycleTime <= 0) {
			_cycleTime = _cycleLength;
			ChangeSun(0);
		}
		else if(_cycleTime <= 3 || (_cycleTime >= 10 && _cycleTime <= 13)) {
			ChangeSun(1);
		}
		else if(_cycleTime >= 3 && _cycleTime <= 10){
			ChangeSun(2);
		}
	}
	
	
	private void ChangeSun(int sun) {
		if(sun != _currentSun) {
			_suns[_currentSun].light.intensity = 0;
			_suns[sun].light.intensity = 4;
			_currentSun = sun;
			Debug.Log("Changed sun to " + _suns[_currentSun].name);
		}
	}
	
}
