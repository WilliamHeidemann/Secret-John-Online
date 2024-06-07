using _1_Lobby;
using Unity.Collections;
using Unity.Netcode;

namespace _0_MainMenu
{
    public class Player : NetworkBehaviour
    {
        public readonly NetworkVariable<FixedString32Bytes> PlayerName = new("Some Name");

        private void Awake() => DontDestroyOnLoad(gameObject);

        public override void OnNetworkSpawn() => PlayerName.OnValueChanged += UpdateNameDisplays;

        private void UpdateNameDisplays(FixedString32Bytes oldName, FixedString32Bytes newName) =>
            FindFirstObjectByType<NamesManager>()?.UpdateName(newName.ToString(), (int)OwnerClientId);

        [Rpc(SendTo.Server)]
        public void ChangeNameRpc(string newName)
        {
            if (string.IsNullOrEmpty(newName)) return;
            PlayerName.Value = newName;
        }
    }
}