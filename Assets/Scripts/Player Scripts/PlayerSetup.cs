using Ball_Scripts;
using UI_Scripts;
using UnityEngine;
using UnityEngine.Networking;

namespace Player_Scripts
{	
	[RequireComponent(typeof(Player))]
	public class PlayerSetup : NetworkBehaviour {
		
		[SerializeField] private Behaviour[] _componentsToDisable; //the bevhaviors are manually placed in the script.
		
		[SerializeField] private const string RemoteLayerName = "RemotePlayer";
		
		[SerializeField] private GameObject _graphics; //all graphical components of player.
		
		private const string DontDrawLayer = "DontDraw";   
		
		[SerializeField] private GameObject _playerUiPrefab;  //canvas + crosshair prefab
		
		private GameObject _playerUiInstance;   //manually placed in script.
		
		[SerializeField] private GameObject _head;  //head of player.

		[SerializeField] private GameObject _namePlatesAndSpeedBarCanvas;
		
		
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
			
			
			var player = GetComponent<Player>();
			var playerNetId =  player.PlayerName +"-"+ GetComponent<NetworkBehaviour>().netId.ToString();
			Debug.Log("Player name : " + player.PlayerName);
			Debug.Log("PlayerSetup: Start(): Player: " + this.name + " NetId: " + playerNetId);
			GameManager.RegisterPlayer(playerNetId, player);
			
			if (!isLocalPlayer)
			{	
				Debug.Log("NOT LOCAL PLAYER");
				DisableComponents();
				AssignRemoteLayer();
			}
			else
			{
				Debug.Log("IS LOCAL PLAYER");
				GameManager.SetLocalPlayerReference(player);
				//Disable model of player in PoV camera. only done once
				SetLayerRecursively(_graphics, LayerMask.NameToLayer(DontDrawLayer));
				SetLayerRecursively(_namePlatesAndSpeedBarCanvas,LayerMask.NameToLayer(DontDrawLayer));
				//create player UI, like crosshair for example.
				_playerUiInstance = Instantiate(_playerUiPrefab);
				_playerUiInstance.name = transform.name +"GUI";
				//Link the ui to the player
				PlayerUI ui = _playerUiInstance.GetComponent<PlayerUI>();
				ui.SetPlayer(GetComponent<Player>());
				GetComponent<PlayerGUI>().HitMarker = _playerUiInstance.GetComponent<Images>().HitMarker;
			}

			if (GetComponent<NetworkIdentity>().isServer)
			{
				Debug.Log("PlayerSetup: Start(): Player is HOST.");
			}
			else
			{
				Debug.Log("PlayerSetup: Start(): Player is CLIENT.");
			}
			
		
			_head.transform.name = player.name;
			
			GetComponent<Player>().SetupPlayer();	
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
			Debug.Log("DISABLED " + name);
			Destroy(_playerUiInstance);
			if (isLocalPlayer)
				GameManager.Singleton.SetSceneCamera(true);
			
				
			//Deregister player
			GameManager.UnRegisterPlayer(transform.name);				
		}

		public void ActivateUi(bool on)
		{
			_playerUiInstance.SetActive(on);
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
