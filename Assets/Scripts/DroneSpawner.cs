

using UnityEngine;
using UnityEngine.Networking;

public class DroneSpawner : MonoBehaviour
{
	[SerializeField]private  GameObject _dronePrefab1;
	[SerializeField]private  GameObject _dronePrefab2;

	

	public void Spawn()
	{
		
		Instantiate(_dronePrefab1);
		Instantiate(_dronePrefab2);

	}
}
