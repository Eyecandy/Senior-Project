using UnityEngine;

namespace Weapon
{
	public class SpecialAbilityAnimatorManager : MonoBehaviour
	{
		
	/*
	 * This script is on the Sci-FI Automatic
	 */
	
		
		/*
	 * Child Objects of SpecialAbilityAnimator
	 
		[SerializeField] private GameObject _lightForwardObject;
		[SerializeField] private GameObject _lightBackwardObject;
		[SerializeField] private GameObject _lineRendererObject;
		[SerializeField] private GameObject _glowObject;
		*/
	

		/*
	 * Components of the child Objects
	 
		[HideInInspector] public Light LightBackward;
		[HideInInspector] public Light LightForward;
		[HideInInspector] public LineRenderer LineRenderer;
		[HideInInspector] public ParticleSystem Glow;


		private void Start()
		{
			LightForward = _lightForwardObject.GetComponent<Light>();
			Debug.Log(_lightBackwardObject + "GO");
			Debug.Log("LIGHT" + LightForward);
			LightBackward = _lightBackwardObject.GetComponent<Light>();
			LineRenderer = _lineRendererObject.GetComponent<LineRenderer>();
			Glow = _glowObject.GetComponent<ParticleSystem>(); //null why?
		}

		public void EnableAnimation()
		{
			LightBackward.enabled = false;
			LightForward.enabled = false;
			LineRenderer.enabled = false;
			Glow.Stop();
		}
		*/
	}
	
}
