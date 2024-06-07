using System;
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

namespace _0_MainMenu
{
    public class GameConnector : MonoBehaviour
    {
        public static string JoinCode = "ABCDEF";
        private UnityTransport transport;
        [SerializeField] private QrJoin qrJoinButton;
        [SerializeField] private InputFieldJoin inputFieldJoin;

        private async void Awake()
        {
            DontDestroyOnLoad(this);
            await Authenticate();
        }

        private async Task Authenticate()
        {
            try
            {
                await UnityServices.InitializeAsync();
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                transport = FindFirstObjectByType<UnityTransport>();
            }
            catch (Exception e)
            {
                print(e);
                throw;
            }
        }

        [SerializeField] private int lobbySceneIndex;

        public async void StartHost()
        {
            await CreateAllocation();
            NetworkManager.Singleton.StartHost();
            NetworkManager.Singleton.SceneManager.LoadScene("1 Lobby", LoadSceneMode.Single);
        }

        private async Task CreateAllocation()
        {
            var allocation = await RelayService.Instance.CreateAllocationAsync(10, "europe-north1");
            transport.SetRelayServerData(new RelayServerData(allocation, "wss"));
            transport.UseWebSockets = true;
            JoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            print(JoinCode);
        }

        public async void TryJoin(string code)
        {
            try
            {
                var allocation = await RelayService.Instance.JoinAllocationAsync(code);
                JoinGameAsClient(allocation, code);
            }
            catch (Exception e)
            {
                print(e);
                throw;
            }
        }

        private void JoinGameAsClient(JoinAllocation allocation, string code)
        {
            transport.SetRelayServerData(new RelayServerData(allocation, "wss"));
            transport.UseWebSockets = true;
            JoinCode = code;
            NetworkManager.Singleton.StartClient();
        }

        // Method is called via javascript in browser
        public void ShowQrJoinButton(string code)
        {
            qrJoinButton.gameObject.SetActive(true);
            qrJoinButton.SetGamePin(code);
            inputFieldJoin.gameObject.SetActive(false);
        }
    }
}