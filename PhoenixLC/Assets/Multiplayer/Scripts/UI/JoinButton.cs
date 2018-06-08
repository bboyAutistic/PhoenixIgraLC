using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JoinButton : MonoBehaviour {

	public void OnClick()
    {
        string address = transform.Find("Data").GetComponent<TextMeshProUGUI>().text;
        GameObject.Find("MP").GetComponent<MyNetworkDiscovery>().Join(address);
    }

    public void Host()
    {
        GameObject.Find("MP").GetComponent<MyNetworkManagerHud>().OnHostPress();
    }
    
    public void JoinIP()
    {
        GameObject.Find("MP").GetComponent<MyNetworkManagerHud>().OnJoinPress();
    }

    public void NapraviServer()
    {
        GameObject.Find("MP").GetComponent<MyNetworkDiscovery>().NapraviServer();
    }

    public void TraziServer()
    {
        GameObject.Find("MP").GetComponent<MyNetworkDiscovery>().TraziServer();
    }

    public void Ugasi()
    {
        GameObject.Find("MP").GetComponent<MyNetworkDiscovery>().Ugasi();
    }

}
