using Ball_Scripts;
using UnityEngine;
using UnityEngine.Networking;

namespace Player_Scripts
{	
	[RequireComponent(typeof(Player))]
	public class PlayerSetup : NetworkBehaviour {
		
		[SerializeField] private Behaviour[] _componentsToDisable; //the bevhaviors are manually placed in the script.
		
		[SerializeField] private const string RemoteLayerName = "RemotePlayer";
		
		public Camera _povCamera; //our PoV Cam
		
		[SerializeField] private GameObject _graphics; //all graphical components of player.
		
		private const string DontDrawLayer = "DontDraw";   
		
		[SerializeField] private GameObject _playerUiPrefab;  //canvas + crosshair prefab
		
		private GameObject _playerUiInstance;   //manually placed in script.
		
		[SerializeField] private GameObject _head;  //head of player.
		
		
		#region Unity Functions

		/*
		 * Checks is we are the local player
		 * if we are not the local player we diable components
		 * components: controller, motor, camera and audiolistener
		 * Obviously the player is always local to themselves, so this is to avoid a player
		 * from controlling multiple players with ASWD or mouse
		 *
		 * if we are the local player, we set the scene camera to inactive.
		 * because we only want to deactive the scene camera for the local player once.
		 *
		 * Calls Setup on Player script.
		 */
		private void Start()
		{
			if (!isLocalPlayer)
			{	
				DisableComponents();
				AssignRemoteLayer();
			}
			else
			{
				_povCamera = Camera.main;
				if (_povCamera != null)
				{
					_povCamera.gameObject.SetActive(false);
				}
				//Disable model of player in PoV camera. only done once
				SetLayerRecursively(_graphics, LayerMask.NameToLayer(DontDrawLayer));
			}
			
			_head.transform.name = transform.name;
			
			GetComponent<Player>().Setup();
			
			
			//create player UI, like crosshair for example.
			_playerUiInstance = Instantiate(_playerUiPrefab);
			//to remove clone.
			_playerUiInstance.name = transform.name + "GUI";
		}

		
		/*
		 * Called when client connects by unity.
		 */
		public override void OnStartClient() 
		{
			base.OnStartClient();
			var playerNetId =  GetComponent<NetworkBehaviour>().netId.ToString();
			var player = GetComponent<Player>();
			GameManager.RegisterPlayer(playerNetId, player);
			var arrayOfBalls = GameObject.FindGameObjectsWithTag("Ball");
			foreach (var ball in arrayOfBalls)
			{
				var ballName = ball.GetComponent<BallMotor>().BallName;
				Debug.Log(ballName);
				var ballMotor = ball.GetComponent<BallMotor>();
				GameManager.RegisterBallMotor(ballName,ballMotor);
			}
		}

		/*
		 * Called when an object is destroyed
		 * In this case if a player is destroyed the scene camera is set to active
		 * This will be helpful on disconnect.
		 *
		 * And we get the players name and we UnRegister the player
		 */
		private void OnDisable()
		{
			if (_povCamera != null)
			{
				_povCamera.gameObject.SetActive(true);
			}
			Destroy(_playerUiInstance);
			GameManager.UnRegisterPlayer(transform.name);
			
		}
		#endregion
		
		
		#region Functions called within class
        /*
         * Disable components on remote players
         * these component can be viewed in the player in unity.
         * They are manually attached to the array.
         */
		private void DisableComponents()
		{
			foreach (var component in _componentsToDisable)
			{
				component.enabled = false;
			}
			
		}
		
		/*
		 * Set the layer of the player to remote
		 */
		private void AssignRemoteLayer()
		{
			SetLayerRecursively(gameObject,LayerMask.NameToLayer(RemoteLayerName));
		}
		
		/*
		 * Called if we are local player
		 * Make our pov camera not draw the our own player body.
		 * as our body is marked with DontDraw layer.
		 * And our pov camera has a cullingmask which exclude DontDraw.
		 */
		private static void SetLayerRecursively(GameObject graphics, int layer)

		{
			graphics.layer = layer;
			foreach (Transform child in graphics.transform)
			{
				SetLayerRecursively(child.gameObject , layer);
			}
		}
		#endregion
		
	}
}
