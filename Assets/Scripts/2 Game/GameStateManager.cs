using System;
using System.Collections.Generic;
using System.Linq;
using _0_MainMenu;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

namespace _2_Game
{
    public class GameStateManager : NetworkBehaviour
    {
        [SerializeField] private PlayerInfo playerInfo;
        [SerializeField] private StandingsSetter standingsSetter;
        [SerializeField] private HistorySetter historySetter;
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

        [SerializeField] private Color liberalColor;
        [SerializeField] private Color fascistColor;
        [SerializeField] private Color hitlerColor;

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

            IEnumerable<(ulong OwnerClientId, FixedString32Bytes playerName)> players =
                FindObjectsByType<Player>(FindObjectsSortMode.None)
                    .Select(player => (player.OwnerClientId, player.PlayerName.Value));

            gameState = new GameState(players);

            foreach (var (id, alignment, role) in gameState.Teams.AllPlayerInfo())
            {
                playerInfo.SetPlayerInfoRpc(alignment, role, gameState.GetName(id), (int)id,
                    RpcTarget.Single(id, RpcTargetUse.Temp));
            }

            foreach (var (playerId, playerAlignment, playerRole) in gameState.Teams.AllPlayerInfo())
            {
                foreach (var (otherId, otherAlignment, otherRole) in gameState.Teams.AllPlayerInfo())
                {
                    Color color;
                    if (playerAlignment == Alignment.Fascist &&
                        otherAlignment == Alignment.Fascist &&
                        playerRole == Role.Member)
                        color = otherRole == Role.Hitler ? hitlerColor : fascistColor;
                    else color = liberalColor;
                    playerInfo.SetPlayerRpc((int)otherId, gameState.GetName(otherId), color,
                        RpcTarget.Single(playerId, RpcTargetUse.Temp));
                }
            }

            AllCanvas.ForEach(g => g.SetActive(false));
            standingsCanvas.SetActive(true);
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
            historySetter.AppendRpc(gameState.GetName(presidentId), gameState.GetName(chancellorId), policy);
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