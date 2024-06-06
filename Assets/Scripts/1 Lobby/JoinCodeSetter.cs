using _0_MainMenu;
using TMPro;
using UnityEngine;

namespace _1_Lobby
{
    public class JoinCodeSetter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI joinCode;

        void Start()
        {
            joinCode.text = GameConnector.JoinCode;
        }
    }
}