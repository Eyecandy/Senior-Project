using System;
using UnityEngine;
using UnityEngine.Networking;

public class BallMotor : NetworkBehaviour
{

	private Rigidbody rb;
	[SerializeField] private float thrust;
	private Vector3 startPosition;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		GameObject spawner = GameObject.FindGameObjectWithTag("Spawner");
		BallManager manager = spawner.GetComponent<BallManager>();
		int ballNumber = Int32.Parse(this.name.Split('-')[1]);
		startPosition = manager.positions[ballNumber];
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (this.transform.position.y < -1)
		{
			this.transform.position = startPosition;
			rb.velocity = Vector3.zero;
		}
		rb.AddForce(new Vector3(0,0,-1) * thrust);
	}
}
