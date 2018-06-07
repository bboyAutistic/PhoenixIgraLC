using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShieldPowerUpMP : NetworkBehaviour {

    public float rotateSpeed = 10f;

    void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isServer)
            return;

        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerHealthMP>().getShield() < PlayerHealthMP.maxShield)
            {
                other.GetComponent<PlayerHealthMP>().RecoverShield();
                Destroy(gameObject);
            }
        }
    }

}
