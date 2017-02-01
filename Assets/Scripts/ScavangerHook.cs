using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScavangerHook : Prototype.NetworkLobby.LobbyHook
{
    private int playerNumber = 0;
    private GameManager gameManager;

    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        playerNumber++;
        
        //if(gameManager == null)
            //gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        //Debug.Log(playerNumber);
        //Debug.Log("GameManager: " + gameManager);

        if(playerNumber == 1)
        {
            //gameManager.AssignPlayerOne(gamePlayer);
            GameManager.AssignPlayerOne(gamePlayer);
        }
        else if (playerNumber == 2)
        {
            //gameManager.AssignPlayerTwo(gamePlayer);
            GameManager.AssignPlayerTwo(gamePlayer);
            GameManager.AssignPlayersToRockets();
            playerNumber = 0;
        }
    }
}
