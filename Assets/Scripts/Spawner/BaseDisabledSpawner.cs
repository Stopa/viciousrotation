using UnityEngine;
using System.Collections;

public class BaseDisabledSpawner : BaseSpawner
{

	new void Start() {
		_spawnableObject = Resources.Load(_prefabPath);
		_spawnTimer = _spawnCooldown;
		_colliding = false;
		_fauxActive=false;
	}
}

