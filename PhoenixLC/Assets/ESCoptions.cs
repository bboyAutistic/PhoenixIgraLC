using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCoptions : MonoBehaviour {

    public GameObject pauseMenu;
	
	void Start () {
        Cursor.visible = false;
    }
	
	
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.visible = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
       
	}
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
    }
}
