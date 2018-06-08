using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCoptions : MonoBehaviour {

    public GameObject pauseMenu;
    public bool lockCursorOnStart = true;
    bool menuOpen = false;

    void Start()
    {
        if (lockCursorOnStart)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menuOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            menuOpen = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && menuOpen)
        {
            Resume();
        }

    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        menuOpen = false;
    }
}
