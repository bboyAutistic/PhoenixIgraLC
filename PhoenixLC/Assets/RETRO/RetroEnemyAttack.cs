using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetroEnemyAttack : MonoBehaviour {

    public Transform pointOfOrigin;
    public GameObject laserBullet;
    public float rateOfFire = 100f;
    public float coneOfFire = 10f;
    public float laserSpeed = 100f;

    
    bool endCorutine = true;

    private void Start()
    {
        StartCoroutine(FireLaser());
    }
    
    IEnumerator FireLaser()
    {
        float sekunda = Random.Range(1f, 4f);
        yield return new WaitForSeconds(sekunda);
        while (endCorutine)
        {
            GameObject bullet = Instantiate(laserBullet, pointOfOrigin.position, pointOfOrigin.rotation, null);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(bullet.transform.forward * rb.mass * laserSpeed, ForceMode.Impulse);

            pointOfOrigin.gameObject.GetComponent<AudioSource>().Play();

            sekunda=Random.Range(1f, 4f);
            yield return new WaitForSeconds(sekunda);
        }
       
    }

   
}
