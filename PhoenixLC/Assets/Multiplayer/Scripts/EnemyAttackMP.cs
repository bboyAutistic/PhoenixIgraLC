using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyAttackMP : NetworkBehaviour {

    public Transform pointOfOrigin;
    public GameObject laserBullet;
    public float rateOfFire = 300f;
    public float coneOfFire = 10f;
    public float laserSpeed = 300f;

    Transform target = null;
    Vector3 lineToTarget;
    float reloadTimer = 0f;
    float targetTimer = 0f;

    void Update()
    {
        if (!isServer)
            return;

        reloadTimer += Time.deltaTime;
        targetTimer += Time.deltaTime;

        if(target == null)
        {
            if (!FindTarget())
                return;
        }
        else if(targetTimer >= 10f)
        {
            targetTimer = 0f;
            if (!FindTarget())
                return;
        }
        
        if (CanHit())
        {
            if (reloadTimer >= 60 / Random.Range(rateOfFire-50,rateOfFire+50))
            {
                reloadTimer = 0;
                FireEnemyLaser();
            }
        }
        
    }

    bool FindTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
        List<GameObject> list = new List<GameObject>();
        for(int i=0; i< targets.Length; i++)
        {
            list.Add(targets[i]);
        }
        list.Sort((e, x) => Vector3.Distance(e.transform.position, transform.position).CompareTo(Vector3.Distance(x.transform.position, transform.position)));

        GameObject temp;
        if (list.Count != 0)
            temp = list[0];
        else
            temp = null;
        if (temp == null)
            return false;

        target = temp.transform;
        return true;
    }

    bool CanHit()
    {
        lineToTarget = target.position - pointOfOrigin.position;
        float angle = Vector3.Angle(pointOfOrigin.forward, lineToTarget);

        RaycastHit hit;
        if (angle < coneOfFire)
        {
            if (Physics.Raycast(pointOfOrigin.position, lineToTarget, out hit, 1000, ~LayerMask.GetMask("LaserBullets", "Sudari")))
            {
                if (hit.collider.tag == "Player")
                {
                    return true;
                }
            }
        }
        
        return false;
    }

    void FireEnemyLaser()
    {
        GameObject bullet = Instantiate(laserBullet, pointOfOrigin.position, pointOfOrigin.rotation, null);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(lineToTarget.normalized * rb.mass * laserSpeed, ForceMode.Impulse);
        NetworkServer.Spawn(bullet);
        pointOfOrigin.gameObject.GetComponent<AudioSource>().Play();
    }
}
