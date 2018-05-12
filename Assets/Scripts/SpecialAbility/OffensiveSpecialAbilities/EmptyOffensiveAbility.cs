using UnityEngine;

namespace SpecialAbility.OffensiveSpecialAbilities
{
	public class EmptyOffensiveAbility : OffensiveSpecialAbility {
		public override void Use()
		{
			Debug.Log("No Ability Currently Active");
		}

		public override void  SetCamera(Camera playerPovCamera)
		{
			throw new System.NotImplementedException();
		}
	}
}
