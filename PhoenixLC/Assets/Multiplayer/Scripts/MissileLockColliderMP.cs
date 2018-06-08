using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLockColliderMP : MonoBehaviour {

    ShootingMP player;

    void Start()
    {
        player = GetComponentInParent<ShootingMP>();
        GetComponent<SphereCollider>().radius = player.lockOnRange;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
            player.LockOnSystem(other);
    }
}
