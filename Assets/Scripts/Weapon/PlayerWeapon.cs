using UnityEngine;

namespace Weapon
{	[System.Serializable]
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(AudioSource))]
	
	public class PlayerWeapon:MonoBehaviour
	{
		
		public float Range = 100f;
		public int Damage = 25;
		
		[HideInInspector] public Animator Animator;
		
		public ParticleSystem MuzzleFlash;

		public Light BackwardLight;

		public Light ForwardLight;

		public LineRenderer LazerRenderer;

		public ParticleSystem LazerGlow;
			
			
		[HideInInspector] public AudioSource AudioSource;

		[HideInInspector] public GameObject SpecialAbilityAnimatorManagerGameObject;

	     public AudioSource SpecialAbilityAudioSource;
		

		private void Start()
		{
			Animator = GetComponent<Animator>();
			AudioSource = GetComponent<AudioSource>();

		}
	}
}
