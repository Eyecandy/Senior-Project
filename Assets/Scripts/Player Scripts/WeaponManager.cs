using UnityEngine;
using UnityEngine.Networking;
using Weapon;

namespace Player_Scripts
{
	public class WeaponManager : NetworkBehaviour
	{

		[SerializeField] private PlayerWeapon _playerWeapon;

		[SerializeField] private GameObject _weaponHolder;
		private WeaponGraphicalEffect _weaponGraphicsScript;

		[HideInInspector] public PlayerWeapon CurrentWeapon;

		[HideInInspector] public WeaponGraphicalEffect WeaponGraphicalEffect;


		private void Start()
		{
			EquipWeapon(_playerWeapon);
			
		}

		private void EquipWeapon(PlayerWeapon weapon)
		{
			CurrentWeapon = weapon;

			var weaponInstance = Instantiate(CurrentWeapon.WeaponPrefab,
				_weaponHolder.transform.position,
				_weaponHolder.transform.rotation);
			
			weaponInstance.transform.SetParent(_weaponHolder.transform);
			WeaponGraphicalEffect = WeaponGraphicalEffect = weaponInstance.GetComponent<WeaponGraphicalEffect>();
			
		}
		
		

		
			
		
	}
}
