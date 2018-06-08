using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RetroGameManager : MonoBehaviour {

    public List<GameObject> wavesOfEnemys;
    public GameObject winScreen;
    public TextMeshProUGUI dialogText;

    int currentWave = 0;
    
    bool spawning = false;

    private void Start()
    {
        
        Time.timeScale = 1;
        FindObjectOfType<AudioManager>().Play("Elise");
    }

    public void Update()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null && !spawning && currentWave < wavesOfEnemys.Count)
        {
            spawning = true;
            StartCoroutine(StartConflict());
        }

        if (GameObject.FindGameObjectWithTag("Enemy") == null && !spawning && currentWave == wavesOfEnemys.Count)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0;
            
        }
    }

    IEnumerator StartConflict()
    {

        if(currentWave < wavesOfEnemys.Count)
        {
            dialogText.text = "get ready a wave of enemies is comming your way";
            yield return new WaitForSeconds(12f);
        }
        else{
            dialogText.text = "Boss is comming and he is bringing backup";
            yield return new WaitForSeconds(6f);
        }
                
        dialogText.text = "";
            
          
        wavesOfEnemys[currentWave].SetActive(true);
        spawning = false;
        currentWave++;

        Debug.Log(currentWave);

        yield return new WaitForSeconds(1f);

    }
    public void PlayTotally()
    {
        FindObjectOfType<AudioManager>().Play("Totally");
    }
}
