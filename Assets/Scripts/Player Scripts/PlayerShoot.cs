﻿using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Weapon;

namespace Player_Scripts
{   [RequireComponent(typeof(WeaponManager))]
    [RequireComponent(typeof(PlayerGUI))]
    public class PlayerShoot : NetworkBehaviour
    {
        [SerializeField] private Camera _camera;
        
        [SerializeField] private LayerMask _layerMask;
        
        [SerializeField] private LayerMask _specialAbilityLayerMask;
        
        //current weapon equipped
        private PlayerWeapon _weaponEquipped;

        private WeaponManager _weaponManager;

        private GameObject _gunBarrelEnd;

        private float _timeToWaitForDisablingAnimation = 0.25f;

        private int _isPush = 1;
        
        private SpecialAbilityManager _specialAbilityManager;
        
        
        #region Unity Functions
        
        /*
         * Get the special Ability Manager attached to player
         * Get the WeaponManager attached to the player
         * Get the currentWeapon
         */

        private void Start()
        {
            _specialAbilityManager = GetComponent<SpecialAbilityManager>();
            _weaponManager = GetComponent<WeaponManager>();
 
            if (_camera != null) return;
            Debug.LogError("No cam found in Player Shoot Script");
            enabled = false;
        }

        private void Update()
        {
            _weaponEquipped = _weaponManager.PlayerWeaponEquipped;
            
            
            if (Input.GetButtonDown("Fire1"))
            {
                FireWeapon();
            }
            
            if (Input.GetButtonDown("Fire2"))
            {
                
                UseOffensiveSpecial(_isPush);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!isLocalPlayer) return;
                CmdChangeColorOfLaser();
             
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
            
            System.Diagnostics.Debug.Assert(_weaponEquipped != null, "_weaponEquipped != null");
            
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
                GetComponent<PlayerGUI>().SetHitMarker();

            }

            if (hit.collider.CompareTag("PlayerHead"))
            {
               
                var dmg = 2 * _weaponEquipped.Damage;
                CmdPlayerShot(hit.collider.name, dmg);
                GetComponent<PlayerGUI>().SetHitMarker();
                
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
            
            _weaponManager.WeaponAnimator.SetTrigger("Fire");
            _weaponEquipped.MuzzleFlash.Stop();
            _weaponEquipped.MuzzleFlash.Play();
            _weaponEquipped.AudioSource.Play();
           
            
        }

        #endregion

        #region OffensiveSpecialAbility

        /*
         * 
         */
        [Client] private void UseOffensiveSpecial(int isPush)
        {
            if (!isLocalPlayer) return;
            if (_specialAbilityManager.OffensiveSpecialAbility == null) return;

            CmdEnableOffensiveEffects();
            
            RaycastHit pushRaycastHit;
            if (!Physics.Raycast(_camera.transform.position,
                    _camera.transform.forward, //starting point of ray
                    out pushRaycastHit,       //Raycast which info is being filled into
                    _weaponEquipped.Range,   //The range of the raycast 
                    _specialAbilityLayerMask)              //masks out things we should not be able to hit.
            ) return;
            
            CmdUseOffensiveAbility(isPush,pushRaycastHit.collider.transform.name);
       }
        
        
        /*
         * Tell server that a player found a ball to use offensive abilty on and forward it to clients
         */
        [Command] private void CmdUseOffensiveAbility(int isPush,string ballName)
        {
            RpcUseOffecsiveAbility(isPush,ballName);
        }

         /*
          * Tell all clients that a ball has been pushed of pulled (offensive ability)
          */
        [ClientRpc] private void RpcUseOffecsiveAbility(int isPush,string ballName)
        {
            var ballMotor = GameManager.GetBallMotor(ballName);
            ballMotor._rb.AddForce( _camera.transform.forward * 100 * isPush ,ForceMode.VelocityChange);

        }
        /*
         * Tell server to enable Laser Animation on all Clients
         */
        [Command] private void CmdEnableOffensiveEffects()
        {
            RpcEnableSpecialOffensiveEffects();
        }

         /*
          * Enable effect on all clients.
          */
        [ClientRpc] private void RpcEnableSpecialOffensiveEffects()
        {
            
            _weaponEquipped.ForwardLight.enabled = true;
            _weaponEquipped.BackwardLight.enabled = true;
             var laserRenderer = _weaponEquipped.LazerRenderer;
            laserRenderer.enabled = true;
          

            _weaponEquipped.SpecialAbilityAudioSource.Play();
            _weaponEquipped.LazerGlow.Play();
            StartCoroutine(DisableAnimationForSpecialEffect());
        }
        /*
         * Tell Server to forward to all clients that a particular player has changed laser ray 
         */
        [Command] private void CmdChangeColorOfLaser()
        {
            RpcChangeColorOfLaser();
        }
        /*
         * Tell all clients that a player has changed color of laser ray
         */
        [ClientRpc]
        private void RpcChangeColorOfLaser()
        {
            _isPush *= -1;
            _weaponManager.ChangeColor(_isPush);
        }

        private IEnumerator DisableAnimationForSpecialEffect()
        {
 
            yield return new WaitForSeconds(_timeToWaitForDisablingAnimation);
            
            _weaponEquipped.ForwardLight.enabled = false;
            _weaponEquipped.BackwardLight.enabled = false;
            _weaponEquipped.LazerRenderer.enabled = false;
            _weaponEquipped.SpecialAbilityAudioSource.Stop();
            _weaponEquipped.LazerGlow.Stop();

        }
        #endregion
      
    }
   
}
