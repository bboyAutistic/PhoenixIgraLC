using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LaserPowerUpMP : NetworkBehaviour {

    public float rotateSpeed = 10f;

    void Update()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime, 0f, 0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isServer)
            return;

        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<ShootingMP>().laserLevel < 5)
            {
                other.GetComponent<ShootingMP>().LaserLevelUp();
                Destroy(gameObject);
            }
        }
    }

}
