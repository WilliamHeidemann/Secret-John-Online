using Unity.Netcode;

namespace _2_Game
{
    public class GameStateManager : NetworkBehaviour
    {
        private GameState gameState;
    
        private void Start()
        {
            if (!NetworkManager.Singleton.IsHost)
            {
                gameObject.SetActive(false);
                return;
            }

            var playerIds = NetworkManager.Singleton.ConnectedClientsIds;
            gameState = new GameState(playerIds);
        }
    }
}
