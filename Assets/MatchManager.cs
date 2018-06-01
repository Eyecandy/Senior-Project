
using UnityEngine;

public class MatchManager : MonoBehaviour
{
	[SerializeField] private float _timeLimit;

	private float _time;
	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
		if (_time < _timeLimit)
		{
			//Disconnect players
		}

		_time += Time.deltaTime;

	}
}
