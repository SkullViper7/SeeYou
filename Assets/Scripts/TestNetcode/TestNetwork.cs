using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class TestNetwork : NetworkBehaviour
{
    public bool HasClicked;
    private NetworkManager _network;

    public void TestCall()
    {
        Debug.Log("TestCallButton");
        if (IsOwner)
        {
            TestClientRpc();
        }
    }

    private void Start()
    {
        if (this._network == null)
        {
            this._network = NetworkManager.Singleton;
        }

        var ButtonSearch = FindObjectsOfType<Button>();

        foreach (var _button in ButtonSearch)
        {
            if (_button.gameObject.tag == "Test")
            {
                _button.onClick.AddListener(TestCall);
            }
        }
    }

    public override void OnNetworkSpawn()
    {
        TestMAnager.Instance.players++;
        this.gameObject.name += TestMAnager.Instance.players;
    }

    [ServerRpc (RequireOwnership = false)]
    public void TestServerRpc()
    {
        HasClicked = true;
        Debug.Log("TestServerCall");
    }

    [ClientRpc]
    public void TestClientRpc()
    {
        HasClicked = true;
        Debug.Log("TestClientCall");
    }

}
