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

        public override void OnNetworkSpawn()
        {
            var players = FindObjectsByType<Player>(FindObjectsSortMode.None);
            foreach (var player in players)
            {
                names[(int)player.OwnerClientId].text = player.PlayerName.Value.ToString();
            }
        }

        public void SetNameTag(string nameTag)
        {
            inputField.text = nameTag;
        }

        public void SetName()
        {
            var input = inputField.text;
            var players = FindObjectsByType<Player>(FindObjectsSortMode.None);
            var owner = players.First(player => player.IsOwner);
            var index = (int)owner.OwnerClientId;
            print(index);
            names[index].text = input;
            owner.ChangeNameRpc(input);
        }

        public void RestrictLength()
        {
            if (inputField.text.Length > 10)
                inputField.text = inputField.text[..10];
        }

        public void UpdateName(string newName, int index)
        {
            print(index);
            names[index].text = newName;
        }
    }
}