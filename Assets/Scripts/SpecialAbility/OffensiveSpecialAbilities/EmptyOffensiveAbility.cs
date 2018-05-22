using UnityEngine;

namespace SpecialAbility.OffensiveSpecialAbilities
{
	public class EmptyOffensiveAbility : OffensiveSpecialAbility
	{

		/*
		 * Place holder for offensive ability, so we never have to check if it is null before
		 * perfoming an action.
		 */
		public override void Use(int isPush)
		{
			Debug.Log("No Ability Currently Active");
		}

		public override void SetCamera(Camera playerPovCamera)
		{
			throw new System.NotImplementedException();
		}

		

	}
}
