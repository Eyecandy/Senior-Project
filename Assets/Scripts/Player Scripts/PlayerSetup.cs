using UnityEngine;
using UnityEngine.Networking;

namespace Player_Scripts
{	
	[RequireComponent(typeof(Player))]
	public class PlayerSetup : NetworkBehaviour {
		[SerializeField] private Behaviour[] _componentsToDisable;
		[SerializeField] private const string RemoteLayerName = "RemotePlayer";
		private Camera _sceneCamera;
		
		/*
		 * Checks is we are the local player
		 * if we are not the local player we diable components
		 * components: controller, motor, camera and audiolistener
		 *
		 * if we are the local player, we set the scene camera to inactive.
		 * because we only want to deactive the scene camera for the local once.
		 * 
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
				_sceneCamera = Camera.main;
				if (_sceneCamera != null)
				{
					_sceneCamera.gameObject.SetActive(false);
				}
			}
			
			
		}
        
		public override void OnStartClient()
		{
			base.OnStartClient();
			var playerNetId =  GetComponent<NetworkBehaviour>().netId.ToString();
			var player = GetComponent<Player>();
			PlayerManager.RegisterPlayer(playerNetId, player);
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
			if (_sceneCamera != null)
			{
				_sceneCamera.gameObject.SetActive(true);
			}
			
			PlayerManager.UnRegisterPlayer(transform.name);
		}
        /*
         * Disable components on remote players
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
			gameObject.layer = LayerMask.NameToLayer(RemoteLayerName);
		}
		
		/*
		 * Gives each player an unique idea
		 */
		
		
		
	}
}
