using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public int firstWave = 5;
    public int increment = 5;
    public int bossWave = 20;

    int nextWave;
    bool spawning = false;

    public void Start()
    {
        nextWave = firstWave;
    }

    public void Update()
    {
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
        float Yposition = 15 * (rows - 1);
        float Xposition = 60;

        for (int r = 0; r < rows; r++)
        {
            for(int c = 0; c < 5; c++)
            {
                Instantiate(enemyPrefab, new Vector3(Xposition, Yposition, transform.position.z), transform.rotation, null);
                Xposition -= 30;
            }
            Yposition -= 30;
            Xposition = 60;
        }

        if(amount == bossWave)
        {
            Instantiate(bossPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z - 5), transform.rotation, null);
        }


        nextWave += increment;
        spawning = false;
    }

    

}
