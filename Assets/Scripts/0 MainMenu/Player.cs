using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    // private NetworkVariable<string> playerName = new NetworkVariable<string>();
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public override void OnNetworkSpawn()
    {
        print("Spawned!");
        print("Is host: " + NetworkManager.Singleton.IsHost);
        print("Is client: " + NetworkManager.Singleton.IsClient);
        print("Client ID: " + NetworkManager.Singleton.LocalClientId);
    }

    public void UpdateName()
    {
        print("Updating name");
        var playerId = NetworkManager.Singleton.LocalClientId;
        NamesManager.Instance.SetNameRequestRpc(playerId, "Player Name");
    }
}
