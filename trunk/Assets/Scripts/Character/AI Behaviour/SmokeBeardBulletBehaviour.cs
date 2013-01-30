using UnityEngine;
using System.Collections;

public class SmokeBeardBulletBehaviour : MonoBehaviour
{
	private GameObject _player;
	private float _speed;
	private float _timeToLive;
	private int _damage;

	void Start ()
	{
		_player = GameObject.FindWithTag ("Player");
		_speed = 1f;
		_timeToLive = 5f;
		_damage = 10;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(_timeToLive > 0) {
			Vector3 moveDirection = new Vector3(_player.transform.position.x-transform.position.x,0,_player.transform.position.z-transform.position.z);
			transform.position += moveDirection * _speed * Time.deltaTime;
			_timeToLive = _timeToLive - Time.deltaTime;
		} else {
			Die();
		}
	}
	
	void OnTriggerEnter() {
		Die ();
	}
	
	void Die() {
		GameObject explosion = Instantiate (Resources.Load ("Prefabs/Explosion"), new Vector3(transform.position.x, 5, transform.position.z), Quaternion.identity) as GameObject;
		Explosion boom = explosion.GetComponent("Explosion") as Explosion;
		boom._isEnemy = true;
		boom._damage = _damage;
		BaseSprite sprite = gameObject.GetComponent("BaseSprite") as BaseSprite;
		sprite.DestroySprite();
		Destroy (gameObject);
	}
}

