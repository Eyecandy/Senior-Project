using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Player_Scripts
{
    public class Player : NetworkBehaviour
    {
        
        //Synced Variables
        [SyncVar] public float WalkingSpeedPercentage = 100f;
        
        [SyncVar] private bool _isDead; 
        
        //Serialized Variables
        [SerializeField] private float _maxWalkingSpeed = 100f;

        [SerializeField] private Behaviour[] _disabledOnDeath; //behaviors to disable on death
        
        [SerializeField] private GameObject[] _disableGameObjectsOnDeath;
        
        [SerializeField] private float _speedGainBackTime; //time it takes to get back speed lost

        [SerializeField] private int _respawnTimer;        //time until respawn from point of death
        
        [SerializeField] private GameObject _spawnEffect;  //Death particle system prefab

        [SerializeField] private GameObject _deathEffect;  //Death particle system prefab
        
        [SerializeField] private int _minimumSpeedThreshold; //movementSpeed can not go below this threshold

        private bool _enteredCollision;
        
        private bool[] _wasEnabled;
        
        public GameObject Graphics; //For disabling graphics on death
        
        private bool _initialSetup = true;

        public int NumberOfDeaths = 0;
        [SyncVar]
        public string PlayerName;

        public bool IsGameOver = false;

        /*
        * Enables component on entering game.
        */
        public void SetupPlayer()
        {
            // Initial Setup
            if (_initialSetup)
            {
                _wasEnabled = new bool[_disabledOnDeath.Length];
                for (var i = 0; i < _wasEnabled.Length; i++)
                {
                    _wasEnabled[i] = _disabledOnDeath[i].enabled;
                }
                _initialSetup = false;
                IsGameOver = false;
            }
            if (isLocalPlayer)
            {
                GameManager.Singleton.SetSceneCamera(false);
                CmdBroadcastPlayerSetup();
            }
        }

        private void Update()
        {
            if (!isLocalPlayer) return;
            if (isLocalPlayer)
            if (IsGameOver)
            {
                DisableUiAndSetSceneCamera();
            }
            else if (transform.position.y < 0.0 && isLocalPlayer && !_isDead) 
            {
                CmdBroadcastPlayerDeath();
            }
        }

        [Command]
        private void CmdBroadcastPlayerSetup()
        {
            RpcOnPlayerSetup();
        }

        [ClientRpc]
        private void RpcOnPlayerSetup()
        {
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
                Debug.Log("Player"+ "RpcPlayerIshot, "+transform.name + " has walking speed percentage " + WalkingSpeedPercentage);
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
            Debug.Log("Player, Respawned()");
            var spawnPoint = NetworkManager.singleton.GetStartPosition();
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
            yield return new WaitForSeconds(0.1f);
            SetupPlayer();
        }

        /*
        *  Disable the components that was enabled on setup or respawn.
        */
        private void ActionsOnDeath()
        {
            
            _isDead = true;
            NumberOfDeaths += 1;
          
            Graphics.SetActive(false) ;
            
           
            var weaponInstance = GetComponent<WeaponManager>().WeaponInstance;
            if (weaponInstance != null)
            {
                weaponInstance.SetActive(false);
            }
            
            ToggleBehavioursOnDeath(true);

            ToggleGameObjectsOnDeath(true);
            
            ToggleCollider(false);
            
            Debug.Log("Player, ActionsOnDeath()");
            GameManager.Singleton._onPlayerDeathCallBack.Invoke(name); 
            //Spawn a death effect at players location + sound
            
            GameObject deathEffectGfx =  Instantiate(_deathEffect, transform.position, Quaternion.identity);
            Destroy(deathEffectGfx,3f);
            
            
            //Deactivate ui and enable scene camera
            if (!isLocalPlayer) return;
            GetComponent<AudioSource>().Play();
            GameManager.Singleton.SetSceneCamera(true);
            GetComponent<PlayerSetup>().ActivateUi(false);
            StartCoroutine(Respawn());
        }

        /*
         * Enables components on setup and on respawn, that previously were disabled.
         */
        private void SetPlayerDefaults()
        {

            Debug.Log("Player, SetDefaults()");
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
            
            ToggleGameObjectsOnDeath(false);

            ToggleCollider(true);
            
            //Deactivate ui and enable scene camera
            if (isLocalPlayer)
            {
                GameManager.Singleton.SetSceneCamera(false);
                GetComponent<PlayerSetup>().ActivateUi(true);
            }
            
            //Create Spawn effect
            
            GameObject spawnEffectGfx =  Instantiate(_spawnEffect, transform.position, Quaternion.identity);
            Destroy(spawnEffectGfx,3f);
            _enteredCollision = false;
        }

        /*
         * Player collider with any object
         * 
         */
        [Client] private void OnCollisionEnter(Collision other)
        {
            if (!isLocalPlayer) return;
            //Check that the object is a ball otherwise return.
            if (!other.transform.CompareTag("Ball")) return;
            // Let Server broadcast player's death
            
            if (_enteredCollision) return;
            Debug.Log("Player, OnCollisionEnter, Should Enter only once per death");
            _enteredCollision = true;
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

        public float GetCurrentWalkingSpeedPercentage()
        {
            return WalkingSpeedPercentage;
        }

        private void DisableUiAndSetSceneCamera()
        {
            GetComponent<PlayerSetup>().ActivateUi(false);
            GameManager.Singleton.SetSceneCamera(true);
            Graphics.SetActive(false);
            ToggleCollider(false);
            ToggleBehavioursOnDeath(true);
            ToggleGameObjectsOnDeath(true);
        }

        public void ToggleCollider(bool isOn)
        {
            var collder = GetComponent<Collider>();
            if (collder != null)
            {
                collder.enabled = isOn;
            }
        }

        public void ToggleBehavioursOnDeath(bool isDead)
        {
            foreach (var behaviour in _disabledOnDeath)
            {
                behaviour.enabled = !isDead;
            }
        }

        public void ToggleGameObjectsOnDeath(bool isDead)
        {
            foreach (var gameobject in _disableGameObjectsOnDeath)
            {
                gameobject.SetActive(!isDead);
            }
        }
    }
}