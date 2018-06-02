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

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Enemy") {
			other.gameObject.GetComponent<EnemyHealth> ().TakeDamage (25);
			Destroy (gameObject);
		} else if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<PlayerHealth> ().TakeDamage (25);
			Destroy (gameObject);
		}
	}
}
