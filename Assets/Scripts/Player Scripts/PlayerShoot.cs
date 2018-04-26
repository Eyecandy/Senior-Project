using UnityEngine;
using UnityEngine.Networking;

namespace Player_Scripts
{
    public class PlayerShoot : NetworkBehaviour
    {
         
        [SerializeField] private Camera _camera;
        [SerializeField] private PlayerWeapon _playerWeapon;
        [SerializeField] private LayerMask _layerMask;
		
        private void Start()
        {
            if (_camera != null) return;
            Debug.LogError ("No cam found in Player Shoot Script");
            enabled = false;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        
        /*
         * Shoot by using raycast, which is an argument which goes into Physics.RayCast Function.
         * And this function fills out information into the hit variable.
         * start of ray is camera transform position, then there is direction, fill out hit, mask
         *mask controls what we hit with layers.
       */
        private void Shoot()
        {
            
            RaycastHit hit;
            if (!Physics.Raycast(_camera.transform.position,
                _camera.transform.forward,  //starting point of ray
                out hit,                    //Raycast which info is being filled into
                _playerWeapon.Range,        //The range of the raycast 
                _layerMask)                 //masks out things we should not be able to hit.
            ) return;                       
            
            
            Debug.Log ("We hit: " + hit.collider.name + "with tag:  "+ hit.collider.tag);
            
            if (hit.collider.CompareTag("Player")) {
                
                CmdPlayerShot (hit.collider.name, _playerWeapon.Damage);
            }
        }
       
        /*
		 * Sends out information to server of who has been shot
		 */
        
        [Command]
        private void CmdPlayerShot(string playerId,int damage) {
		
            Debug.Log (playerId + " has been shot");
		
        }
        
       
    }
   
}
