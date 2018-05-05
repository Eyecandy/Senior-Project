using UnityEngine;

namespace Weapon
{	[System.Serializable]
	[RequireComponent(typeof(Animator))]
	public class PlayerWeapon:MonoBehaviour
	{
		public float Range = 100f;
		public int Damage = 25;
		//public Animator OnFireWeaponAnimation;
		[HideInInspector]public Animator Animator;
		
		public ParticleSystem MuzzleFlash;

		private void Start()
		{
			Animator = GetComponent<Animator>();
		}
	}
}
