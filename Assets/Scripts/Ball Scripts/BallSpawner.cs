using UnityEngine;
using UnityEngine.Networking;

namespace Ball_Scripts
{
    public class BallSpawner : NetworkBehaviour
    {
        [SerializeField] private GameObject _ballPrefab;
        [SerializeField] private int _numberOfBalls;
        [SerializeField] private Transform _plane;
        private float _cornerX;
        private float _cornerY;
        private float _cornerZ;
        private Vector3 _corner;
        private float _span;
        public Vector3[] Positions; 
        
        public override void OnStartServer()
        {
            _cornerX = (_plane.localScale.x / 2)*10 + _plane.position.x;
            _cornerY = _plane.position.y+1.78f;
            _cornerZ = (_plane.localScale.z / 2)*10 + _plane.position.z - 1;
            _corner = new Vector3(_cornerX,_cornerY,_cornerZ);
            _span = ((_plane.transform.localScale.x * 10f)/_numberOfBalls) - 1.5f;
            Vector3 spanVector3 = new Vector3(_span, 0f, 0f);
            Positions = new Vector3[_numberOfBalls];
            for (int i = 0; i < _numberOfBalls; i++)
            {
                Positions[i] = _corner - spanVector3;
                GameObject ball = Instantiate(_ballPrefab, Positions[i], Quaternion.identity);
                BallMotor ballMotor = ball.GetComponent<BallMotor>();
                ballMotor.StartPosition = Positions[i];
                _corner = _corner - spanVector3;
                ballMotor.SetBallName("Ball-" + i);
                NetworkServer.Spawn(ball);
            }
        }
        
        
        
    }
}
