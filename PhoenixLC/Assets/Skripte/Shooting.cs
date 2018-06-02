using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

	public GameObject laserBullet;
	public float rateOfFire = 120f;
	public Transform pointOfOrigin;

	float reloadTimer = 0f;
	

	void Update () {

		reloadTimer += Time.deltaTime;
		if (Input.GetMouseButton (0) && reloadTimer >= 60 / rateOfFire) {
			reloadTimer = 0;
			FireLaser ();
		}
	}

	void FireLaser(){
		GameObject bullet = Instantiate (laserBullet, pointOfOrigin.position, pointOfOrigin.rotation, null);
		Rigidbody rb = bullet.GetComponent<Rigidbody> ();
		rb.AddForce (bullet.transform.forward * rb.mass * 300, ForceMode.Impulse);
	}
}
