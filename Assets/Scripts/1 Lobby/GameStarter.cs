using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : NetworkBehaviour
{
    [SerializeField] private int gameSceneIndex;
    
    private void Start()
    {
        if (!NetworkManager.Singleton.IsHost)
        {
            gameObject.SetActive(false);
        }
    }
    
    public void StartGame()
    {
        if (!NetworkManager.Singleton.IsHost) return;
        StartGameRpc();
    }

    [Rpc(SendTo.Everyone)]
    private void StartGameRpc()
    {
        SceneManager.LoadScene(gameSceneIndex);
    }
}
