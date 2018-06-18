using SpriteGlow;
using UnityEngine;
using System;

namespace Unity_UI
{
	public class SpotlightColor : MonoBehaviour {

		private SpriteGlowEffect _spotLight;
		private float _timeBeforeSwitch = 3f;
		private Color[] _colors = new Color[4];
		private int _current;

		// Use this for initialization
		void Start ()
		{
			_spotLight = GetComponent<SpriteGlowEffect>();
			_spotLight.GlowColor = Color.cyan;
			_current = 0;
			_colors[0] = Color.cyan;
			_colors[1] = Color.red;
			_colors[2] = Color.yellow;
			_colors[3] = Color.green;
		}
	
		// Update is called once per frame
		void Update ()
		{
			_timeBeforeSwitch -= Time.deltaTime;
			if (_timeBeforeSwitch <= 0f)
			{
				_spotLight.GlowColor = _colors[_current];
				_timeBeforeSwitch = 3f;
				_current = (_current + 1) % _colors.Length;
			}
			
		}
	}
}
