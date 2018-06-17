using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity_UI
{
	public class LoadSceneOnClick : MonoBehaviour {

		public void LoadByIndex(int sceneIndex)
		{
			SceneManager.LoadScene(sceneIndex);
		}
	}
}
