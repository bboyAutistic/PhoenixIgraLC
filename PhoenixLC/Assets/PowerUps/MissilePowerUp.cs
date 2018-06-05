using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePowerUp : MonoBehaviour {

    public float rotateSpeed = 10f;

    void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Shooting>().AddMissile();
            Destroy(gameObject);
        }
    }

}
