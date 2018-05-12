using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

namespace Ball_Scripts
{
	public class BallMotor : NetworkBehaviour
	{

		private Rigidbody _rb;
		private float _time;
		[SerializeField] private float _thrust;
		public Vector3 StartPosition;

		// Use this for initialization
		void Start ()
		{
			_rb = GetComponent<Rigidbody>();
		}
		
		
	
		
		
		// Update is called once per frame
		void FixedUpdate () {
			if (this.transform.position.y < -1)
			{
				this.transform.position = StartPosition;
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
