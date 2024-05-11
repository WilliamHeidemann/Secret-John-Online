using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class NamesManager : NetworkBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> names;
    public static NamesManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [Rpc(SendTo.Server)]
    public void SetNameRequestRpc(ulong playerId, string playerName)
    {
        SetNameRpc(playerId, playerName);
    }

    [Rpc(SendTo.Everyone)]
    public void SetNameRpc(ulong playerId, string playerName)
    {
        print($"Finding player with id {playerId} within {NetworkManager.Singleton.ConnectedClientsIds.Count} ids");
        var ids = NetworkManager.Singleton.ConnectedClientsIds;
        for (var i = 0; i < ids.Count; i++)
        {
            if (playerId == ids[i])
            {
                print("Found");
                names[i].text = playerName;
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("Updating names");
            UpdateNames();
        }
    }
    
    public void UpdateNames()
    {
        var players = NetworkManager.Singleton.ConnectedClientsIds;
        for (int i = 0; i < players.Count; i++)
        {
            names[i].text = "Player " + players[i];
        }
    }
    
}
