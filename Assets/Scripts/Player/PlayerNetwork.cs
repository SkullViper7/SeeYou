using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Samples;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    private NetworkManager _network;

    private PlayerMain _playerMain;

    private List<GameObject> playerList = new List<GameObject>();

    [SerializeField] 
    private float delayBeforeChangeRoles;

    [SerializeField] 
    private float maxNumberOfPlayer;

    private void Start()
    {
        if (_network == null)
        {
            _network = NetworkManager.Singleton;
        }
    }

    public void InitPlayerMain(PlayerMain _PM)
    {
        _playerMain = _PM;
        _PM.playerNetwork = this;
    }

    public bool ActionFromClient()
    {
        bool canDoTheAction = false;
        if (_network == null)
        {
            _network = NetworkManager.Singleton;
        }

        if (_network.LocalClient != null)
        {
            if (_network.LocalClient.PlayerObject.TryGetComponent(out PlayerMain playerMain))
            {
                canDoTheAction = true;
            }
        }

        return canDoTheAction;
    }

    public bool IsOwnerOfTheGameObject()
    {
        return IsOwner;
    }


    /// <summary>
    /// Lorsqu'un joueur se connecte au serveur on lui créer un nouveau gameObject
    /// De plus si il y a suffisament de joueur on lancer le jeu
    /// Si un nouveau joueur se connecte au serveur alors que le jeu est lancer il sera seulement spectateur
    /// </summary>
    public override void OnNetworkSpawn()
    {
        if (GameManager.Instance.players.Count <= NetworkManager.Singleton.GetComponent<NetworkLan>().NumberOfPlayer.Value)
        {
            GameManager.Instance.players.Add(gameObject);
            gameObject.name += GameManager.Instance.players.Count;
            if (IsOwner)
            {
                GetComponent<PlayerMain>().InitPlayer();
                GetComponent<PlayerMain>().playerCamera.ActiveCam();
            }

            GetComponent<SpawnPlayer>().Spawn();
            if (GameManager.Instance.players.Count == NetworkManager.Singleton.GetComponent<NetworkLan>().NumberOfPlayer.Value)
            {
                GameManager.Instance.preys.AddRange(GameManager.Instance.players);
                RolesChangesServerRpc();
            }
        }
        else
        {
            //En faire un spectateur
        }
    }

    [ServerRpc]
    public void RolesChangesServerRpc()
    {
        StartCoroutine(DelayChangeHunter(GameManager.Instance.teamManager.FindAHunterServ()));
    }

    [ClientRpc]
    private void ChangeHunterClientRpc(int newHunter)
    {
        GameManager.Instance.teamManager.SetHunterForAllClients(newHunter);
    }

    private IEnumerator DelayChangeHunter(int newHunter)
    {
        GameObject actualHunter = GameManager.Instance.teamManager._hunter;
        //yield return new WaitForSeconds(_playerMain.shoot.DelayBulletBeforeGetDestroy);
        SearchAllPlayerClientRpc();
        yield return new WaitForSeconds(delayBeforeChangeRoles);
        ChangeHunterClientRpc(newHunter);
        if (GameManager.Instance.teamManager._hunter != actualHunter)
        {
            GameManager.Instance.teamManager.SetPreys(actualHunter);
        }
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
                else
                {
                    player.SendMessage("BecomePrey");
                }
            }
        }
    }

    /// <summary>
    /// Lorsqu'un joueur reçoit le message BecomeHunter
    /// </summary>
    public void BecomeHunter()
    {
        FindMain();
        if (IsOwner)
        {
            _playerMain.playerInputs.SwitchToHunter();
        }
    }

    /// <summary>
    /// Lorsqu'un joueur reçoit le message BecomePrey
    /// </summary>
    public void BecomePrey()
    {
        FindMain();
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

    /// <summary>
    /// Quand un joueur tire
    /// </summary>
    [ServerRpc]
    public void SyncShootServerRpc()
    {
        StartCoroutine(WaitPlayers());
    }

    /// <summary>
    /// On va montrer à tout les joueurs qu'un joueur tire
    /// </summary>
    [ClientRpc]
    public void SyncShootClientRpc()
    {
        GameManager.Instance.teamManager._hunter.GetComponent<Shoot>().Shooting();
    }

    private IEnumerator WaitPlayers()
    {
        yield return new WaitForSeconds(0.1f);
        SyncShootClientRpc();
    }

    /// <summary>
    /// Quand un joueur est touché
    /// </summary>
    [ServerRpc(RequireOwnership = false)]
    public void GetTouchedServerRpc()
    {
       GetTouchedClientRpc();
    }

    /// <summary>
    /// On va le remove de la liste des joueurs vivants
    /// </summary>
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

    [ServerRpc(RequireOwnership = false)]
    public void SoundEmitServerRpc()
    {
        WaitPlayersSounds();
    }

   /*private IEnumerator WaitPlayersSounds()
    {
        yield return new WaitForSeconds(0.01f);
        SoundEmitClientRpc();
    }*/

    private async void WaitPlayersSounds()
    {
        await Task.CompletedTask;
        SoundEmitClientRpc();
    }

    [ClientRpc]
    private void SoundEmitClientRpc()
    {
        SendMessage("Step");
    }
}