using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QrJoin : MonoBehaviour
{
    [SerializeField] private GameConnector gameConnector;
    private string gameCode;

    public void SetGamePin(string code)
    {
        gameCode = code;
    }

    public void TryJoin()
    {
        gameConnector.TryJoin(gameCode);
    }
}
