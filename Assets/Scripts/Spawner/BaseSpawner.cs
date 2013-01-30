using UnityEngine;
using System.Collections;

public class BaseSpawner : MonoBehaviour {
	
	public string _prefabPath;
	public int _maxSpawns;
	public float _spawnCooldown;
	public bool _fauxActive;
	
	protected Object _spawnableObject;
	protected float _spawnTimer;
	private int _totalSpawns;
	protected bool _colliding;

	void Start () {
		_spawnableObject = Resources.Load(_prefabPath);
		_spawnTimer = 0;
		_colliding = false;
		_fauxActive = true;
	}
	
	void Update () {
		if(!_fauxActive) {return;}
		if(_totalSpawns >= _maxSpawns) {
			// disable spawner when maximum number of NPCs have been spawned
			gameObject.active = false;
		}
		if(_spawnTimer <= 0) {
			// don't spawn NPCs on top of each other
			if(!_colliding) {
				Spawn();
			}
			_spawnTimer = _spawnCooldown;
		} else {
			_spawnTimer -= Time.deltaTime;
		}
	}
	
	public void Spawn() {
		Instantiate(_spawnableObject, transform.position, Quaternion.identity);
		_totalSpawns++;
	}
	
	void OnTriggerEnter() {
		_colliding = true;
	}
	
	void OnTriggerExit() {
		_colliding = false;
	}
}
