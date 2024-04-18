using Unity.Netcode;
using UnityEngine;

public class NetworkMessageManager : NetworkBehaviour
{

    private void Start()
    {
        GameManager.Instance.network = this;
    }

    [ClientRpc]
    public void ChangeHunterClientRpc(int _hunter)
    {
        Debug.Log("ici on change les hunter");
        GameManager.Instance.teamManager.SetHunterForAllClients(_hunter);
    }

    [ServerRpc]
    public void ChangePreyServerRpc(int _prey)
    {
        GameManager.Instance.teamManager.SetPreyForAllClients(_prey);
    }

    [ServerRpc]
    public void SetAllPlayerInPreyServerRpc()
    {
        GameManager.Instance.teamManager.SetAllPlayerInPrey();
    }

    [ServerRpc]
    public void SetHunterForManagersServerRpc()
    {
        Debug.Log("ici on change les hunter");
        ChangeHunterClientRpc(GameManager.Instance.teamManager.FindAHunterServ());
    }
}
