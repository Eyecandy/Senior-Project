using UnityEngine;

namespace Player_Scripts
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(WeaponManager))]
	[RequireComponent(typeof(Player))]
	public class PlayerMotor : MonoBehaviour
	{
		
		
		[SerializeField] private Camera _camera;
		[SerializeField] private float _cameraRotationLimit = 85f;
		private Rigidbody _rigidbody;
		private Vector3 _velocity = Vector3.zero;
		private Vector3 _rotation = Vector3.zero;
		private float _cameraRotation = 0f;
		private float _currentCameraRotation = 0f;
		private WeaponManager _weaponManager;
		private Player _player;
		
		
		#region Unity InBuilt Functions
		
		private void Start()
		{
			_rigidbody = GetComponent<Rigidbody>();
			_weaponManager = GetComponent<WeaponManager>();
			_player = GetComponent<Player>();
		}
		/*
		 * Physics calculations.
		 */
		private void FixedUpdate()
		{
			PerformMovement();
			PerformRotation();
			PerformCameraRotation();
		}
		#endregion

		#region Public Functions accessed from Player Controller

		/*
		 * Takes in a velocity from PlayerController
		 * Sets player's velocity to new velocity
		 */
		public void Move(Vector3 newVelocity)
		{
			_velocity = newVelocity;
			_weaponManager.SetMoving(newVelocity != Vector3.zero);
			
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
		 * Sets new camera rotation
		 */
		public void RotateCamera(float newCameraRotation)
		{
			_cameraRotation = newCameraRotation;
		}
		#endregion

		#region Private Functions used in Fixed Update (Physics calculations)

		/*
		 * Performs movement on rigidbody, makes player walker
		 * if it has velocity. i.e keyboard is pressed (W,A,S,D)
		 */
		private void PerformMovement()
		{
			if ( _velocity != Vector3.zero )
			{
				_rigidbody.MovePosition(_rigidbody.position + _velocity * _player.WalkingSpeedPercentage * Time.deltaTime) ;
			}
		}
		/*
		 * Performs rotation on rigidbody around X axis(left and right)
		 * if it has a new rotation, i.e the mouse moved
		 */
		private void PerformRotation()
		{
			if (_rotation != Vector3.zero)
			{	//rotation is a Quatarnion, _rotation is a vector3 and turned into Quatarnion with Euler function
				_rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(_rotation));
			}
		}

		/*
		 * Performs camera rotation around Y axis(up and down)
		 */
		private void PerformCameraRotation()
		{
			if ( _camera != null )
			{
				_currentCameraRotation -= _cameraRotation;
				_currentCameraRotation = Mathf.Clamp(_currentCameraRotation, -_cameraRotationLimit, _cameraRotationLimit);
				
				//Apply to camera
				_camera.transform.localEulerAngles = new Vector3(_currentCameraRotation,0f,0f);
			}
			else
			{
				Debug.Log("PoV camera is null in Player Motor");
			}
		}
		#endregion

	}
}
