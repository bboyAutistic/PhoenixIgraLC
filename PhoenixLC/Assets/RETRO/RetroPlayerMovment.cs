using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetroPlayerMovment : MonoBehaviour {

    public float moveSpeed = 0.5f;

    Rigidbody rb;
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        
    }
    void FixedUpdate()
    {

        
        float fwd = Input.GetAxis("Vertical");
        if (fwd >= 0)
        {
            rb.AddForce(fwd * transform.forward * moveSpeed, ForceMode.Impulse);
        }
        else
        {
            //prema nazad sporije ide
            rb.AddForce(fwd * transform.forward * moveSpeed * 0.5f, ForceMode.Impulse);
        }

       
        float side = Input.GetAxis("Horizontal");
        if (side != 0)
        {
            rb.AddForce(side * transform.right * moveSpeed, ForceMode.Impulse);
        }

       
    }
}
