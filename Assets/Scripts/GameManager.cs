
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
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
