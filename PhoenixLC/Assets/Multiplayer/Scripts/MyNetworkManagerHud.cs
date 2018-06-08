using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MyNetworkManagerHud : MonoBehaviour {

    public NetworkManager manager;
    public Text ipAddressText;
    public GameObject panel;
    public GameObject game;

    void Awake()
    {
        manager = GetComponent<NetworkManager>();
    }

    public void OnHostPress()
    {
        manager.StartHost();
    }

    public void OnJoinPress()
    {
        manager.networkAddress = ipAddressText.text;
        manager.networkPort = 7777;
        manager.StartClient();
    }

    public void Join(string address)
    {
        manager.networkAddress = address;
        manager.networkPort = 7777;
        manager.StartClient();
    }
}
