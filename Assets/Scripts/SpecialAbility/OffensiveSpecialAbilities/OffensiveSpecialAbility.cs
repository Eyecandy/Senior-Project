using UnityEngine;

namespace SpecialAbility.OffensiveSpecialAbilities
{
	public abstract class OffensiveSpecialAbility : MonoBehaviour, IOffensiveSpecialAbility

	{
		public abstract void Use();

		public abstract void SetCamera(Camera camera);

	}
}
