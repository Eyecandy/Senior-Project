
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    /*
     * This class has a dictionary, which stores the players that are online.
     * And if they diconnect they are removed from the dictionary.
     * This is fast way of finding out if a plyer is online or not.
     */
    private const string PlayerPrefix = "Player";
    private static readonly Dictionary<string, Player> Players = new Dictionary<string, Player>();
    
    
    public static void RegisterPlayer(string playerNetId,Player player) {
        var playerId = PlayerPrefix + playerNetId;
        Players.Add (playerId, player);
        player.transform.name = playerId;
    }
    
    
    public static void UnRegisterPlayer(string netId) {
        var playerId = PlayerPrefix + netId;
        Players.Remove (playerId);
    }
    
    public static Player GetPlayer(string playerId) {
        return Players [playerId];
    }

 
}
