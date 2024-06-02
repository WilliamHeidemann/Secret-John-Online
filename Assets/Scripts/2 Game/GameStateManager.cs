using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace _2_Game
{
    public class GameStateManager : NetworkBehaviour
    {
        [SerializeField] private PlayerInfo playerInfo;
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
            foreach (var (id, alignment, role) in gameState.Teams.AllPlayerInfo())
            {
                playerInfo.SetPlayerInfo(alignment, role, RpcTarget.Single(id, RpcTargetUse.Temp));
            }
        }

        [Rpc(SendTo.Server)]
        public void EnactPolicyRpc(Alignment policy)
        {
            gameState.Policies.EnactPolicy(policy);
        }

        [Rpc(SendTo.Server)]
        public void ForwardPoliciesRpc(Alignment policy1, Alignment policy2)
        {
            
        }

        [Rpc(SendTo.Server)]
        public void DiscardPolicyRpc(Alignment policy)
        {
            gameState.Policies.Discard(policy);
        }
    }
}
