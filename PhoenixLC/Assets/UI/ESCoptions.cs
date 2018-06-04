using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCoptions : MonoBehaviour {

    public GameObject pauseMenu;
	
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
    }
	
	
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
			Cursor.lockState = CursorLockMode.None;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
       
	}
    public void Resume()
    {
        pauseMenu.SetActive(false);
		Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
}
