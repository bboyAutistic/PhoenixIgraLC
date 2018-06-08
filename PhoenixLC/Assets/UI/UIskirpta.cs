using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class UIskirpta : MonoBehaviour {

	public void PlayGame(string levelName)
	{
        GameObject.Find("MP").GetComponent<NetworkManager>().StopHost();
		SceneManager.LoadScene(levelName);
	}
	public void QuitGame()
	{
		Application.Quit();
	}
}
