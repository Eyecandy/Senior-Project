
using System.Collections.Generic;
using System.Linq;
using Ball_Scripts;
using Player_Scripts;
using UnityEngine;

public class GameManager : MonoBehaviour {
    /*
     * This class has a dictionary, which stores the players that are online.
     * And if they diconnect they are removed from the dictionary.
     * This is fast way of finding out if a plyer is online or not.
     */
    private const string PlayerPrefix = "Player";
    private static readonly Dictionary<string, Player> Players = new Dictionary<string, Player>();

    [SerializeField]
    private GameObject _sceneCamera;
    public static GameManager Singleton;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }

    public void SetSceneCamera(bool active)
    {
        if (_sceneCamera == null)
        {
            return;
        }
        _sceneCamera.SetActive(active);
    }


    public static void RegisterPlayer(string playerNetId,Player player) 
    {
        var playerId = PlayerPrefix + playerNetId;
        if (Players.ContainsKey(playerId))
        {
            Players[playerId] = player;
            player.transform.name = playerId;
            
        }else{
            player.transform.name = playerId;
            Players.Add (playerId, player);
        }
    }


    public static void UnRegisterPlayer(string playerId)
    {
        Debug.Log("Deregister " + playerId);
        Debug.Log("PlayersDictionary: " + Players[playerId]);
        Players.Remove(playerId);
    }

    public static Player GetPlayer(string playerId) 
    {
        return Players [playerId];
    }
    
    
    private static readonly Dictionary<string, BallMotor> BallMotors  = new Dictionary<string, BallMotor>();
         
         
    public static void RegisterBallMotor(string ballNetId, BallMotor ballMotor) 
    {
        if (BallMotors.ContainsKey(ballNetId))
        {
            BallMotors[ballNetId] = ballMotor;
            ballMotor.transform.name = ballNetId;
        }
        else
        {
            ballMotor.transform.name = ballNetId;
            BallMotors.Add (ballNetId, ballMotor);
        }
    }
         
         
    public static void UnRegisterBallMotor(string ballNetId) 
    {
        
        BallMotors.Remove (ballNetId);
    }
         
    public static BallMotor GetBallMotor(string ballNetId)
    {
        return BallMotors[ballNetId];
    }

    public static Dictionary<string, BallMotor> BallMotorsDictionary
    {
        get { return BallMotors; }
    }

    //Returns the length of the ball motors dictionary
    public void BallMotorsDictionaryLength()
    {
        Debug.Log(BallMotors.Count);
    }
    
    //Returns the length of the ball motors dictionary
    public void PlayersDictionaryLength()
    {
        Debug.Log("PlayersDictionary Length: " + Players.Count);
    }

    public void PrintDictionary()
    {
        Debug.Log("Start of Players");
        foreach (var key in Players)
        {
            print(key);
        }
        Debug.Log("End of Players");
    }
}
