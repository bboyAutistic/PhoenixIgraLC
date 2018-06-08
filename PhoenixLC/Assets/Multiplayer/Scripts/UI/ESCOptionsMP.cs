using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCOptionsMP : MonoBehaviour {

    public GameObject pauseMenu;
    bool menuOpen = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menuOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            pauseMenu.SetActive(true);
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
        menuOpen = false;
    }
}
