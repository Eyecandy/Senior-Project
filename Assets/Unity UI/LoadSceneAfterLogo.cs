using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity_UI
{
	public class LoadSceneAfterLogo : MonoBehaviour {

		// Use this for initialization
		void Start ()
		{
			StartCoroutine(Load());
		}
	

		private IEnumerator Load()
		{
			yield return new WaitForSeconds(3f);
			SceneManager.LoadScene(1);
		}
	}
}
