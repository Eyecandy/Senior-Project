using UnityEngine;
using UnityEngine.Networking;

namespace Player_Scripts
{
    public class PlayerShoot : NetworkBehaviour
    {
         
        [SerializeField] private Camera _camera;
        private PlayerWeapon _playerWeapon;
        [SerializeField] private GameObject _weapon;
         
        [SerializeField] private LayerMask _layerMask;

        #region Unity Functions

        private void Start()
        {
            
            _playerWeapon = _weapon.GetComponent<PlayerWeapon>();
            if (_camera != null) return;
            Debug.LogError ("No cam found in Player Shoot Script");
            enabled = false;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                FireWeapon();
            }
        }
        #endregion
        
        #region  Shooting with weapon
        /*
         * Shoot by using raycast, which is an argument which goes into Physics.RayCast Function.
         * And this function fills out information into the hit variable.
         * start of ray is camera transform position, then there is direction, fill out hit, mask
         * mask controls what we hit with layers.
       */
        
        
        private void FireWeapon()
        {
            
           
             //CmdEffect(_playerWeapon);
            
            RaycastHit hit;
            if (!Physics.Raycast(_camera.transform.position,
                _camera.transform.forward,  //starting point of ray
                out hit,                    //Raycast which info is being filled into
                _playerWeapon.Range,        //The range of the raycast 
                _layerMask)                 //masks out things we should not be able to hit.
            ) return;

             
            Debug.Log(_playerWeapon);
            _playerWeapon.Muzzleflash.Play();

           
            Debug.Log ("We hit: " + hit.collider.name + "with tag:  "+ hit.collider.tag);
            
            if (hit.collider.CompareTag("Player")) {
                Debug.Log("NONONONONO");
                CmdPlayerShot (hit.collider.name, _playerWeapon.Damage);
            }

            if (hit.collider.CompareTag("PlayerHead"))
            {
               
                var dmg = 2 * _playerWeapon.Damage;
                CmdPlayerShot (hit.collider.name, dmg);
            }

        }
       
        /*
		 * Sends out information to server regarding who has been shot and their health
         * Before it is sent to server the health of the player shot is only local to the player who shot it.
         * After the players health is synced accross all clients.
         * The Server Is the host
		 */
        
        [Command]
        private void CmdPlayerShot(string playerId,int damage) {
            
            Debug.Log (playerId + " has been shot");
            var player = GameManager.GetPlayer(playerId);
            player.RpcPlayerIsShot(damage);
            

        }

   
        #endregion
      
    }
   
}
