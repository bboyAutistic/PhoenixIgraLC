﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIskirpta : MonoBehaviour {

	public void PlayGame(string levelName)
	{
		SceneManager.LoadScene(levelName);
	}
	public void QuitGame()
	{
		Application.Quit();
	}
}