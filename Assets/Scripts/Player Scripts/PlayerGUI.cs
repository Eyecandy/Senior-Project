using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Networking;

public class PlayerGUI: NetworkBehaviour
{
	private static bool _isActive;

	[HideInInspector] public GameObject HitMarker;

	[SerializeField] private float _hitMarkerTimer;
	
	public void Start()
	{
		
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
    
	
	private void Update()
	{
		
		Menu();
	}
	
	private static void Menu()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			//EnableNetworkManagerHud();
		}
	}
	
	

	public void SetHitMarker()
	{
		
		HitMarker.SetActive(true);
		StartCoroutine(RemoveHitMarker());
	}

	IEnumerator RemoveHitMarker()
	{
		yield return new WaitForSeconds(_hitMarkerTimer);
		HitMarker.SetActive(false);
	}
}
