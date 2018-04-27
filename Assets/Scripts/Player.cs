
using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
public class Player : NetworkBehaviour
{	
	[SerializeField] private int _maxHealth = 100;
	[SyncVar] private int _currentHealth;
	
	[SyncVar] private bool _isDead = false;
	
	[SerializeField] private Behaviour[] _disabledOnDeath;
	private bool[] _wasEnabled;

	[SerializeField] private int _respawnTimer = 1;


	public void Setup()
	{
		
		_wasEnabled = new bool[_disabledOnDeath.Length];
		
		_wasEnabled = new bool[_disabledOnDeath.Length];
		for (var i = 0; i < _wasEnabled.Length; i++) {
			_wasEnabled [i] = _disabledOnDeath [i].enabled;
		}
		SetPlayerDefaults ();
		
	}
	
	
	[ClientRpc]
	public void RpcTakeDamage(int amount)
	{
		if (_isDead) return;
		if (_currentHealth - amount <= 0)
		{
			ActionsOnDeath();
		}
		else
		{
			_currentHealth -= amount;
			Debug.Log(transform.name + "has "+ _currentHealth);
		}
	
	}
	
	
	private IEnumerator Respawn()
	{
		yield return new WaitForSeconds(_respawnTimer);
		Debug.Log("Respawned");
		SetPlayerDefaults ();
		var spawnPoint = NetworkManager.singleton.GetStartPosition ();
		transform.position = spawnPoint.position;
		transform.rotation = spawnPoint.rotation;
		
	}

	private void ActionsOnDeath()
	{
		
		_isDead = true;
		Debug.Log("Died");
		
		foreach (var behaviour in _disabledOnDeath)
		{
			behaviour.enabled = false;
		}

		var collder = GetComponent<Collider> ();
		
		if (collder != null) {
			collder.enabled = false;
		}
		
		StartCoroutine (Respawn ());
		
		
	}

	private void SetPlayerDefaults()
	{
		
		Debug.Log ("SetDefaults");
		_isDead = false;

		_currentHealth = _maxHealth;

		for (var i = 0; i < _disabledOnDeath.Length; i++) {
			_disabledOnDeath [i].enabled = _wasEnabled [i];
		}

		var collder = GetComponent<Collider> ();
		if (collder != null) {
			collder.enabled = true;
		}
		
		
	}
	
}
