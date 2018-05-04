using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerGUI: MonoBehaviour
{
	public void Start()
	{
		NetworkManager.singleton.GetComponent<NetworkManagerHUD>().enabled = false;
		Cursor.visible = false;
	}

	public static void EnableNetworkManagerHud()
	{
		var networkManagerHud = NetworkManager.singleton.GetComponent<NetworkManagerHUD>();
		networkManagerHud.enabled = !networkManagerHud.enabled;
		Cursor.visible = !Cursor.visible;
	}



}
