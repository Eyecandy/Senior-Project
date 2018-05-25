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
		
		[SerializeField] private string _weaponLayerName = "Weapon";

		[HideInInspector] public PlayerWeapon PlayerWeaponEquipped;
		
		[HideInInspector] public Animator WeaponAnimator;

		[HideInInspector] public GameObject WeaponInstance;

		/*
		 * Gets the prefab of the weapon
		 */
		private void Start()
		{
			EquipWeapon();
		}
		
		/*
		 * Instansiate an instance of the weapon prefab.
		 * And sets animator and audio source and playerweapon.
		 * so we can use this information in the playershoot script.
		 * We set the parent to the pov camera attached to the player.
		 * 
		 */
		private void EquipWeapon()
		{
			// Instantiate Weapon in WeaponHolder position
			var weaponInstance = Instantiate(_weaponPrefab,
				_weaponHolder.transform.position,
				_weaponHolder.transform.rotation);
			
			WeaponInstance = weaponInstance;
			weaponInstance.transform.SetParent(_weaponHolder.transform);
			PlayerWeaponEquipped = weaponInstance.GetComponent<PlayerWeapon>();
			Debug.Log("isLocalPlayer: " + isLocalPlayer);
			if (isLocalPlayer)
			{
				Debug.Log("Set weapon layer");
				//Set WeaponLayerName to Weapon
				WeaponInstance.layer = LayerMask.NameToLayer(_weaponLayerName);
				//Set Child of Weapon Layer to "Weapon" so that Muzzle Flash is aligned with the  FOV.
				Util.SetLayerRecursively(WeaponInstance,LayerMask.NameToLayer(_weaponLayerName));
			}
			WeaponAnimator = PlayerWeaponEquipped.Animator;

		}

		// Animate weapon if player is moving
		public void SetMoving(bool isMoving)
		{
			WeaponAnimator.SetBool("IsWalking",isMoving);
		}

		//Change color depending on push/pull
		public void ChangeColor(int isPush)
		{
			if (isPush != 1)
			{
				PlayerWeaponEquipped.BackwardLight.color = Color.magenta;
				PlayerWeaponEquipped.ForwardLight.color = Color.magenta;
				PlayerWeaponEquipped.LazerRenderer.startColor = Color.magenta;
				PlayerWeaponEquipped.LazerRenderer.endColor = Color.magenta;
				#pragma warning disable 618
				PlayerWeaponEquipped.LazerGlow.startColor = Color.magenta;
				#pragma warning restore 618

			}
			else
			{
				PlayerWeaponEquipped.BackwardLight.color = Color.green;
				PlayerWeaponEquipped.ForwardLight.color = Color.green;
				PlayerWeaponEquipped.LazerRenderer.startColor = Color.green;
				PlayerWeaponEquipped.LazerRenderer.endColor = Color.green;
				#pragma warning disable 618
				PlayerWeaponEquipped.LazerGlow.startColor = Color.green;
				#pragma warning restore 618
			}

		}

	}
}
