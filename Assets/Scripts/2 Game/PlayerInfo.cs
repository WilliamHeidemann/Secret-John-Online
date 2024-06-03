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

        [Rpc(SendTo.SpecifiedInParams)]
        public void SetPlayerInfoRpc(Alignment alignment, Role role, RpcParams rpcParams)
        {
            alignmentText.text = alignment.ToString();
            roleText.text = role.ToString();
        }
    }
}
