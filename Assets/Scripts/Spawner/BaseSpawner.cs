using UnityEngine;
using System.Collections;

public class BaseSpawner : MonoBehaviour {
	
	public string _prefabPath;
	public int _maxSpawns;
	public float _spawnCooldown;
	
	private Object _spawnableObject;
	private float _spawnTimer;
	private int _totalSpawns;
	private bool _colliding;

	void Start () {
		_spawnableObject = Resources.Load(_prefabPath);
		_spawnTimer = _spawnCooldown;
		_colliding = false;
	}
	
	void Update () {
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
	
	void OnCollisionEnter() {
		_colliding = true;
	}
	
	void OnCollisionExit() {
		_colliding = false;
	}
}
