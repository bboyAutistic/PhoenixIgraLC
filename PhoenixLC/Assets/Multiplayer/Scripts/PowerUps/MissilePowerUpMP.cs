using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MissilePowerUpMP : NetworkBehaviour {

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
            other.GetComponent<ShootingMP>().AddMissile();
            Destroy(gameObject);

        }
    }

}
