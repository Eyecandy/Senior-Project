using UnityEngine;

namespace Pickup_Scripts
{
	public class Rotator : MonoBehaviour {
	
		// Update is called once per frame
		void Update () {
			transform.Rotate (new Vector3 (15, 30, 45) * 0.75f * Time.deltaTime);
		}
	}
}
