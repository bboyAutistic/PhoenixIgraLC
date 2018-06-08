using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSistem : MonoBehaviour {

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScore;
    public int points;

    private void Start()
    {
        scoreText.text = "Score: " + points;
        highScore.text ="High Score: "+ PlayerPrefs.GetInt("HighScore", 0).ToString();
        UpdateScore(points);
    }
   
    public void UpdateScore(int _points)
    {
        points += _points;
        scoreText.text = "Score: "+points;

        if(PlayerPrefs.GetInt("HighScore", 0) < points)
        {
            highScore.text = "HighScore: " + points;
            PlayerPrefs.SetInt("HighScore", points);
        }
    }
}
