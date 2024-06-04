using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace _2_Game
{
    public class PlayerInfo : NetworkBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerNameText;
        [SerializeField] private TextMeshProUGUI alignmentText;
        [SerializeField] private TextMeshProUGUI roleText;

        [SerializeField] private List<TextMeshProUGUI> playerNames;
        [SerializeField] private List<Image> playerOutlines;
        
        [Rpc(SendTo.SpecifiedInParams)]
        public void SetPlayerInfoRpc(Alignment alignment, Role role, string playerName, int index, RpcParams rpcParams)
        {
            alignmentText.text = alignment.ToString().ToUpper();
            roleText.text = role.ToString().ToUpper();
            playerNames[index].text = playerName;
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
        
        // Called in a double nested for loop
        // For every connected client
        // For every player info
        [Rpc(SendTo.SpecifiedInParams)]
        public void SetPlayerRpc(int index, FixedString32Bytes playerName, Color color, RpcParams rpcParams)
        {
            playerNames[index].text = playerName.ToString();
            playerOutlines[index].color = color;
        }
    }
}
