using System.Net.Configuration;
using OffensiveSpecialAbilities;
using SpecialAbility.DeffensiveSpecialAbilities;
using UnityEngine;

namespace Player_Scripts
{
	public class SpecialAbilityManager : MonoBehaviour
	{
		[SerializeField] private Camera _camera;

		public OffensiveSpecialAbility OffensiveSpecialAbility; //Should be tagged as offensive.

		public DefensiveSpecialAbility DefensiveSpecialAbility; //Should be tagged as defensive.
		
		
		/* Pick up SpecialAbility on trigger event
		 * SpecialAbility object has script atached to it derived from special ability
		 * Set OffensiveSpecial ability to that script
		 * Destroy object
		 * 
		 */
		private void Pickup()
		{
			
		}






	}
}
