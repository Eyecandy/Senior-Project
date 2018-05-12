using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;
using Smooth;

namespace Ball_Scripts
{
	public class BallMotor : NetworkBehaviour
	{
		SmoothSync smoothSync;

		private Rigidbody _rb;
		private float _time;
		[SerializeField] private float _thrust;
		public Vector3 StartPosition = Vector3.zero;

		// Use this for initialization
		void Start ()
		{
			_rb = GetComponent<Rigidbody>();
			smoothSync = GetComponent<SmoothSync>();
		}
		
		
	
		
		
		// Update is called once per frame
		void FixedUpdate () {
			if (this.transform.position.y < -1)
			{
				//this.transform.position = StartPosition;
				Debug.Log("Respawn Ball " + StartPosition + " " + transform.rotation);
				//Network timestamp
				int timestamp = NetworkTransport.GetNetworkTimestamp();
				// Teleport owned object
				smoothSync.teleport(timestamp, StartPosition, transform.rotation);
				_rb.velocity = Vector3.zero;
			}
			
			_time += Time.deltaTime;
			if (Math.Abs(_time % 2.5) < 1)
			{
				_rb.AddForce(new Vector3(0,0,-1) * _thrust, ForceMode.VelocityChange);
			}

			
		}
	}
}
