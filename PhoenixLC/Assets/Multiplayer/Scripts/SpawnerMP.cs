using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnerMP : NetworkBehaviour {

    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public int firstWave = 5;
    public int increment = 5;
    public int bossWave = 20;
    public bool spawnBoss = false;

    int nextWave;
    bool spawning = false;

    public void Start()
    {
        nextWave = firstWave;
    }

    public void Update()
    {
        if (!isServer)
            return;

        if (GameObject.FindGameObjectWithTag("Enemy") == null && !spawning && nextWave <= bossWave)
        {
            spawning = true;
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(5);
        SpawnEnemies(nextWave);
    }

    public void SpawnEnemies(int amount)
    {
        int rows = Mathf.CeilToInt(amount / 5);
        float Yposition = transform.position.y + ( 15 * (rows - 1));
        float Xposition = transform.position.x + 60;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < 5; c++)
            {
                NetworkServer.Spawn(Instantiate(enemyPrefab, new Vector3(Xposition, Yposition, transform.position.z), transform.rotation, null));
                Xposition -= 30;
            }
            Yposition -= 30;
            Xposition = transform.position.x + 60;
        }

        if (amount == bossWave && spawnBoss)
        {
            NetworkServer.Spawn(Instantiate(bossPrefab, new Vector3(transform.position.x, Yposition - 30f, transform.position.z - 5), transform.rotation, null));
        }


        nextWave += increment;
        spawning = false;
    }
}
