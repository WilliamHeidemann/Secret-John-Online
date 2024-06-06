using System.Collections.Generic;
using System.Linq;
using _0_MainMenu;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace _1_Lobby
{
    public class NamesManager : NetworkBehaviour
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private List<TextMeshProUGUI> names;

        public void SetNameTag(string nameTag)
        {
            inputField.text = nameTag;
        }

        public void SetName()
        {
            var input = inputField.text;
            var players = FindObjectsByType<Player>(FindObjectsSortMode.None);
            players.First(player => player.IsOwner).ChangeNameRpc(input);
        }

        public void RestrictLength()
        {
            if (inputField.text.Length > 10)
                inputField.text = inputField.text[..10];
        }

        public void UpdateNames()
        {
            var players = 
                FindObjectsByType<Player>(FindObjectsSortMode.None)
                .OrderBy(player => player.OwnerClientId)
                .ToList();

            for (var i = 0; i < players.Count; i++)
                names[i].text = players[i].PlayerName.Value.ToString();
        }
    }
}