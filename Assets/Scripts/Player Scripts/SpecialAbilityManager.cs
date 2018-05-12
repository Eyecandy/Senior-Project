using System.Net.Configuration;
using JetBrains.Annotations;
using SpecialAbility.DeffensiveSpecialAbilities;
using SpecialAbility.OffensiveSpecialAbilities;
using UnityEngine;

namespace Player_Scripts
{
	public class SpecialAbilityManager : MonoBehaviour
	{



		[SerializeField] private GameObject _emptyAbilty;
		
		[SerializeField] private Camera _camera;

		private EmptyOffensiveAbility _emptyOffensive;
		
		public OffensiveSpecialAbility OffensiveSpecialAbility; //Should be tagged as offensive.

		 //public DefensiveSpecialAbility DefensiveSpecialAbility; //Should be tagged as defensive.
		
		
		
		private void Start()
		{
			OffensiveSpecialAbility = _emptyAbilty.GetComponent<OffensiveSpecialAbility>();

		}
		
		
		/* Pick up SpecialAbility on trigger event
		 * SpecialAbility object has script atached to it derived from special ability
		 * Set OffensiveSpecial ability to that script
		 * Destroy object
		 * 
		 */
		private void OnTriggerEnter(Collider other)
		{
			if (!other.gameObject.CompareTag("Pick Up")) return;
			OffensiveSpecialAbility = other.gameObject.GetComponent<OffensiveSpecialAbility>();
			OffensiveSpecialAbility.SetCamera(_camera);
			other.gameObject.SetActive(false);

		}

	}
}
