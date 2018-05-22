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
		[SyncVar] public string BallName;
		[SyncVar] public Vector3 StartPosition;

		private Vector3 _directionalPush;

		// Use this for initialization
		void Start()
		{
			_rb = GetComponent<Rigidbody>();
			_renderer = GetComponent<MeshRenderer>();
			this.name = BallName;
//			if (!GetComponent<NetworkIdentity>().isServer)
//			{
//				this.enabled = false;
//			}
		}


		// Update is called once per frame
		void FixedUpdate()
		{
			if (GetComponent<NetworkIdentity>().isServer)
			{
				CmdAddForce();
				if (this.transform.position.y < -1)
				{
					_renderer.enabled = false;
					CmdRespawn();
//				Debug.Log("Respawn Ball " + StartPosition + " " + transform.rotation);
				}
			}
		}

		[Command]
		void CmdRespawn()
		{
			RpcRespawnOnAllClients();
		}

		[ClientRpc]
		void RpcRespawnOnAllClients()
		{
			StartCoroutine(Respawn());
		}

		[Command]
		void CmdAddForce()
		{
			RpcAddForce();
		}

		[ClientRpc]
		void RpcAddForce()
		{
			if (_rb != null)
			{
				_rb.AddForce(new Vector3(0, 0, -1) * _thrust);
			}
		}
		
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
			yield return new WaitForSeconds(1.25f);
			_rb.position = StartPosition;
			_rb.velocity = Vector3.zero;
			_renderer.enabled = true;
		}

		public void SetBallName(string ballName)
		{
			BallName = ballName;
			
			
		}
		
		
	}
}
