using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RertoBOSSEnemyAttack : MonoBehaviour {


    public List<Transform> pointOfOrigins;
    public GameObject laserBullet;
    public float rateOfFire = 100f;
    public float coneOfFire = 10f;
    public float laserSpeed = 100f;

    float reloadTimer = 0f;
    bool endCorutine = true;

    private void Start()
    {
        StartCoroutine(FireLaser());
    }

    void Update()
    {
        reloadTimer = 0;

    }

    IEnumerator FireLaser()
    {
        while (endCorutine)
        {
            for (int i = 0; i < pointOfOrigins.Count; i++)
            {
                GameObject bullet = Instantiate(laserBullet, pointOfOrigins[i].position, pointOfOrigins[i].rotation, null);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(bullet.transform.forward * rb.mass * laserSpeed, ForceMode.Impulse);

                pointOfOrigins[i].gameObject.GetComponent<AudioSource>().Play();
            }
           

            float sekunda = Random.Range(1f, 4f);
            yield return new WaitForSeconds(sekunda);
        }

    }

}
