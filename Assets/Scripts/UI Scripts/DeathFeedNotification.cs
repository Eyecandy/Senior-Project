using UnityEngine;
using UnityEngine.UI;

namespace UI_Scripts
{
	public class DeathFeedNotification : MonoBehaviour
	{

		[SerializeField] private Text _deathFeedText;
		private string x;
    
		public void Setup(string playerName)
		{

			_deathFeedText.text = playerName + "   Died";

		}
	
	


	}
}
