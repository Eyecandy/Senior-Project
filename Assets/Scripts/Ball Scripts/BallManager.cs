using UnityEngine;
using UnityEngine.Networking;

public class BallManager : NetworkBehaviour
{

	[SerializeField] private Transform plane;
	[SerializeField] private GameObject sphere;
	[SerializeField] private int numberOfBalls;
	private float cornerX;
	private float cornerY;
	private float cornerZ;
	private Vector3 corner;
	private float span;
	public Vector3[] positions; 

	// Use this for initialization
	void Start () {
		cornerX = (plane.localScale.x / 2)*10 + plane.position.x;
		cornerY = plane.position.y;
		cornerZ = (plane.localScale.z / 2)*10 + plane.position.z - 1;
		corner = new Vector3(cornerX,cornerY,cornerZ);
		span = ((plane.transform.localScale.x * 10f)/numberOfBalls) - 1.5f;
		Vector3 spanVector3 = new Vector3(span, 0f, 0f);
		positions = new Vector3[numberOfBalls];
		for (int i = 0; i < numberOfBalls; i++)
		{
			positions[i] = corner - spanVector3;
			Debug.Log(positions[i]);
			var obj = Instantiate(sphere, positions[i], Quaternion.identity);
			obj.name = "Ball-"+i.ToString();
			corner = corner - spanVector3;
		}
		
	}
}
