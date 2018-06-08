using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class SpawnerMP : NetworkBehaviour {

    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public int firstWave = 5;
    public int increment = 5;
    public int bossWave = 20;
    public bool spawnBoss = false;

    int nextWave;
    bool spawning = false;

    public TextMeshProUGUI dialogText;
    public GameObject winScreen;

    public void Start()
    {
        spawning = true;
        StartCoroutine(DialogText());
        
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
        if (GameObject.FindGameObjectWithTag("Enemy") == null && !spawning && nextWave > bossWave)
        {

            spawning = true;
            StartCoroutine(WinDilej());

        }
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2);
        if (nextWave == bossWave)
        {
            dialogText.text = "Boss is comming and he is bringing birdup";

        }
        else
        {
            dialogText.text = "prepare yourself a wave of Birdships is comming your way";
        }

        yield return new WaitForSeconds(8);
        dialogText.text = "";
        SpawnEnemies(nextWave);
        
    }
    IEnumerator DialogText()
    {
        yield return new WaitForSeconds(10);
        dialogText.text = "Welcome to the Phoenix asteroid field";
        yield return new WaitForSeconds(4);
        dialogText.text = "your objective is to defeat the evil boss of the phoenix gang";
        yield return new WaitForSeconds(6);
        spawning = false;

    }
    public void SpawnEnemies(int amount)
    {
        if (!isServer)
            return;

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
    IEnumerator WinDilej()
    {

        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        winScreen.SetActive(true);
    }
}
