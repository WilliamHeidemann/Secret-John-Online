using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace _2_Game
{
    public class PlayerInfo : NetworkBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerNameText;
        [SerializeField] private TextMeshProUGUI alignmentText;
        [SerializeField] private TextMeshProUGUI roleText;

        [SerializeField] private List<TextMeshProUGUI> playerNames = new();
        
        [Rpc(SendTo.SpecifiedInParams)]
        public void SetPlayerInfoRpc(Alignment alignment, Role role, RpcParams rpcParams)
        {
            alignmentText.text = alignment.ToString().ToUpper();
            roleText.text = role.ToString().ToUpper();

            SetPlayerNames();
        }

        private void SetPlayerNames()
        {
            var players = FindObjectsByType<Player>(FindObjectsSortMode.None);
            players = players.OrderBy(p => p.GetComponent<NetworkObject>().OwnerClientId).ToArray();
            for (int i = 0; i < players.Length; i++)
            {
                var player = players[i];
                playerNames[i].text = player.PlayerName.Value.ToString();
                if (player.IsOwner) playerNameText.text = player.PlayerName.Value.ToString();
            }
        }
    }
}
