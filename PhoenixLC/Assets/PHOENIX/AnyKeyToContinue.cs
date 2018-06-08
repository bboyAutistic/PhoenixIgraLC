using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AnyKeyToContinue : MonoBehaviour
{

    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("MainMenu");
            FindObjectOfType<AudioManager>().Play("Totally");
        }


    }
}
