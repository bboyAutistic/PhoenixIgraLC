using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	public Transform pointOfOrigin;
	public GameObject laserBullet;
	public float rateOfFire = 300f;
	float reloadTimer = 0f;

    [SerializeField]
    Transform target;

    Laser laser;

    Vector3 hitPosition;

	void Awake(){
		laser = GetComponentInChildren<Laser> ();
	}

    void Update()
    {
		reloadTimer += Time.deltaTime;

        if (!FindTarget()) return;
        //InFront();
        //HaveLineOfSightRayCast();
        if(InFront() && HaveLineOfSightRayCast())
        {
			if (reloadTimer >= 60 / rateOfFire) {
				reloadTimer = 0;
				FireEnemyLaser ();
			}
        }
    }



	bool InFront()
    {
        Vector3 directionToTarget = transform.position - target.position;
        float angle = Vector3.Angle(transform.forward, directionToTarget);

        if (Mathf.Abs(angle) > 120 && Mathf.Abs(angle) < 300)
        {
           // Debug.DrawLine(transform.position, target.position,Color.green);
            return true;
        }
        //Debug.DrawLine(transform.position, target.position, Color.red);
        return false;
    }


    bool HaveLineOfSightRayCast()
    {
        RaycastHit hit;

        Vector3 direction = target.position - transform.position;
        
        if(Physics.Raycast(laser.transform.position,direction,out hit,laser.Distance()))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.DrawRay(laser.transform.position, direction, Color.green);
                hitPosition = hit.transform.position;
                return true;
            }
        }
        return false;
    }


    void FireEnemyLaser()
    {
		/*
        //Debug.Log("fire Laseeerrrrrrrr");
		if (laser.getCanFire ()) {
			laser.FireLaser(hitPosition,target);
		}
		//laser.FireLaser();
		*/

			GameObject bullet = Instantiate (laserBullet, pointOfOrigin.transform.position, pointOfOrigin.transform.rotation, null);
			Rigidbody rb = bullet.GetComponent<Rigidbody> ();
			rb.AddForce (bullet.transform.forward * rb.mass * 300, ForceMode.Impulse);


    }

    bool FindTarget()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;
        if (target == null) return false;
        return true;
    }
}
