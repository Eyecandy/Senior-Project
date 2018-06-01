using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListScript : MonoBehaviour
{

	[SerializeField] private GameObject _individualPlayerEntryPrefab;
	
	
	
	public void ListOutScores()
	{
		Debug.Log("LIST OUT SCORES CALLED");
		foreach (var player in GameManager.GetAllPlayers())
		{
			
			var playerVal = player.Value;
			var playerScoreInstance = Instantiate(_individualPlayerEntryPrefab, transform);
			playerScoreInstance.transform.SetParent(transform);
			var individualPlayerScoreScript = playerScoreInstance.GetComponent<PlayerEntry>();
			individualPlayerScoreScript.Setup(playerVal.name,playerVal.NumberOfDeaths);
			
					
		}

	}

	public void ClearOutScores()
	{
		foreach (Transform child in this.transform )
		{
			Destroy(child.gameObject);
		}
	}




}
