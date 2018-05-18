using System.Collections;
using Smooth;
using UnityEngine;
using UnityEngine.Networking;
//using Smooth;

namespace Ball_Scripts
{
	public class BallMotor : NetworkBehaviour
	{
		SmoothSync _smoothSync;

		private Rigidbody _rb;
		private MeshRenderer _renderer;
		private SphereCollider _collider;
		[SerializeField] private float _thrust;
		[SyncVar] public string BallName;
		[SyncVar] public Vector3 StartPosition;

		// Use this for initialization
		void Start ()
		{
			_rb = GetComponent<Rigidbody>();
			_smoothSync = GetComponent<SmoothSync>();
			_renderer = GetComponent<MeshRenderer>();
			this.name = BallName;
			if (!GetComponent<NetworkIdentity>().isServer)
			{
				this.enabled = false;
			}

		}
		
		
		// Update is called once per frame
		void FixedUpdate () {
			if (this.transform.position.y < -1)
			{
				_renderer.enabled = false;
				StartCoroutine(Respawn());
//				Debug.Log("Respawn Ball " + StartPosition + " " + transform.rotation);
			}
			_rb.AddForce(new Vector3(0,0,-1) * _thrust);
		}
		
		private IEnumerator Respawn()
		{
			_smoothSync.stopLerping();
			yield return new WaitForSeconds(1.25f);
			_rb.position = StartPosition;
			_rb.velocity = Vector3.zero;
			_renderer.enabled = true;
			_smoothSync.restartLerping();
		}

		public void SetBallName(string ballName)
		{
			BallName = ballName;
		}
		
		
	}
}
