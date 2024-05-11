using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameConnector : MonoBehaviour
{
    public static string JoinCode = string.Empty;
    private UnityTransport transport;

    private async void Awake()
    {
        DontDestroyOnLoad(this);
        await Authenticate();
    }

    private async Task Authenticate()
    {
        transport = FindFirstObjectByType<UnityTransport>();
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
    
    [SerializeField] private int lobbySceneIndex;
    public async void StartHost()
    {
        await CreateAllocation();
        NetworkManager.Singleton.StartHost();
        SceneManager.LoadScene(lobbySceneIndex);
    }

    private async Task CreateAllocation()
    {
        var allocation = await RelayService.Instance.CreateAllocationAsync(10, "europe-north1");
        transport.SetRelayServerData(new RelayServerData(allocation, "wss"));
        transport.UseWebSockets = true;
        JoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        print(JoinCode);
    }
    
    public void JoinGameAsClient(JoinAllocation allocation, string code)
    {
        transport.SetRelayServerData(new RelayServerData(allocation, "wss"));
        transport.UseWebSockets = true;
        JoinCode = code;
        NetworkManager.Singleton.StartClient();
        SceneManager.LoadScene(lobbySceneIndex);
    }
}
