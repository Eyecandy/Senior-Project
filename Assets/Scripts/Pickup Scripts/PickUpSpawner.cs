using UnityEngine;
using UnityEngine.Networking;

namespace Pickup_Scripts
{
	public class PickUpSpawner : NetworkBehaviour 
	{
	
		[SerializeField] private GameObject _pickUpPrefab;

		private Vector3[] Positions =
		{
			new Vector3(-24.6f,0.98f,23.1f),
			new Vector3(-25.3f,0.98f,2.9f),
			new Vector3(24.4f,0.98f,-0.2f),
			new Vector3(19f,0.98f,-24.5f),
			new Vector3(-23.9f,0.98f,-23.9f),
			new Vector3(1.7f,0.98f,27f)
		};

		private int[,] randomList = new int[,]{
			{ 0,1,2,3,4,5 }, 
			{ 0,2,5,4,1,3 }, 
			{ 5,4,1,2,0,3 }, 
			{ 4,2,1,3,5,0 }
		};

		public override void OnStartServer()
		{
			int randomSelection = Random.Range(0,4);
			for (int i = 0; i < Positions.Length;i++)
			{
				Vector3 position = Positions[randomList[randomSelection,i]];
				GameObject pickup = Instantiate(_pickUpPrefab, position, Quaternion.identity);
				pickup.name = "Pickup-"+i.ToString();
				NetworkServer.Spawn(pickup);
			}
		}
		
		
	}
}
