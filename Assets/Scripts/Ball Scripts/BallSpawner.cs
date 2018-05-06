using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BallSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private int numberOfBalls;
    [SerializeField] private Transform plane;
    private float cornerX;
    private float cornerY;
    private float cornerZ;
    private Vector3 corner;
    private float span;
    public Vector3[] positions; 

    public override void OnStartServer()
    {
        cornerX = (plane.localScale.x / 2)*10 + plane.position.x;
        cornerY = plane.position.y+1.78f;
        cornerZ = (plane.localScale.z / 2)*10 + plane.position.z - 1;
        corner = new Vector3(cornerX,cornerY,cornerZ);
        span = ((plane.transform.localScale.x * 10f)/numberOfBalls) - 1.5f;
        Vector3 spanVector3 = new Vector3(span, 0f, 0f);
        positions = new Vector3[numberOfBalls];
        for (int i = 0; i < numberOfBalls; i++)
        {
            positions[i] = corner - spanVector3;
            GameObject ball = Instantiate(ballPrefab, positions[i], Quaternion.identity);
            ball.name = "Ball-"+i.ToString();
            corner = corner - spanVector3;
            NetworkServer.Spawn(ball);
        }
    }
}
