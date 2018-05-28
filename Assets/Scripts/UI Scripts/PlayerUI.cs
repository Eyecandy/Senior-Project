using Player_Scripts;
using UnityEngine;

namespace UI_Scripts
{
	public class PlayerUI : MonoBehaviour {

		// Reference to Thruster fuel fill
		[SerializeField] private RectTransform _speedbarFill;
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
		}
	}
}