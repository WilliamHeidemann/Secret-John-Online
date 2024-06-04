using System;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace _2_Game
{
    public class GameStateManager : NetworkBehaviour
    {
        [SerializeField] private PlayerInfo playerInfo;
        [SerializeField] private StandingsSetter standingsSetter;
        private GameState gameState;

        private bool isGovernmentEnacted;
        private ulong presidentId;
        private ulong chancellorId;

        [SerializeField] private GameObject drawCanvas;
        [SerializeField] private GameObject playersCanvas;
        [SerializeField] private GameObject standingsCanvas;
        [SerializeField] private GameObject historyCanvas;

        private List<GameObject> AllCanvas => new()
        {
            drawCanvas,
            playersCanvas,
            standingsCanvas,
            historyCanvas
        };

        [SerializeField] private DrawSystem drawSystem;

        public override async void OnNetworkSpawn()
        {
            if (!NetworkManager.Singleton.IsHost)
            {
                gameObject.SetActive(false);
                return;
            }

            while (!playerInfo.IsSpawned)
            {
                await Awaitable.NextFrameAsync();
            }

            var playerIds = NetworkManager.Singleton.ConnectedClientsIds;
            gameState = new GameState(playerIds);
            foreach (var (id, alignment, role) in gameState.Teams.AllPlayerInfo())
            {
                playerInfo.SetPlayerInfoRpc(alignment, role, RpcTarget.Single(id, RpcTargetUse.Temp));
            }
        }

        [Rpc(SendTo.Server)]
        public void ElectGovernmentRpc(ulong electedPresidentId, ulong electedChancellorId)
        {
            if (isGovernmentEnacted) return;
            presidentId = electedPresidentId;
            chancellorId = electedChancellorId;
            var (card1, card2, card3) = gameState.Policies.DrawThree();
            SendPresidentToDrawRpc(card1, card2, card3, RpcTarget.Single(presidentId, RpcTargetUse.Temp));
        }

        [Rpc(SendTo.SpecifiedInParams)]
        private void SendPresidentToDrawRpc(Alignment first, Alignment second, Alignment third, RpcParams rpcParams)
        {
            AllCanvas.ForEach(g => g.SetActive(false));
            drawCanvas.SetActive(true);
            drawSystem.SetPresidentPolicies(first, second, third);
        }


        [Rpc(SendTo.Server)]
        public void EnactPolicyRpc(Alignment policy)
        {
            gameState.Policies.EnactPolicy(policy);
            standingsSetter.SetStandingRpc(policy, gameState.Policies.PoliciesCount(policy));
        }

        [Rpc(SendTo.Server)]
        public void ForwardPoliciesRpc(Alignment policy1, Alignment policy2)
        {
            SendChancellorToDrawRpc(policy1, policy2, RpcTarget.Single(chancellorId, RpcTargetUse.Temp));
        }

        [Rpc(SendTo.SpecifiedInParams)]
        private void SendChancellorToDrawRpc(Alignment first, Alignment second, RpcParams rpcParams)
        {
            AllCanvas.ForEach(g => g.SetActive(false));
            drawCanvas.SetActive(true);
            drawSystem.SetChancellorPolicies(first, second);
        }

        [Rpc(SendTo.Server)]
        public void DiscardPolicyRpc(Alignment policy)
        {
            gameState.Policies.Discard(policy);
        }
    }
}