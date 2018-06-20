using System;
using Player_Scripts;
using Prototype.NetworkLobby;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using PlayerController = UnityEngine.Networking.PlayerController;

namespace UI_Scripts
{
	public class CountdownTimer : NetworkBehaviour
	{

		[SerializeField]
		private Text _timerText;
		
		[SerializeField]
		private Text _gameOverText;
		
		[SerializeField] 
		private float _gameOverTimeRemaining = 15f;

		private GameObject _lobbyManager;

		private bool _scriptsEnabled;
		
		[SyncVar] public float TimeRemaining = 20f;


		// Use this for initialization
		void Start ()
		{
			_scriptsEnabled = true;
			_lobbyManager = GameObject.Find("LobbyManager");
			// gameOverText enabled when match time is over
			_gameOverText.enabled = false;
			
		}
		
		// Update is called once per frame
		void Update ()
		{
			if (TimeRemaining < 0)
			{
				DisablePlayerScripts();
				CountDownGameOver();
				if (_gameOverTimeRemaining < 0)
				{	
					_lobbyManager.GetComponent<LobbyManager>().StopHostClbk();
					enabled = false;
				}
			}
			else
			{
				CountdownGame();
			}
		}

		// Decrement In Match Timer
		private void CountdownGame()
		{
			TimeRemaining -= Time.deltaTime;
			int minutes = ((int) TimeRemaining / 60);
			float seconds = TimeRemaining % 60;
			_timerText.text = FormatTimeString(minutes, seconds);
		}

		// Decrement Game Over Timer
		private void CountDownGameOver()
		{
			_gameOverTimeRemaining -= Time.deltaTime;
			float gameOverseconds = _gameOverTimeRemaining % 60;
			_gameOverText.text = gameOverseconds.ToString("f0");
		}

		//Format Time
		private String FormatTimeString(int minutes, float seconds)
		{
			String minutesStr, secondsStr;
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
			
			return minutesStr + ":" + secondsStr;
		}
		
		// Disable Player Scripts when Game is over
		private void DisablePlayerScripts()
		{
			if (_scriptsEnabled)
			{
				GameManager.Singleton.IsGameOver = true;
				
				GameManager.GetLocalPlayerReference().DisableUiAndSetSceneCamera();
				GameManager.GetLocalPlayerReference().GetComponent<PlayerSetup>().ActivateUi(true);
				GameManager.DisableAllPlayers();
				_scriptsEnabled = false;
				_timerText.enabled = false;
				_gameOverText.enabled = true;
			}
		}
	}
}
