using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;
using Smooth;

namespace Ball_Scripts
{
	public class BallMotor : NetworkBehaviour
	{
		SmoothSync _smoothSync;

		private Rigidbody _rb;
		private float _time;
		[SerializeField] private float _thrust;
		[SyncVar] public string BallName;
		[SyncVar] public Vector3 StartPosition;

		// Use this for initialization
		void Start ()
		{
			_rb = GetComponent<Rigidbody>();
			_smoothSync = GetComponent<SmoothSync>();
			this.name = BallName;

		}
		
		
	
		
		
		// Update is called once per frame
		void FixedUpdate () {
			if (this.transform.position.y < -1)
			{
				this.transform.position = StartPosition;
//				Debug.Log("Respawn Ball " + StartPosition + " " + transform.rotation);
				//Network timestamp
				int timestamp = NetworkTransport.GetNetworkTimestamp();
				// Teleport owned object
//				_smoothSync.teleport(timestamp, StartPosition, transform.rotation);
				_rb.velocity = Vector3.zero;
			}
			
			_time += Time.deltaTime;
			if (Math.Abs(_time % 2.5) < 1)
			{
				_rb.AddForce(new Vector3(0,0,-1) * _thrust, ForceMode.VelocityChange);
			}

			
		}

		public void SetBallName(string ballName)
		{
			BallName = ballName;
		}
	}
}
