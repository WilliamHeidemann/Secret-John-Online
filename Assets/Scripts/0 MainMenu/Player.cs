using Unity.Collections;
using Unity.Netcode;

namespace _0_MainMenu
{
    public class Player : NetworkBehaviour
    {
        public readonly NetworkVariable<FixedString32Bytes> PlayerName = new("Some Name");

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public override void OnNetworkSpawn()
        {
            FindFirstObjectByType<NamesManager>()?.SetNameTag("New Player");
            PlayerName.OnValueChanged += UpdateNameDisplays;
            UpdateNameDisplays("", "");
        }

        private static void UpdateNameDisplays(FixedString32Bytes oldName, FixedString32Bytes newName) =>
            FindFirstObjectByType<NamesManager>()?.UpdateNames();

        [Rpc(SendTo.Server)]
        public void ChangeNameRpc(string newName)
        {
            if (string.IsNullOrEmpty(newName)) return;
            PlayerName.Value = newName;
        }
    }
}