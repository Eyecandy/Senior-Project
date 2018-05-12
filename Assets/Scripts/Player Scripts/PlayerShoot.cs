using SpecialAbility.OffensiveSpecialAbilities;
using UnityEngine;
using UnityEngine.Networking;
using Weapon;

    


namespace Player_Scripts
{   [RequireComponent(typeof(WeaponManager))]
    public class PlayerShoot : NetworkBehaviour
    {

        [SerializeField] private Camera _camera;
        private PlayerWeapon _weaponEquipped;

        private WeaponManager _weaponManager;
        [SerializeField] private LayerMask _layerMask;

        //[SerializeField] private PushAbility _pushAbility;
        [SerializeField] private SpecialAbilityManager _specialAbilityManager;
        

        #region Unity Functions

        private void Start()
        {
            _specialAbilityManager = GetComponent<SpecialAbilityManager>();
            _weaponManager = GetComponent<WeaponManager>();
            _weaponEquipped = _weaponManager.CurrentWeapon;
            
            if (_camera != null) return;
            Debug.LogError("No cam found in Player Shoot Script");
            enabled = false;
        }

        private void Update()
        {
            _weaponEquipped = _weaponManager.CurrentWeapon;
            
            
            if (Input.GetButtonDown("Fire1"))
            {
                FireWeapon();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                
                UseOffensiveSpecial();
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

        
        [Client] private void FireWeapon()
        {
            if (!isLocalPlayer) return;
            
            CmdOnFireWeapon();
            
            RaycastHit hit;
            
            if (!Physics.Raycast(_camera.transform.position,
                    _camera.transform.forward, //starting point of ray
                    out hit, //Raycast which info is being filled into
                    _weaponEquipped.Range, //The range of the raycast 
                    _layerMask) //masks out things we should not be able to hit.
            ) return;
            

            Debug.Log("We hit: " + hit.collider.name + "with tag:  " + hit.collider.tag);

            if (hit.collider.CompareTag("Player"))
            {
                CmdPlayerShot(hit.collider.name, _weaponEquipped.Damage);
            }

            if (hit.collider.CompareTag("PlayerHead"))
            {
                var dmg = 2 * _weaponEquipped.Damage;
                CmdPlayerShot(hit.collider.name, dmg);
            }

        }

        /*
		 * Sends out information to server regarding who has been shot and their health
         * Before it is sent to server the health of the player shot is only local to the player who shot it.
         * After the players health is synced accross all clients.
         * The Server Is the host
		 */

        [Command] private void CmdPlayerShot(string playerId, int damage)
        {
            Debug.Log(playerId + " has been shot");
            var player = GameManager.GetPlayer(playerId);
            player.RpcPlayerIsShot(damage);
        }

        /*
         * Informs server when any player fired their weapon
         */
        [Command] private void CmdOnFireWeapon()
        {
            RpcDisplayMuzzleFlash();
        }
        /*
         * Displays muzzleflash across all clients
         */
        [ClientRpc]
        private void RpcDisplayMuzzleFlash()
        {
            _weaponManager.Animator.SetTrigger("Fire");
            _weaponManager.WeaponEffectOnSHoot.Play();
            
        }

        #endregion

        #region OffensiveSpecialAbility


       [Client] private void UseOffensiveSpecial()
       {
           if (!isLocalPlayer) return;
           
           CmdUseOffensiveAbility();
       }

        [Command] private void CmdUseOffensiveAbility()
        {
            _specialAbilityManager.OffensiveSpecialAbility.Use();

        }

        #endregion
      
    }
   
}
