using System.Collections;
using Smooth;
using UnityEngine;
using UnityEngine.Networking;
//using Smooth;

namespace Ball_Scripts
{
	public class BallMotor : NetworkBehaviour
	{
		public Rigidbody _rb;
		private MeshRenderer _renderer;
		private SphereCollider _collider;
		[SerializeField] private float _thrust;
		[SerializeField] private Transform _plane;
		[SyncVar] public string BallName;
		[SyncVar] public Vector3 StartPosition;
//		private Vector3 _directionalPush;
		[SyncVar] private int _currentSide = 0;
		private bool _isFalling = false;

		private Vector3[] _sideForce = new[] {
			new Vector3(0, 0, -1), //Side 0
			new Vector3(1, 0, 0),  //Side 1
			new Vector3(-1, 0, 0), //Side 2
		};
		private float _planeSide;
		
		public void SetBallName(string ballName)
		{
			BallName = ballName;
		}

		// Use this for initialization
		void Start()
		{
			_rb = GetComponent<Rigidbody>();
			_renderer = GetComponent<MeshRenderer>();
			this.name = BallName;
			GameManager.RegisterBallMotor(BallName,this);
			_planeSide = (_plane.localScale.x / 2)*10;
		}


		// Update is called once per frame
		[ServerCallback]
		void FixedUpdate()
		{
			if (!_isFalling)
			{
				_rb.AddForce(_sideForce[_currentSide] * _thrust);
			}
			if (transform.position.y < -1 && !_isFalling)
			{
				_isFalling = true;
//				Debug.Log("Server Ball Respawn");
				_renderer.enabled = false;
				StartPosition = RandomizeStart();
				StartCoroutine(Respawn());
//				CmdRespawn();
			}
		}

//		[Command]
//		void CmdRespawn()
//		{
//			Debug.Log("CMD Respawn");
//			RpcRespawnOnAllClients();
//		}
//
//		[ClientRpc]
//		void RpcRespawnOnAllClients()
//		{
//			Debug.Log("RPC Respawn");
//			StartCoroutine(Respawn());
//		}

//		[Command]
//		void CmdAddForce()
//		{
//			RpcAddForce();
//		}

//		[ClientRpc]
//		void RpcAddForce()
//		{
//			if (_rb != null)
//			{
//				_rb.AddForce(_sideForce[_currentSide] * _thrust);
//			}
//		}
		
		/*

		public void PlayerAddForceToBall(int isPush, Vector3 direction, float force)
		{
			_directionalPush = direction;
			CmdPlayerAddForceToBall(isPush,force);

		}

		[Command]
		private void CmdPlayerAddForceToBall(int isPush,float force)
		{
			RpcPlayerAddForceToBall(isPush,force);
		}

		[ClientRpc]
		private void RpcPlayerAddForceToBall(int isPush,float force)
		{
			_rb.AddForce( _directionalPush* force * isPush ,ForceMode.VelocityChange);
		}
		*/

		private IEnumerator Respawn()
		{
			// TO DO: Randomize Respawn Time
			yield return new WaitForSeconds(1f);
			_rb.position = StartPosition;
			_rb.rotation = Quaternion.identity;
			_rb.velocity = Vector3.zero;
			yield return new WaitForSeconds(0.45f);
			_renderer.enabled = true;
			_isFalling = false;
		}

		private Vector3 RandomizeStart()
		{
			var side = Random.Range(0, 3);
			var location = Random.Range(-_planeSide, _planeSide);
			_currentSide = side;
			if (side == 0){
//				Debug.Log("SIDE " + side);
				return new Vector3(location,0f,_planeSide-2.2f);
			} else if (side == 1){
//				Debug.Log("SIDE " + side);
				return new Vector3(-_planeSide+1.78f,0f,location);
			} else{
//				Debug.Log("SIDE " + side);
				return new Vector3(_planeSide-1.78f,0f,location);
			}
		}
	}
}
