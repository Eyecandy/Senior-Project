using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Player_Scripts
{
    public class Player : NetworkBehaviour
    {
        [SyncVar] public float WalkingSpeedPercentage = 100f;
        
        [SerializeField] private float _maxWalkingSpeed = 100f;

        [SyncVar] private bool _isDead; 

        [SerializeField] private Behaviour[] _disabledOnDeath; //behaviors to disable on death
       
        private bool[] _wasEnabled;
        
        [SerializeField] private float _speedGainBackTime; //time it takes to get back speed lost

        [SerializeField] private int _respawnTimer;        //time until respawn from point of death

        [SerializeField] private GameObject _povCam;

        [SerializeField] private GameObject _deathEffect;  //Death particle system prefab
       
        public GameObject Graphics; //For disabling graphics on death

        [SerializeField] private int _minimumSpeedThreshold; //movementSpeed can not go below this threshold 


        /*
        * enables component on entering game.
        */
        public void Setup()
        {
            _wasEnabled = new bool[_disabledOnDeath.Length];

            for (var i = 0; i < _wasEnabled.Length; i++)
            {
                _wasEnabled[i] = _disabledOnDeath[i].enabled;
            }
            SetPlayerDefaults();
        }

        /*
         * Tells all clients that some one has been shot and their current health.
         * If we don't do this the health of the player will only be local to the player that shot it.
         */
        [ClientRpc]
        public void RpcPlayerIsShot(int amount)
        {
            if (_isDead) return;
            var percentageReduced = (WalkingSpeedPercentage * amount / 100f);
            
            if (WalkingSpeedPercentage > _minimumSpeedThreshold)
                {
                   WalkingSpeedPercentage -= percentageReduced;
                   StartCoroutine(GainSpeedBack(percentageReduced));
                }
                Debug.Log(transform.name + "has walking speed percentage " + WalkingSpeedPercentage);
        }
        
        /*
         * Coroutine on enumerator started when player shot and his walkingspeed is above the minimum threshold
         * Player will gain back the speed lost afte a period of time (_speedGainBackTime variable decides the time).
         * 
         */
        private IEnumerator GainSpeedBack(float gainSpeedBack)
        {
            yield return new WaitForSeconds(_speedGainBackTime);
            WalkingSpeedPercentage += gainSpeedBack;
            if (WalkingSpeedPercentage > 100)
            {
                WalkingSpeedPercentage = 100;
            }
        }

        #region player lifecycle methods (local to this class)

        private IEnumerator Respawn()
        {
            yield return new WaitForSeconds(_respawnTimer);
            Debug.Log("Respawned");
            
            SetPlayerDefaults();
            var spawnPoint = NetworkManager.singleton.GetStartPosition();
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
        }

        /*
        *  Disable the components that was enabled on setup or respawn.
        */
        private void ActionsOnDeath()
        {
            
            _isDead = true;
            Graphics.SetActive(false) ;
            Debug.Log("Died");
            var weaponInstance = GetComponent<WeaponManager>().WeaponInstance;
            if (weaponInstance != null)
            {
                weaponInstance.SetActive(false);
            }

            foreach (var behaviour in _disabledOnDeath)
            {
                behaviour.enabled = false;
            }

            var collder = GetComponent<Collider>();

            if (collder != null)
            {
                collder.enabled = false;
            }

            //Spawn a death effect at players location
            GameObject _deathEffectGfx =  Instantiate(_deathEffect, transform.position, Quaternion.identity);
            Destroy(_deathEffectGfx,3f);
            
            if (isLocalPlayer)
            {
               _povCam.SetActive(false);
               
                GameManager.Singleton.ScenerCamera.SetActive(true);
               
            }

            StartCoroutine(Respawn());
        }

        /*
         * Enables components on setup and on respawn, that previously were disabled.
         */
        private void SetPlayerDefaults()
        {

            Debug.Log("SetDefaults");
            if (isLocalPlayer && _povCam != null)
            {

                _povCam.SetActive(true);
                GameManager.Singleton.SetSceneCamera(false);
            }
            
            Graphics.SetActive(true) ;
            _isDead = false;
            var weaponInstance = GetComponent<WeaponManager>().WeaponInstance;
            if (weaponInstance != null)
            {
                weaponInstance.SetActive(true);
            }

            WalkingSpeedPercentage = _maxWalkingSpeed;

            for (var i = 0; i < _disabledOnDeath.Length; i++)
            {
                _disabledOnDeath[i].enabled = _wasEnabled[i];
            }

            var collder = GetComponent<Collider>();
            if (collder != null)
            {
                collder.enabled = true;
            }
        }

        
        private void OnCollisionEnter(Collision other)
        {
            if (!other.transform.CompareTag("Ball")) return;
            // Let Server broadcast player's death
            CmdBroadcastPlayerDeath();
            
        }

        //Server broadcast player's death
        [Command] 
        private void CmdBroadcastPlayerDeath()
        {
            RpcPlayerDeath();
        }

        //Simulate player's death on all client's
        [ClientRpc]
        private void RpcPlayerDeath()
        {
            ActionsOnDeath();
        }
        #endregion
    }
}