using TMPro;
using UnityEngine;

namespace _0_MainMenu
{
    public class QrJoin : MonoBehaviour
    {
        [SerializeField] private GameConnector gameConnector;
        [SerializeField] private TextMeshProUGUI buttonText;
        private string gameCode;

        public void SetGamePin(string code)
        {
            gameCode = code;
            buttonText.text = $"Join {code}";
        }

        public void TryJoin()
        {
            gameConnector.TryJoin(gameCode);
        }
    }
}