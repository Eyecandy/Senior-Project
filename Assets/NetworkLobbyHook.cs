
using Player_Scripts;
using Prototype.NetworkLobby;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook {
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager,
        GameObject lobbyPlayerGo,
        GameObject gamePlayerGo)
    {
        var lobbyPlayer = lobbyPlayerGo.GetComponent<LobbyPlayer>();
        var localPlayer = gamePlayerGo.GetComponent<Player>();
        localPlayer.PlayerName = lobbyPlayer.playerName;
    }

	
}