using UnityEngine;

namespace Player_Scripts
{
	[RequireComponent(typeof(Rigidbody))]
	
	public class PlayerMotor : MonoBehaviour
	{
		[SerializeField] private Camera _camera;
		private Rigidbody _rigidbody;
		private Vector3 _velocity = Vector3.zero;
		private Vector3 _rotation = Vector3.zero;
		private Vector3 _cameraRotation = Vector3.zero;
		

		private void Start()
		{
			_rigidbody = GetComponent<Rigidbody>();
			
		}

		private void FixedUpdate()
		{
			PerformMovement();
			PerformRotation();
			PerformCameraRotation();
			
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


		public void RotateCamera(Vector3 newCameraRotation)
		{
			_cameraRotation = newCameraRotation;
		}

		/*
		 * Performs movement on rigidbody, makes player walker
		 * if it has velocity. i.e keyboard is pressed (w,A,S,D)
		 */
		private void PerformMovement()
		{
			if ( _velocity != Vector3.zero )
			{
				_rigidbody.MovePosition(_rigidbody.position + _velocity * Time.deltaTime) ;
			}
		}
		/*
		 * Performs rotation on rigidbody
		 * if it has a new rotation, i.e the mouse moved 
		 */
		private void PerformRotation()
		{
			if (_rotation != Vector3.zero)
			{	//rotation is a Quatarnion, _rotation is a vector3 and turned into Quatarnion with Euler function
				_rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(_rotation));
			}
		}

		private void PerformCameraRotation()
		{
			if (_camera != null)
			{
				_camera.transform.Rotate(-_cameraRotation);
			}
		}

	}
}
