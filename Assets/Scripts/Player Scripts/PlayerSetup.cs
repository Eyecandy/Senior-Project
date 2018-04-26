using UnityEngine;
using UnityEngine.Networking;

namespace Player_Scripts
{	
	
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
			RegisterPlayer();
			
		}
		
		/*
		 * Called when an object is destroyed
		 * In this case if a player is destroyed the scene camera is set to active
		 * This will be helpful on disconnect.
		 */
		private void OnDisable()
		{
			if (_sceneCamera != null)
			{
				_sceneCamera.gameObject.SetActive(true);
			}
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
		
		private void RegisterPlayer()
		{
			var id = "Player" + GetComponent<NetworkBehaviour>().netId;
			transform.name = id;
		}
		
	}
}
