using System.Diagnostics;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    private NetworkManager _network;

    private PlayerMain _playerMain;

    private void Start()
    {
        if (this._network == null)
        {
            this._network = NetworkManager.Singleton;
        }

        this._playerMain = this.GetComponent<PlayerMain>();
    }

    public void InitPlayerMain(PlayerMain _PM)
    {
        this._playerMain = _PM;
        _PM.playerNetwork = this;
    }

    public bool ActionFromClient()
    {
        bool canDoTheAction = false;
        if (this._network == null)
        {
            this._network = NetworkManager.Singleton;
        }

        if (this._network.LocalClient != null)
        {
            if (this._network.LocalClient.PlayerObject.TryGetComponent(out PlayerMain playerMain))
            {
                canDoTheAction = true;
            }
        }

        return canDoTheAction;
    }

    public bool IsOwnerOfTheGameObject()
    {
        return this.IsOwner;
    }

    public override void OnNetworkSpawn()
    {
        if (GameManager.Instance.players.Count <= 6)
        {
            GameManager.Instance.players.Add(this.gameObject);
            this.gameObject.name += GameManager.Instance.players.Count;
            if (this.IsOwner)
            {
                this.GetComponent<PlayerMain>().InitPlayer();
                this.GetComponent<PlayerMain>().ActiveCam();
            }

            this.GetComponent<SpawnPlayer>().Spawn();
            if (GameManager.Instance.players.Count == 2)
            {
                GameManager.Instance.teamManager.StartRotation();
            }

            /*    GetComponent<PlayerMain>().cam.gameObject.SetActive(true);
            }
            GetComponent<SpawnPlayer>().Spawn();*/
        }
        else
        {
            //En faire un spectateur
        }
    }
}