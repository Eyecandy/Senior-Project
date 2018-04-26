
using UnityEngine.Networking;
using UnityEngine;
public class Player : NetworkBehaviour
{	
	[SerializeField] private int _maxHealth = 100;
	[SyncVar] private int _currentHealth;



	private void Awake()
	{
		SetDefaults();
	}

	public void TakeDamage(int amount)
	{
		_currentHealth -= amount;
		Debug.Log(transform.name + "has "+ _currentHealth);
		
	}

	private void SetDefaults()
	{
		
		_currentHealth = _maxHealth;
	}
	
	





}
