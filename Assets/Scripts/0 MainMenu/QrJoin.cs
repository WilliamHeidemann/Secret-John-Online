using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QrJoin : MonoBehaviour
{
    [SerializeField] private GameConnector gameConnector;
    [SerializeField] private TextMeshProUGUI buttonText;
    private string gameCode;

    public void SetGamePin(string code)
    {
        gameCode = code;
        buttonText.text = $"Join {code}";
    }

    public void TryJoin()
    {
        gameConnector.TryJoin(gameCode);
    }
}
