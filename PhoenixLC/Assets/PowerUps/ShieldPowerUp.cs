﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour {
    
    public float rotateSpeed = 10f;

    void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerHealth>().getShield() < other.GetComponent<PlayerHealth>().maxShield)
            {
                other.GetComponent<PlayerHealth>().RecoverShield(100);
                Destroy(gameObject);
            }
        }
    }

}