using OffensiveSpecialAbilities;
using UnityEngine;

namespace SpecialAbility.OffensiveSpecialAbilities
{
	public class PushAbility : OffensiveSpecialAbility
	{

		[SerializeField] private Camera _camera;
		[SerializeField] private float _range = 100f;
		[SerializeField] private float _pushForce = 400f;
		[SerializeField] private LayerMask _layerMask;
		
		public void Use()
		{
			PerformPush();
		}

		private void PerformPush()
		{
			
			RaycastHit pushRaycastHit;
			if (!Physics.Raycast(_camera.transform.position,
					_camera.transform.forward,    //starting point of ray
					out pushRaycastHit,           //Raycast which info is being filled into
					_range,                       //The range of the raycast 
					_layerMask)                   //masks out things we should not be able to hit.
			) return;
			var ballRigidBody = pushRaycastHit.collider.attachedRigidbody;

			ballRigidBody.AddForce( _camera.transform.forward * _pushForce );
		}
		
		
	}
}
