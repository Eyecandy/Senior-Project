using SpriteGlow;
using UnityEngine;

public class LightToggle : MonoBehaviour
{
	private SpriteGlowEffect _brakeLight;
	private bool _descend;

	// Use this for initialization
	void Start ()
	{
		_brakeLight = GetComponent<SpriteGlowEffect>();
		_brakeLight.GlowBrightness = 1.54f;
		_descend = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (_descend)
		{
			_brakeLight.GlowBrightness -= 0.01f;
			if (_brakeLight.GlowBrightness <= 1.54f)
			{
				_descend = false;
			}
		}
		else
		{
			_brakeLight.GlowBrightness += 0.05f;
			if (_brakeLight.GlowBrightness >= 2.5f)
			{
				_descend = true;
			}
		}
		
	}
}
