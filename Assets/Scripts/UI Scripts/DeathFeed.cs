using UnityEngine;

namespace UI_Scripts
{
	public class DeathFeed : MonoBehaviour
	{
		[SerializeField] private GameObject _deathFeedNotificationPrefab;
		// Use this for initialization
		void Start ()
		{
			GameManager.Singleton._onPlayerDeathCallBack += OnDeath;
		}

		private void OnDeath(string playerName)
		{
		
			Debug.Log("Killed " + playerName);
			var deathFeedNotifcationInstance = Instantiate(_deathFeedNotificationPrefab, transform);
			deathFeedNotifcationInstance.GetComponent<DeathFeedNotification>().Setup(playerName);
			Destroy(deathFeedNotifcationInstance,2f);
		
		}
	}
}
