using System;
using Prototype.NetworkLobby;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace UI_Scripts
{
	public class CountdownTimer : NetworkBehaviour
	{

		[SerializeField]
		private Text _timerText;

		private GameObject _lobbyManager;
		public float TimeRemaining = 300f;
		public bool IsFinished;

		// Use this for initialization
		void Start ()
		{
			IsFinished = false;
			_lobbyManager = GameObject.Find("LobbyManager");
		}

		public void SetIsFinished(bool value)
		{
			IsFinished = value;
			
		}

		public void ResetTimer()
		{
			IsFinished = false;
			TimeRemaining = 300f;
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (TimeRemaining < 0)
			{
				IsFinished = true;
				_lobbyManager.GetComponent<LobbyManager>().StopHostClbk();
				return;
			}
			TimeRemaining -= Time.deltaTime;
			String minutesStr, secondsStr;
			int minutes = ((int) TimeRemaining / 60);
			float seconds = TimeRemaining % 60;
			if (minutes < 10)
			{
				minutesStr = "0" + minutes.ToString();
			}
			else
			{
				minutesStr = minutes.ToString();
			}
			if ((TimeRemaining % 60) < 10f)
			{
				secondsStr = "0" + seconds.ToString("f2");
			}
			else
			{
				secondsStr = seconds.ToString("f2");
			}
			_timerText.text = minutesStr + ":" + secondsStr;
		}
	}
}
