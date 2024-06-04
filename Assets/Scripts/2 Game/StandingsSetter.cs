using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace _2_Game
{
    public class StandingsSetter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI liberalPolicies;
        [SerializeField] private TextMeshProUGUI fascistPolicies;

        [Rpc(SendTo.Everyone)]
        public void SetStandingRpc(Alignment alignment, int policies)
        {
            var max = alignment == Alignment.Liberal ? "5" : "6";
            var gui = alignment == Alignment.Liberal ? liberalPolicies : fascistPolicies;
            gui.text = $"{policies} / {max}";
        }
    }
}
