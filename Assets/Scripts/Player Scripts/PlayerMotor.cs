using UnityEngine;

namespace Player_Scripts
{
	[RequireComponent(typeof(Rigidbody))]
	
	public class PlayerMotor : MonoBehaviour {
		private Rigidbody _rigidbody;
		private Vector3 _velocity = Vector3.zero;
		
		private Vector3 _rotation = Vector3.zero;


		private void Start()
		{
			_rigidbody = GetComponent<Rigidbody>();
		}

		private void FixedUpdate()
		{
			PerformMovement();
			
		}

		/*
		 * Takes in a velocity from PlayerController
		 * Sets player's velocity to new velocity
		 */
		public void Move(Vector3 newVelocity)
		{
			_velocity = newVelocity;
		}
		
		/*
		 * Takes in rotation from PlayerController
		 * Sets player's rotation to new rotation
		 */
		public void Rotate(Vector3 newRotation)
		{
			_rotation = newRotation;
		}

		/*
		 * PerformsMovement on rigidbody
		 * if it has velocity
		 */
		private void PerformMovement()
		{
			if ( _velocity != Vector3.zero )
			{
				_rigidbody.MovePosition(_rigidbody.position + _velocity * Time.deltaTime) ;
			}
		}

		private void PerformRotation()
		{
			if (_rotation != Vector3.zero)
			{	//rotation is a Quatarnion
				_rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(_rotation));
			}
		}







	}
}
