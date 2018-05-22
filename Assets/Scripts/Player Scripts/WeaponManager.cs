using System;
using UnityEngine;
using UnityEngine.Networking;
using Weapon;


namespace Player_Scripts
{
	[RequireComponent(typeof(SpecialAbilityManager))]
	public class WeaponManager : NetworkBehaviour
	{

		
		[SerializeField] private GameObject _weaponPrefab;

		[SerializeField] private GameObject _weaponHolder;

		[HideInInspector] public PlayerWeapon CurrentWeapon;

		[HideInInspector] public ParticleSystem WeaponEffectOnSHoot;

		[HideInInspector] public Animator Animator;

		[HideInInspector] public AudioSource AudioSource;


		[HideInInspector] public Light BackwardLight;

		[HideInInspector] public Light ForwardLight;

		[HideInInspector] public ParticleSystem Glow;

		[HideInInspector] public LineRenderer LazerRenderer;
		

		/*
		 * Gets the prefab of the weapon
		 */
		private void Start()
		{
			
			EquipWeapon(_weaponPrefab.GetComponent<PlayerWeapon>());

		}
		/*
		 * Instansiate an instance of the weapon prefab.
		 * And sets animator and audio source and playerweapon.
		 * so we can use this information in the playershoot script.
		 * We set the parent to the pov camera attached to the player.
		 * 
		 */
		private void EquipWeapon(PlayerWeapon weapon)
		{
			CurrentWeapon = weapon;

			var weaponInstance = Instantiate(_weaponPrefab,
				_weaponHolder.transform.position,
				_weaponHolder.transform.rotation);
			
			weaponInstance.transform.SetParent(_weaponHolder.transform);
			WeaponEffectOnSHoot = weaponInstance.GetComponent<PlayerWeapon>().MuzzleFlash;
			Animator = weaponInstance.GetComponent<Animator>();
			AudioSource = weaponInstance.GetComponent<AudioSource>();
			
			BackwardLight = weaponInstance.GetComponent<PlayerWeapon>().BackwardLight;
			ForwardLight = weaponInstance.GetComponent<PlayerWeapon>().ForwardLight;
			LazerRenderer = weaponInstance.GetComponent<PlayerWeapon>().LazerRenderer;
			Glow = weaponInstance.GetComponent<PlayerWeapon>().LazerGlow;
			
		}

		public void SetMoving(bool isMoving)
		{
			Animator.SetBool("IsWalking",isMoving);
			
		}

		public void ChangeColor(int isPush)
		{
			if (isPush != 1)
			{
				BackwardLight.color = Color.magenta;
				ForwardLight.color = Color.magenta;
				LazerRenderer.startColor = Color.magenta;
				LazerRenderer.endColor = Color.magenta;
				#pragma warning disable 618
				Glow.startColor = Color.magenta;
				#pragma warning restore 618
			
			
			


			}
			else
			{
				BackwardLight.color = Color.green;
				ForwardLight.color = Color.green;
				LazerRenderer.startColor = Color.green;
				LazerRenderer.endColor = Color.green;
				#pragma warning disable 618
				Glow.startColor = Color.green;
				#pragma warning restore 618
			}

		}

	}
}
