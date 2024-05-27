using System;
using System.Collections;
using System.Collections.Generic;
using _2_Game;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI alignmentText;
    [SerializeField] private TextMeshProUGUI roleText;

    [Rpc(SendTo.SpecifiedInParams)]
    public void SetPlayerInfo(Alignment alignment, Role role, RpcSendParams sendParams)
    {
        alignmentText.text = alignment.ToString();
        roleText.text = role.ToString();
    }
}
