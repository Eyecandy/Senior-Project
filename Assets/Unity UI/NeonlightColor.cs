using SpriteGlow;
using UnityEngine;
using UnityEngine.UI;

namespace Unity_UI
{
	public class NeonlightColor : MonoBehaviour {

		private SpriteGlowEffect _neonLight;
		private bool _descend;

		// Use this for initialization
		void Start ()
		{
			_neonLight = GetComponent<SpriteGlowEffect>();
			_neonLight.GlowBrightness = 1.2f;
			_descend = false;
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (_descend)
			{
				_neonLight.GlowBrightness -= 0.01f;
				if (_neonLight.GlowBrightness <= 1.2f)
				{
					_descend = false;
				}
			}
			else
			{
				_neonLight.GlowBrightness += 0.01f;
				if (_neonLight.GlowBrightness >= 1.4f)
				{
					_descend = true;
				}
			}
		
		}
	}
}
