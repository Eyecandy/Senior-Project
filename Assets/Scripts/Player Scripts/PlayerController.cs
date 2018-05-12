
using UnityEngine;

namespace Player_Scripts
{
	[RequireComponent(typeof(PlayerMotor))]
	
	public class PlayerController : MonoBehaviour
	{	
		[SerializeField] private float _speed = 5f;
		[SerializeField] private float _lookSensitivity = 5f;
		private PlayerMotor _motor;

		#region Unity InBuilt Functions

		private void Start() {
			_motor = GetComponent<PlayerMotor> ();
		}
		
		
		private void Update()
		{
			MovementInput();
			CameraRotationInput();
			CameraRotation();
			Menu();
			
		}
		#endregion

		#region InputFunctions for Player Motor

	
		/*
		 * Applies Input to enable the player walk (W,S,A,D).
		 * The input is sent to Player Motor.
		 */
		private void MovementInput()
		{
			var xMove = Input.GetAxis("Horizontal");
			var yMove = Input.GetAxis("Vertical");
			var moveHorizontal = transform.right * xMove;  
			var moveVertical = transform.forward * yMove; 
			var velocity = (moveHorizontal + moveVertical).normalized* _speed; 
			_motor.Move(velocity);
		}
		/*
		 * Applies Input to enable player to turn around.
		 * We are turning around the Y axis based on the mouse positiion of X.
		 */
		private void CameraRotationInput()
		{
			var rotationAroundY = Input.GetAxis ("Mouse X");
			var rotation = new Vector3 (0f, rotationAroundY, 0f) * _lookSensitivity;
			_motor.Rotate(rotation);
		}
		/*
		 * Applies Input to tilt camera up and down.
		 */
		private void CameraRotation()
		{
			var cameraRotationAroundX = Input.GetAxis ("Mouse Y");
			var rotation = new Vector3 (cameraRotationAroundX, 0f, 0f) * _lookSensitivity;
			_motor.RotateCamera(rotation);
		}
		#endregion
		
		
		
		#region GUI
		/*
		 * Controls GUI.
		 */
		private static void Menu()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				PlayerGUI.EnableNetworkManagerHud();
			}
		}
		#endregion
		
		


	}
}
