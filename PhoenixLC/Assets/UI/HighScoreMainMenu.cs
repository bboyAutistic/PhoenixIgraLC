using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreMainMenu : MonoBehaviour {

    public TextMeshProUGUI highScore;
    private void Start()
    {
        highScore.text ="High Score: "+ PlayerPrefs.GetInt("HighScore", 0).ToString();
        Time.timeScale = 1;
        
    }
}
