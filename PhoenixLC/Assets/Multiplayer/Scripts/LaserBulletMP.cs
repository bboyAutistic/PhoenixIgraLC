using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LaserBulletMP : NetworkBehaviour {

    public float damage = 25f;
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
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponentInChildren<EnemyHealthMP>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealthMP>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}
