using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerGUI: MonoBehaviour
{
	private static bool _isActive;
	public void Start()
	{
		//NetworkManager.singleton.GetComponent<NetworkManagerHUD>().enabled = _isActive;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public static void EnableNetworkManagerHud()
	{
		_isActive = !_isActive;
		if (_isActive)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

	}



}
