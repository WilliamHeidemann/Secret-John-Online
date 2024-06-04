using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public readonly NetworkVariable<FixedString32Bytes> PlayerName = new();
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public override void OnNetworkSpawn()
    {
        const string defaultName = "New Player";

        if (IsServer)
        {
            PlayerName.Value = defaultName;
        }
        
        FindFirstObjectByType<NamesManager>()?.SetNameTag(defaultName);
        UpdateNameDisplays();

        PlayerName.OnValueChanged += (value, newValue) =>
        {
            UpdateNameDisplays();
        };
    }

    private static void UpdateNameDisplays() => FindFirstObjectByType<NamesManager>()?.UpdateNames();

    [Rpc(SendTo.Server)]
    public void ChangeNameRpc(string newName)
    {
        PlayerName.Value = newName;
    }
}
