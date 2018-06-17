using UnityEngine;

namespace Unity_UI
{
	public class QuitOnClick : MonoBehaviour {

		public void Quit()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}
	}
}
