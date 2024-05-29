using UnityEngine;
using Unity.Netcode;

public class MineNetwork : NetworkBehaviour
{
    [ClientRpc]
    private void ExplodingMineClientRPC(int newHunter)
    {
        GameManager.Instance.teamManager.SetHunterForAllClients(newHunter);
    }
}
