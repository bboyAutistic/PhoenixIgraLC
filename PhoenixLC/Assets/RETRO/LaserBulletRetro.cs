using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBulletRetro : MonoBehaviour {


    public float damage = 25f;
    public bool isEnemy=false;

    float lifetimeTimer = 0f;


    void Update()
    {
        lifetimeTimer += Time.deltaTime;
        if (lifetimeTimer >= 10f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy" && isEnemy==false)
        {
            other.gameObject.GetComponentInChildren<EnemyHealthRetro>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Enemy" && isEnemy==true)
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<RetroPlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
