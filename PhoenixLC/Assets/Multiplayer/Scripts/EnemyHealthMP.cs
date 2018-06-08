using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyHealthMP : NetworkBehaviour {

    public float maxHealth = 100f;
    public GameObject deathExplosion;
    public List<GameObject> powerUps;

    float currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float dmg)
    {
        if(!isServer)
            return;

        currentHealth -= dmg;

        if (currentHealth <= 0)
            Death();
    }

    void Death()
    {
        NetworkServer.Spawn(Instantiate(deathExplosion, transform.position, transform.rotation));
        Destroy(transform.root.gameObject);

        if (Random.Range(1, 100) < 35)
        {
            if(powerUps.Count != 0)
            {
                NetworkServer.Spawn(Instantiate(powerUps[Random.Range(0, powerUps.Count - 1)], transform.position, transform.rotation, null));
            }
        }
    }
}
