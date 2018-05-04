using UnityEngine;

namespace Weapon
{	[System.Serializable]
	public class PlayerWeapon:MonoBehaviour
	{
		public float Range = 100f;
		public int Damage = 25;
		public ParticleSystem MuzzleFlash;
	}
}
