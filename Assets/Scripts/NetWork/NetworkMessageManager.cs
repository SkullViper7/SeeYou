using System;
using Unity.Netcode;
using UnityEngine;

public class NetworkMessageManager : MonoBehaviour
{

    private void Start()
    {
        GameManager.Instance.network = this;
    }

    [ServerRpc]
    public void ChangeHunterServerRpc(GameObject _hunter)
    {
        GameManager.Instance.teamManager.SetHunterForAllClients(_hunter);
    }

    [ServerRpc]
    public void ChangePreyServerRpc(GameObject _prey)
    {
        GameManager.Instance.teamManager.SetPreyForAllClients(_prey);
    }

    [ServerRpc]
    public void SetAllPlayerInPreyServerRpc()
    {
        GameManager.Instance.teamManager.SetAllPlayerInPrey();
    }
}
