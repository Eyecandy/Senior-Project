using Ball_Scripts;
using UnityEngine;

namespace SpecialAbility.OffensiveSpecialAbilities
{
	public class PushAbility : OffensiveSpecialAbility
	{

		[SerializeField] private Camera _camera;
		[SerializeField] private float _range = 100f;
		[SerializeField] private float _pushForce = 100f;
		[SerializeField] private LayerMask _layerMask;
		
		

		/*
		 * Interface Function from OffensiveSpecialAbility (overwritten abstract method)
		 */
		public override void Use(int isPush)
		{
			PerformPush(isPush);
		}

		public override void SetCamera(Camera playerPovCamera)
		{
			_camera = playerPovCamera;
		}

		/*
		 * Performs push
		 */
		private void PerformPush(int isPush)
		{
			RaycastHit pushRaycastHit;
			if (!Physics.SphereCast(_camera.transform.position,1f,
					_camera.transform.forward,    //starting point of ray
					out pushRaycastHit,           //Raycast which info is being filled into
					_range,                       //The range of the raycast 
					_layerMask)                   //masks out things we should not be able to hit.
			) return;

			
			var ballRigidBody = pushRaycastHit.collider.attachedRigidbody;
			ballRigidBody.AddForce( _camera.transform.forward * _pushForce * isPush ,ForceMode.VelocityChange);
		}

	
	}
}
