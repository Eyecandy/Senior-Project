using System;
using UnityEngine;
using UnityEngine.Networking;
using Weapon;


namespace Player_Scripts
{
	public class WeaponManager : NetworkBehaviour
	{

		
		[SerializeField] private GameObject _weaponPrefab;

		[SerializeField] private GameObject _weaponHolder;

		[HideInInspector] public PlayerWeapon CurrentWeapon;

		[HideInInspector] public ParticleSystem WeaponEffectOnSHoot;

		[HideInInspector] public Animator Animator;


		[HideInInspector] public AudioSource AudioSource;
		
		
	
		private void Start()
		{
			EquipWeapon(_weaponPrefab.GetComponent<PlayerWeapon>());
		}

		private void EquipWeapon(PlayerWeapon weapon)
		{
			CurrentWeapon = weapon;

			var weaponInstance = Instantiate(_weaponPrefab,
				_weaponHolder.transform.position,
				_weaponHolder.transform.rotation);
			
			weaponInstance.transform.SetParent(_weaponHolder.transform);
			WeaponEffectOnSHoot = weaponInstance.GetComponent<PlayerWeapon>().MuzzleFlash;
			Animator = weaponInstance.GetComponent<PlayerWeapon>().Animator;
			AudioSource = weaponInstance.GetComponent<AudioSource>();

		}

		public void SetMoving(bool isMoving)
		{
			Animator.SetBool("IsWalking",isMoving);
			
		}
	}
}
