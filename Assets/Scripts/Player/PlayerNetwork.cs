using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    private NetworkManager _network;

    private PlayerMain _playerMain;

    private List<GameObject> playerList = new List<GameObject>();

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
            GameManager.Instance.players.Add(gameObject);
            this.gameObject.name += GameManager.Instance.players.Count;
            if (this.IsOwner)
            {
                this.GetComponent<PlayerMain>().InitPlayer();
                this.GetComponent<PlayerMain>().playerCamera.ActiveCam();
            }

            /*if (this.IsHost)
            {
                AddPlayerClientRpc(this.gameObject.name);
                InitPlayerServerRPC();
            }*/

            this.GetComponent<SpawnPlayer>().Spawn();

            if (GameManager.Instance.players.Count == 2)
            {
                GameManager.Instance.preys.AddRange(GameManager.Instance.players);
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

    }

    [ServerRpc(RequireOwnership = false)]
    public void InitPlayerServerRPC()
    {
        if (NetworkManager.Singleton.ConnectedClientsList.Count == 2)
        {
            foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
            {
                GameObject playerObj = client.PlayerObject.gameObject;
                playerList.Add(playerObj);
            }

            InitPlayerClientRpc();
        }
    }

    [ClientRpc]
    private void AddPlayerClientRpc(string hisName)
    {
        this.gameObject.name = hisName;
    }

    [ClientRpc]
    private void InitPlayerClientRpc()
    {
        GameManager.Instance.players.AddRange(playerList);
    }

    IEnumerator SpawnClient()
    {
        yield return new WaitForSeconds(2);
        SpawnServer();
    }

    void SpawnServer()
    {
        if (IsHost)
        {
            BeginRotationClientRpc(GameManager.Instance.teamManager.FindAHunter());
        }
    }

    public void ChangeRoles()
    {
        //Debug.Log("changeRole");
        //RolesChangesServerRpc(GameManager.Instance.teamManager.FindAHunter());
        if (IsOwner)
        {
            RolesChangesClientRpc(GameManager.Instance.teamManager.FindAHunter());
        }
    }

    [ServerRpc]
    public void RolesChangesServerRpc()
    {
        //GameManager.Instance.network.SetHunterForManagersServerRpc();
        //ChangeHunterClientRpc(GameManager.Instance.teamManager.FindAHunterServ());

        StartCoroutine(DelayChangeHunter(GameManager.Instance.teamManager.FindAHunterServ()));
        //RolesChangesClientRpc(GameManager.Instance.teamManager.FindAHunter());
    }

    [ClientRpc]
    private void RolesChangesClientRpc(int newHunter)
    {
        StartCoroutine(GameManager.Instance.WaitBeforeChangeRoles(newHunter));
    }

    [ClientRpc]
    private void ChangeHunterClientRpc(int newHunter)
    {
        GameManager.Instance.teamManager.SetHunterForAllClients(newHunter);
    }

    IEnumerator DelayChangeHunter(int newHunter)
    {
        yield return new WaitForSeconds(2f);
        SearchAllPlayerClientRpc();
        yield return new WaitForSeconds(2f);
        ChangeHunterClientRpc(newHunter);
    }

    [ClientRpc]
    private void SearchAllPlayerClientRpc()
    {
        if (GameManager.Instance.preys.Count == 0)
        {
            GameManager.Instance.preys.AddRange(GameManager.Instance.players);
            foreach (GameObject player in GameManager.Instance.players)
            {
                player.layer = 6;
                if (player.GetComponent<PlayerMain>().IsHunter)
                {
                    player.GetComponent<PlayerMain>().IsHunter = false;
                }

                //player.transform.SetParent(this._preyParent);
            }
        }
    }

    [ClientRpc]
    private void BeginRotationClientRpc(int newHunter)
    {
        StartCoroutine(GameManager.Instance.WaitBeforeBegin(newHunter));
    }

    public void BecomeHunter()
    {
        this.FindMain();
        if (IsOwner)
        {
            _playerMain.playerInputs.SwitchToHunter();
        }
    }

    public void BecomePrey()
    {
        this.FindMain();
        if (IsOwner)
        {
            _playerMain.playerInputs.SwitchToPrey();
        }
    }

    private void FindMain()
    {
        if (_playerMain == null)
        {
            _playerMain = GetComponent<PlayerMain>();
            _playerMain.playerInputs = GetComponent<PlayerInputs>();
        }
    }

    [ServerRpc]
    public void SyncShootServerRpc()
    {
        StartCoroutine(WaitPlayers());
    }
    [ClientRpc]
    public void SyncShootClientRpc()
    {
        if (!IsOwner)
        {
            GameManager.Instance.teamManager._hunter.GetComponent<Shoot>().Shooting();
        }
    }

    private IEnumerator WaitPlayers()
    {
        yield return new WaitForSeconds(0.1f);
        SyncShootClientRpc();
    }


    [ServerRpc(RequireOwnership = false)]
    public void GetTouchedServerRpc()
    {
       GetTouchedClientRpc();
    }
    [ClientRpc]
    public void GetTouchedClientRpc()
    {
        for (int i = 0; i < GameManager.Instance.preys.Count; i++)
            {
                if (GameManager.Instance.preys[i] != null)
                {
                    if (GameManager.Instance.preys[i] == gameObject)
                    {
                        GameManager.Instance.preys.Remove(GameManager.Instance.preys[i]);
                    }
                }
            }

            for (int i = 0; i < GameManager.Instance.players.Count; i++)
            {
                if (GameManager.Instance.players[i] != null)
                {
                    if (GameManager.Instance.players[i] == gameObject)
                    {
                        GameManager.Instance.players.Remove(GameManager.Instance.players[i]);
                    }
                }
            }
            
            gameObject.SetActive(false);
        if (GameManager.Instance.players.Count == 1)
        {
            GameManager.Instance.teamManager.Victory(GameManager.Instance.players[0].name);
        }
    }
}