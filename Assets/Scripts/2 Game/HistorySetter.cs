using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

namespace _2_Game
{
    public class HistorySetter : NetworkBehaviour
    {
        [SerializeField] private List<TextMeshProUGUI> history;
        private int indexCounter;
        [SerializeField] private Color liberalColor;
        [SerializeField] private Color fascistColor;
        [SerializeField] private Color neutralColor;

        public override void OnNetworkSpawn()
        {
            foreach (var text in history)
            {
                text.color = neutralColor;
                text.text = "-";
            }
        }

        [Rpc(SendTo.Everyone)]
        public void AppendRpc(FixedString32Bytes president, FixedString32Bytes chancellor, Alignment policy)
        {
            history[indexCounter].text = president.ToString();
            indexCounter++;

            history[indexCounter].text = chancellor.ToString();
            indexCounter++;

            history[indexCounter].text = policy.ToString().ToUpper();
            history[indexCounter].color = policy == Alignment.Liberal ? liberalColor : fascistColor;
            indexCounter++;
        }
    }
}