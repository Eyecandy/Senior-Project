
using UnityEngine;

public class Score : MonoBehaviour
{

	[SerializeField] private GameObject _playerListGameObject;
	
	

	
	public void OnEnable()
	{
		Debug.Log("Enabled SCORE PANEL");
		_playerListGameObject.GetComponent<PlayerListScript>().ListOutScores();
		
		

	}

	private void OnDisable()
	{
		_playerListGameObject.GetComponent<PlayerListScript>().ClearOutScores();
	}

	

}
