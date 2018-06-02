
using UnityEngine;
using UnityEngine.UI;

public class PlayerEntry : MonoBehaviour
{
	public Text PlayerName;
	public Text NumberOfDeaths;

	


	public void Setup(string playerName,int numberOfDeaths)
	{
		Debug.Log("Player entry , Setup " + playerName + " " + numberOfDeaths);
		
		PlayerName.text = playerName;
		NumberOfDeaths.text = "Deaths: "+ numberOfDeaths.ToString();
	}

}
