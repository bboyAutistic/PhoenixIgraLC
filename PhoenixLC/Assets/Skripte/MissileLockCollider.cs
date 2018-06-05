using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLockCollider : MonoBehaviour {

    Shooting player;

	void Start()
    {
        player = GetComponentInParent<Shooting>();
        GetComponent<SphereCollider>().radius = player.lockOnRange;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
            player.LockOnSystem(other);
    }

}
