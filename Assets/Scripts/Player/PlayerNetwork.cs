using System.Collections;
using System.Collections.Generic;
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
            //GameManager.Instance.players.Add(this.gameObject);
            //AddPlayerServerRPC(NetworkManager.Singleton.LocalClientId);
            if (IsHost)
            {
                AddPlayerServerRPC();
            }

            if (this.IsOwner)
            {
                this.GetComponent<PlayerMain>().InitPlayer();
                this.GetComponent<PlayerMain>().playerCamera.ActiveCam();
            }

            this.GetComponent<SpawnPlayer>().Spawn();

            InitPlayerServerRPC();

            if (GameManager.Instance.players.Count == 2)
            {
                this.StartCoroutine(this.SpawnClient());
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

    [ServerRpc(RequireOwnership = false)]
    public void AddPlayerServerRPC()
    {
        this.gameObject.name += NetworkManager.Singleton.ConnectedClientsList.Count;
       // AddPlayerClientRpc(gameObject, gameObject.name);
    }

    [ServerRpc(RequireOwnership = false)]
    public void InitPlayerServerRPC()
    {
        if (NetworkManager.Singleton.ConnectedClientsList.Count == 2)
        {
            List<GameObject> playerList = new List<GameObject>();
            foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
            {
                GameObject playerObj = client.PlayerObject.gameObject;
                playerList.Add(playerObj);
            }

            //InitPlayerClientRpc(playerList);
        }
    }

   /* [ClientRpc]
    private void AddPlayerClientRpc(GameObject newPlayer, string hisName)
    {
        newPlayer.name = hisName;
    }

    [ClientRpc]
    private void InitPlayerClientRpc(List<GameObject> allPlayer)
    {
        GameManager.Instance.players.AddRange(allPlayer);
    }*/

    IEnumerator SpawnClient()
    {
        yield return new WaitForSeconds(2);
        UnityEngine.Debug.Log(this.gameObject.name);
        this.SpawnServer();
    }

    void SpawnServer()
    {
        if (IsServer)
        {
            BeginRotationClientRpc(GameManager.Instance.teamManager.FindAHunter());
        }
    }

    public void ChangeRoles()
    {
        if (IsServer)
        {
            RolesChangesClientRpc(GameManager.Instance.teamManager.FindAHunter());
        }
    }

    [ClientRpc]
    private void RolesChangesClientRpc(int newHunter)
    {
        this.StartCoroutine(GameManager.Instance.WaitBeforeChangeRoles(newHunter));
    }

    [ClientRpc]
    private void BeginRotationClientRpc(int newHunter)
    {
        this.StartCoroutine(GameManager.Instance.WaitBeforeBegin(newHunter));
    }

    public void BecomeHunter()
    {
        this.FindMain();
        if (this.IsOwner)
        {
            this._playerMain.playerInputs.SwitchToHunter();
        }
    }

    public void BecomePrey()
    {
        this.FindMain();
        if (this.IsOwner)
        {
            this._playerMain.playerInputs.SwitchToPrey();
        }
    }

    private void FindMain()
    {
        if (this._playerMain == null)
        {
            this._playerMain = this.GetComponent<PlayerMain>();
            this._playerMain.playerInputs = this.GetComponent<PlayerInputs>();
        }
    }
}