using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetroShooting : MonoBehaviour
{

    //laser public variables
    public GameObject laserBullet;
    public float rateOfFire = 120f;
    public Transform pointOfOrigin;
    public int laserLevel = 1;
    public float laserSpeed = 300f;

    //laser private variables
    float reloadTimer = 0f;

    void Update()
    {
        if (Time.timeScale == 0) { return; }

        //laseri
        reloadTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && reloadTimer >= 60 / rateOfFire)
        {
            reloadTimer = 0;
            FireLaser();
        }
    }


    void FireLaser()
    {
        
        GameObject bullet = Instantiate(laserBullet, pointOfOrigin.position, pointOfOrigin.rotation, null);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(bullet.transform.forward * rb.mass * laserSpeed, ForceMode.Impulse);
        pointOfOrigin.gameObject.GetComponent<AudioSource>().Play();

    }
}
