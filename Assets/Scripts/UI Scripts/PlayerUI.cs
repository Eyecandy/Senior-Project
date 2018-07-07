using Player_Scripts;
using UnityEngine;

namespace UI_Scripts
{
	public class PlayerUI : MonoBehaviour {

		// Reference to Thruster fuel fill
		[SerializeField] private RectTransform _speedbarFill;
		[SerializeField] private GameObject _scorePanel;
		
		private Player _player;

		public void SetPlayer(Player player)
		{
			_player = player; 
		}

		void SetSpeedbarFill(float amount)
		{
			_speedbarFill.localScale = new Vector3(amount/100f, 1f, 1f);
		}

		void Update()
		{
			SetSpeedbarFill(_player.GetCurrentWalkingSpeedPercentage());

			if (Input.GetKeyDown(KeyCode.Tab))
			{	
				
				_scorePanel.SetActive(true);
				
			}

			if (Input.GetKeyUp(KeyCode.Tab))
			{
				_scorePanel.SetActive(false);
			}

			if (GameManager.Singleton.IsGameOver)
			{
				foreach (Transform child in this.transform)
				{
					if (!child.transform.name.Equals("ScoreGameObject"))
						child.gameObject.SetActive(false);
				}

				_scorePanel.SetActive(true);
			}

		}

		


	}
}