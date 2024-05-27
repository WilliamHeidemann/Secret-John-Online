using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : NetworkBehaviour
{
    [SerializeField] private int gameSceneIndex;
    [SerializeField] private GameObject gameStarterButton;
    
    private void Start()
    {
        if (!NetworkManager.Singleton.IsHost)
        {
            gameStarterButton.SetActive(false);
        }
    }
    
    public void StartGame()
    {
        if (!NetworkManager.Singleton.IsHost) return;
        print("Starting game");
        StartGameRpc();
        print("Start game called");
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void StartGameRpc()
    {
        NetworkManager.Singleton.SceneManager.LoadScene("2 Game", LoadSceneMode.Single);
        // SceneManager.LoadScene(gameSceneIndex);
    }
}