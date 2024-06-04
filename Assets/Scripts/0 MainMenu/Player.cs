using Unity.Collections;
using Unity.Netcode;

namespace _0_MainMenu
{
    public class Player : NetworkBehaviour
    {
        public readonly NetworkVariable<FixedString32Bytes> PlayerName = new(readPerm: NetworkVariableReadPermission.Everyone, writePerm: NetworkVariableWritePermission.Server);
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public override void OnNetworkSpawn()
        {
            const string defaultName = "New Player";
            PlayerName.OnValueChanged += UpdateNameDisplays;

            if (IsServer)
            {
                PlayerName.Value = defaultName;
            }
        
            FindFirstObjectByType<NamesManager>()?.SetNameTag(defaultName);
            UpdateNameDisplays("", "");
        }

        private static void UpdateNameDisplays(FixedString32Bytes oldName, FixedString32Bytes newName) => FindFirstObjectByType<NamesManager>()?.UpdateNames();

        [Rpc(SendTo.Server)]
        public void ChangeNameRpc(string newName)
        {
            PlayerName.Value = newName;
        }
    }
}
