using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _1_Lobby
{
    public class GameStarter : NetworkBehaviour
    {
        [SerializeField] private GameObject gameStarterButton;

        private void Start()
        {
            if (!NetworkManager.Singleton.IsHost)
            {
                gameStarterButton.SetActive(false);
            }
        }

        public void StartGame()
        {
            if (!NetworkManager.Singleton.IsHost) return;
            StartGameRpc();
        }

        [Rpc(SendTo.ClientsAndHost)]
        private void StartGameRpc()
        {
            NetworkManager.Singleton.SceneManager.LoadScene("2 Game", LoadSceneMode.Single);
        }
    }
}