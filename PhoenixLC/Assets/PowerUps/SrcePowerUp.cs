using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SrcePowerUp : MonoBehaviour {

    public float heal=20f;
    public float rotateSpeed=10f;

    void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<PlayerHealth>().getHealth() < other.GetComponent<PlayerHealth>().maxHealth)
            {
                other.GetComponent<PlayerHealth>().Repair(heal);
                Destroy(gameObject);
            }
        }
    }

}
