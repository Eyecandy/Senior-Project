using UnityEngine;
using UnityEngine.Networking;

namespace UI_Scripts
{
	public class CountdownTimerSpawner : NetworkBehaviour {
		
		[SerializeField] private GameObject _countdownPrefab;

		public override void OnStartServer()
		{
			GameObject timer = Instantiate(_countdownPrefab);
			NetworkServer.Spawn(timer);
		}
	}
}
