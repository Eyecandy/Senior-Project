using UnityEngine;

namespace SpecialAbility.OffensiveSpecialAbilities
{
	/*
	 * Interface to force implementation of methods for all Offensive abilities 
	 */
	public interface IOffensiveSpecialAbility
	
	{
		void Use(int isPush);


		void SetCamera(Camera camera);
		
		




	}
}
