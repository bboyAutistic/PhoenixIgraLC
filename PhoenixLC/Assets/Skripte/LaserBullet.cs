using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour {

	float lifetimeTimer = 0f;

	void Update () {
		lifetimeTimer += Time.deltaTime;
		if (lifetimeTimer >= 10f) {
			Destroy (gameObject);
		}
	}
}
