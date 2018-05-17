using UnityEngine;

namespace SpecialAbility.OffensiveSpecialAbilities
{
	/*
	 * Abstract class so we can use polymorphism
	 * It implements the offensive special abilty interface
	 * Every offensive ability will extend this class
	 */
	public abstract class OffensiveSpecialAbility : MonoBehaviour, IOffensiveSpecialAbility

	{
		public abstract void Use(int isPush);

		public abstract void SetCamera(Camera camera);


	}
}
