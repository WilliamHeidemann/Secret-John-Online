using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Relay;
using UnityEngine;

public class JoinButton : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameConnector gameConnector;
    public void OnSelect()
    {
        if (inputField.text == "Join Game")
        {
            inputField.text = string.Empty;
        }
    }

    public void OnEndEdit()
    {
        if (inputField.text.Length == 0)
        {
            inputField.text = "Join Game";
        }
    }

    public void OnValueChanged()
    {
        if (inputField.text != "Join Game" && inputField.text.Length > 6)
        {
            inputField.text = inputField.text[..6];
        }
        
        arrow.SetActive(inputField.text.Length == 6);
    }

    public async void TryJoin()
    {
        var code = inputField.text.ToUpper();
        inputField.text = "Join Game";
        arrow.SetActive(false);
        try
        {
            var allocation = await RelayService.Instance.JoinAllocationAsync(code);
            gameConnector.JoinGameAsClient(allocation, code);
        }
        catch (Exception e)
        {
            print(e);
        }

    }
}
