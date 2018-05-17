using UnityEngine;

namespace Weapon
{	[System.Serializable]
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(AudioSource))]
	public class PlayerWeapon:MonoBehaviour
	{
		public float Range = 100f;
		public int Damage = 25;
		
		[HideInInspector]public Animator Animator;
		
		public ParticleSystem MuzzleFlash;

		[HideInInspector] public AudioSource AudioSource;
		

		private void Start()
		{
			Animator = GetComponent<Animator>();
			AudioSource = GetComponent<AudioSource>();
			
		}
	}
}
