using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public int firstWave = 5;
    public int increment = 5;
    public int bossWave = 20;

    public TextMeshProUGUI dialogText;
    public GameObject winScreen;

    int nextWave;
    bool spawning = false;

    public void Start()
    {
        Time.timeScale = 1;
        nextWave = firstWave;
        spawning = true;
        StartCoroutine(DialogText());
    }

    public void Update()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null && !spawning && nextWave <= bossWave)
        {
            spawning = true;
            StartCoroutine(Spawn());
        }
        if (GameObject.FindGameObjectWithTag("Enemy") == null && !spawning && nextWave > bossWave)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            winScreen.SetActive(true);
            spawning = true;
            
        }
    }

    IEnumerator DialogText()
    {
        dialogText.text = "Welcome to the Phoenix asteroid ring";
        yield return new WaitForSeconds(4);
        dialogText.text = "your objective is to defite the evil boss os the phoenix gang";
        yield return new WaitForSeconds(6);
        dialogText.text = "prepare yourself the first wave of Birdships are comming your way";
        spawning = false;
        yield return new WaitForSeconds(6);
        dialogText.text = "";
    }

    IEnumerator Spawn()
    {

        yield return new WaitForSeconds(8);
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
