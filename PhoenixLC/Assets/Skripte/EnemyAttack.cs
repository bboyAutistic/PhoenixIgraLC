using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	public Transform pointOfOrigin;
	public GameObject laserBullet;
	public float rateOfFire = 300f;
	public float coneOfFire = 10f;

	Transform target;
	Vector3 lineToTarget;
	float reloadTimer = 0f;

    void Update()
    {
		reloadTimer += Time.deltaTime;

		if (!FindTarget ())
			return;

		if (CanHit ()) {
			if (reloadTimer >= 60 / rateOfFire) {
				reloadTimer = 0;
				FireEnemyLaser ();
			}
        }
    }

	bool FindTarget()
	{
		GameObject temp = GameObject.FindWithTag ("Player");
		if (temp == null)
			return false;

		target = temp.transform;
		return true;
	}

	bool CanHit(){
		lineToTarget = target.position - pointOfOrigin.position;
		float angle = Vector3.Angle (pointOfOrigin.forward, lineToTarget);

		RaycastHit hit;
		if (angle < coneOfFire) {
            if (Physics.Raycast(pointOfOrigin.position, lineToTarget, out hit, 1000, ~LayerMask.GetMask("LaserBullets", "Sudari")))
            {
                if (hit.collider.tag == "Player")
                {
                    return true;
                }
                else
                    Debug.Log(hit.collider.name);
            }
		}

		return false;
	}

	void FireEnemyLaser()
	{
		GameObject bullet = Instantiate (laserBullet, pointOfOrigin.position, pointOfOrigin.rotation, null);
		Rigidbody rb = bullet.GetComponent<Rigidbody> ();
		rb.AddForce (lineToTarget.normalized * rb.mass * 300, ForceMode.Impulse);
	}
}
