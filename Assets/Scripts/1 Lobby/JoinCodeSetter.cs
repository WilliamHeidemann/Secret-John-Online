using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JoinCodeSetter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI joinCode;

    void Start()
    {
        joinCode.text = GameConnector.JoinCode;
    }
}