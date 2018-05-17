using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Player_Scripts
{
    public class Player : NetworkBehaviour
    {
        [SyncVar] public float WalkingSpeedPercentage = 100f;
       private float _maxWalkingSpeed = 100f;

        [SyncVar] private bool _isDead = false;

        [SerializeField] private Behaviour[] _disabledOnDeath;
        private bool[] _wasEnabled;

        [SerializeField] private int _respawnTimer = 100;

        public GameObject Graphics;


        /*
        * enables component on entering game.
        * 
        */
        public void Setup(Camera poVCamera)
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
            if (WalkingSpeedPercentage -  percentageReduced<= 0)
            {
                Debug.LogError("PLAYER DIED FROM SHOTS, THIS SHOULD BE IMPOSSIBLE, KILLING PLAYER THO");
                ActionsOnDeath();
            }
            else
            {
                WalkingSpeedPercentage= WalkingSpeedPercentage - percentageReduced;
                Debug.Log(transform.name + "has walking speed percentage " + WalkingSpeedPercentage);
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

            foreach (var behaviour in _disabledOnDeath)
            {
                behaviour.enabled = false;
            }

            var collder = GetComponent<Collider>();

            if (collder != null)
            {
                collder.enabled = false;
            }
            

            StartCoroutine(Respawn());
        }

        /*
     * Enables components on setup and on respawn, that previously were disabled.
     */
        private void SetPlayerDefaults()
        {

            Debug.Log("SetDefaults");
            Graphics.SetActive(true) ;
            _isDead = false;
            

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
            ActionsOnDeath();
        }

        

        #endregion
    }
}