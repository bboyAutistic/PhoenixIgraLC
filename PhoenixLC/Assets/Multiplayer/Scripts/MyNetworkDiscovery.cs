using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class MyNetworkDiscovery : NetworkDiscovery {

    List<string> lista = new List<string>();

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        base.OnReceivedBroadcast(fromAddress, data);

        if (lista.Exists(e => e.Equals(fromAddress)))
        {
            return;
        }
        else
        {
            lista.Add(fromAddress);
            GameObject gameprefab = GetComponent<MyNetworkManagerHud>().game;
            GameObject game = Instantiate(gameprefab, GetComponent<MyNetworkManagerHud>().panel.transform);
            game.GetComponentInChildren<TextMeshProUGUI>().text = fromAddress;
        }
    }

    public void Join(string address)
    {
        GetComponent<MyNetworkManagerHud>().Join(address);
        
    }

    public void NapraviServer()
    {
        StopBroadcast();
        Initialize();
        StartAsServer();
        GameObject[] games = GameObject.FindGameObjectsWithTag("MPgame");
        foreach (GameObject x in games)
        {
            Destroy(x);
        }
        lista.Clear();
    }

    public void TraziServer()
    {
        Initialize();
        StartAsClient();
        GameObject[] games = GameObject.FindGameObjectsWithTag("MPgame");
        foreach (GameObject x in games)
        {
            Destroy(x);
        }
        lista.Clear();
    }

    public void Ugasi()
    {
        StopBroadcast();
        GameObject[] games = GameObject.FindGameObjectsWithTag("MPgame");
        foreach(GameObject x in games)
        {
            Destroy(x);
        }
        lista.Clear();
    }
}
