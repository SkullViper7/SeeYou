using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Samples;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    public string Pseudo;

    public NetworkList<Vector3> spawnList = new NetworkList<Vector3>();
    private NetworkVariable<int> hunterIndex = new NetworkVariable<int>();

    private NetworkVariable<int> numberOfPlayer = new NetworkVariable<int>();

    private Vector3 spawnToRemove;
    private GameObject itemsToSpawn;
    private NetworkManager _network;

    private GameObject actualHunter;
    private PlayerMain _playerMain;

    private bool hostCanChangeHunter;
    private bool hostPseudoLimit;

    private List<GameObject> playerList = new List<GameObject>();

    [SerializeField]
    private float delayBeforeChangeRoles;

    [SerializeField]
    private int maxNumberOfPlayer;

    private void Start()
    {
        if (_network == null)
        {
            _network = NetworkManager.Singleton;
        }

        if (IsHost)
        {
            hostCanChangeHunter = true;
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
        Pseudo = PlayerPrefs.GetString("Pseudo");
        if (numberOfPlayer.Value == 0)
        {
            numberOfPlayer.Value = NetworkManager.GetComponent<NetworkLan>().NumberOfPlayer.Value;
        }

        if (IsHost)
        {
            hostPseudoLimit = false;
            for (int i = 0; i < SpawnManager.Instance.spawnList.Count; i++)
            {
                spawnList.Add(SpawnManager.Instance.spawnList[i].transform.position);
            }
        }

        itemsToSpawn = NetworkManager.Singleton.GetComponent<NetworkLan>().networkUI.ItemsToSpawn;

        //if (GameManager.Instance.players.Count <= NetworkManager.Singleton.GetComponent<NetworkLan>().NumberOfPlayer.Value)
        if (GameManager.Instance.players.Count <= numberOfPlayer.Value)
        {
            GameManager.Instance.players.Add(gameObject);
            WaitConnectePlayer(Pseudo);
            NetworkManager.GetComponent<NetworkLan>().networkUI.PlayerNeeded.text = GameManager.Instance.players.Count + " / " + numberOfPlayer.Value;
            gameObject.name += GameManager.Instance.players.Count;
            spawnToRemove = spawnList[Random.Range(0, spawnList.Count)];
            if (IsOwner)
            {
                GetComponent<PlayerMain>().InitPlayer();
                GetComponent<PlayerMain>().playerCamera.ActivePreyCam();
                //GetComponent<SpawnPlayer>().Spawn(spawnToRemove);
                GameManager.Instance.LobbyCam.SetActive(false);
                _playerMain.MeshToHide.layer = _playerMain.LayerToChangeThePreyMesh;
            }

            SpawnerNetworkServerRPC();
            if (GameManager.Instance.players.Count == numberOfPlayer.Value)
            {
                NetworkManager.GetComponent<NetworkLan>().networkUI.PlayerNeeded.gameObject.SetActive(false);
                NetworkManager.GetComponent<NetworkLan>().networkUI.ipAddressText.gameObject.SetActive(false);
                GameManager.Instance.preys.AddRange(GameManager.Instance.players);
                Wait();
            }
        }
        else
        {
            //En faire un spectateur
        }
    }

    private async void WaitConnectePlayer(string _pseudo)
    {
        await Task.CompletedTask;
        SyncPseudoServerRpc(_pseudo);
    }

    /*private async void WaitConnectePlayer()
    {
        await Task.Delay(10);
        SyncPseudoServerRpc();
    }*/

    [ServerRpc(RequireOwnership = false)]
    public void SyncPseudoServerRpc(string _pseudo)
    {
        GameManager.Instance.players[GameManager.Instance.players.Count-1].GetComponent<PlayerNetwork>().Pseudo = _pseudo;
        if (GameManager.Instance.players.Count == numberOfPlayer.Value)
        {
            for (int i = 0; i < GameManager.Instance.players.Count; i++)
            {
                string playerPseudo = GameManager.Instance.players[i].GetComponent<PlayerNetwork>().Pseudo;

                Debug.Log($"Syncing Pseudo: {playerPseudo} for player index: {i}");
                SyncPseudoClientRpc(playerPseudo, i);
            }
        }
        /*    if (!hostPseudoLimit)
        {
            hostPseudoLimit = true;
            for (int i = 0; i < GameManager.Instance.players.Count; i++)
            {
                string playerPseudo = GameManager.Instance.players[i].GetComponent<PlayerNetwork>().Pseudo;
                if (i == GameManager.Instance.players.Count - 1)
                {
                    playerPseudo = _pseudo;
                }

                Debug.Log($"Syncing Pseudo: {playerPseudo} for player index: {i}");
                SyncPseudoClientRpc(playerPseudo, i);
            }
        }*/
    }

    /*[ServerRpc(RequireOwnership = false)]
    public void SyncPseudoServerRpc()
    {
        if (!hostPseudoLimit)
        {
            hostPseudoLimit = true;
            for (int i = 0; i < GameManager.Instance.players.Count; i++)
            {
                string playerPseudo = GameManager.Instance.players[i].GetComponent<PlayerNetwork>().Pseudo;
                if (i == GameManager.Instance.players.Count - 1)
                {
                    playerPseudo = pseudoOnNetwork.Value.ToString();
                }

                Debug.Log($"Syncing Pseudo: {playerPseudo} for player index: {i}");
                SyncPseudoClientRpc(playerPseudo, i);
            }
        }
    }*/

    [ClientRpc]
    public void SyncPseudoClientRpc(string _pseudo, int _indexPlayer)
    {
        Debug.Log($"Client received Pseudo: {_pseudo} for player index: {_indexPlayer}");
        GameManager.Instance.players[_indexPlayer].GetComponent<PlayerNetwork>().Pseudo = _pseudo;
    }


    private async void WaitForSpawn()
    {
        await Task.Delay(1000);
        GetComponent<SpawnPlayer>().Spawn(spawnToRemove);
    }

    /// <summary>
    /// Va enlever un spawner
    /// </summary>
    [ServerRpc(RequireOwnership = false)]
    public void SpawnerNetworkServerRPC()
    {
        spawnList.Remove(spawnToRemove);
    }


    /// <summary>
    /// Se lance que lorsque tout les joueurs sont connectés
    /// </summary>
    [ServerRpc(RequireOwnership = false)]
    private void StartTheGameServerRpc()
    {
        if (GameManager.Instance.Items.Count == 0)
        {
            for (int i = 0; i < itemsToSpawn.GetComponent<SpawnZoneObjects>().Items.Length; i++)
            {
                SpawnItemsClientRPC(itemsToSpawn.GetComponent<SpawnZoneObjects>().SpawnItems(), i);
            }
        }

        if (GameManager.Instance.teamManager._hunter == null)
        {
            RolesChangesServerRpc();
        }
    }

    /// <summary>
    /// Permet de créer un délai avant de lancer le jeu, ce délai est neccessaire au bon fonctionnement du serveur
    /// </summary>
    public async void Wait()
    {
        await Task.Delay(1000);
        StartTheGameServerRpc();
    }

    /// <summary>
    /// Va chercher le future chasseur, puis va attendre que tout les clients soient prêt à recevoir l'information
    /// </summary>
    [ServerRpc(RequireOwnership = false)]
    public void RolesChangesServerRpc()
    {
        Debug.Log("Roles are swap");
        CancelInvoke();
        if (hostCanChangeHunter)
        {
            hostCanChangeHunter = false;
            StartCoroutine(DelayChangeHunter(GameManager.Instance.teamManager.FindAHunterServ()));
        }
    }

    /// <summary>
    /// Va set le nouveau chasseur
    /// </summary>
    /// <param name="newHunter"></param>
    [ClientRpc]
    private void ChangeHunterClientRpc(int newHunter)
    {

        GameManager.Instance.teamManager.SetHunterForAllClients(newHunter);
        foreach (GameObject _player in GameManager.Instance.players)
        {
            _player.GetComponent<PlayerUI>().TransitionUI();
        }

    }

    /// <summary>
    /// Va instantier les items
    /// </summary>
    /// <param name="_position"></param>
    /// <param name="_indexItem"></param>
    [ClientRpc]
    private void SpawnItemsClientRPC(Vector2 _position, int _indexItem)
    {
        itemsToSpawn.GetComponent<SpawnZoneObjects>().InstantiateEachItem(_position, _indexItem);
    }

    /// <summary>
    /// Va créer des délais avant de changer les roles
    /// </summary>
    /// <param name="newHunter"></param>
    /// <returns></returns>
    private IEnumerator DelayChangeHunter(int newHunter)
    {
        yield return new WaitForSeconds(1f);
        
        SearchAllPlayerClientRpc();

        yield return new WaitForSeconds(delayBeforeChangeRoles);
        hostCanChangeHunter = true;
        if (GameManager.Instance.preys[newHunter] == null)
        {
            RolesChangesServerRpc();
        }
        else
        {
            ChangeHunterClientRpc(newHunter);
            SetActualHunterPreyClientRpc();
        }

        
    }

    private void DelayBeforeChangeHunter()
    {
        Debug.Log("DelayBeforeChangeHunter");
        if (IsHost) 
        {
            RemoveBulletOfHunterClientRpc();
            RolesChangesServerRpc();
        }
        
    }

    [ClientRpc]
    private void RemoveBulletOfHunterClientRpc() 
    {
        GameManager.Instance.teamManager._hunter.GetComponent<StarterAssetsInputs>()._canShoot = false;
    }

    /// <summary>
    /// Permet d'enlever un bug lié au rollement qui n'enlevait pas le chasseur d'avant
    /// </summary>
    [ClientRpc]
    private void SetActualHunterPreyClientRpc()
    {
        if (GameManager.Instance.teamManager._hunter != actualHunter && GameManager.Instance.teamManager._hunter != null && actualHunter != null)
        {
            GameManager.Instance.teamManager.SetPreys(actualHunter);
        }
    }

    /// <summary>
    /// Permet d'enlever certains bugs lié au rollement du chasseur
    /// </summary>
    [ClientRpc]
    private void SearchAllPlayerClientRpc()
    {
        if (GameManager.Instance.teamManager._hunter != null)
        {
            actualHunter = GameManager.Instance.teamManager._hunter;
            if (GameManager.Instance.preys.Count == 0)
            {
                GameManager.Instance.preys.AddRange(GameManager.Instance.players);
                foreach (GameObject player in GameManager.Instance.players)
                {
                    player.layer = 6;
                    player.tag = "Prey";
                    if (player.GetComponent<PlayerMain>().IsHunter)
                    {
                        player.GetComponent<PlayerMain>().IsHunter = false;
                    }
                    else
                    {
                        Debug.Log("Player not hunter");
                        player.SendMessage("BecomePrey");
                    }
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
            _playerMain.playerInputs = GetComponent<StarterAssetsInputs>();
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
        GetTouchedDelay();
    }

    private async void GetTouchedDelay()
    {
        await Task.CompletedTask;
        GetTouchedClientRpc();
    }

    /// <summary>
    /// On va le remove de la liste des joueurs vivants
    /// </summary>
    [ClientRpc]
    public void GetTouchedClientRpc()
    {
        Debug.Log($"Player {Pseudo} has been touched at {Time.time}");
        SendMessage("DeadState");
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

    /// <summary>
    /// Quand un joueur est touché par un trap
    /// </summary>
    [ServerRpc(RequireOwnership = false)]
    public void TrapEventServerRPC(int _trapIndex)
    {
        WaitPlayersTraps(_trapIndex);
    }

    /// <summary>
    /// On attend la sync avec tout les joueurs
    /// </summary>
    private async void WaitPlayersTraps(int _trapIndex)
    {
        await Task.Delay(100);
        TrapEventClientRPC(_trapIndex);
    }

    /// <summary>
    /// On va lancer l'event du trap
    /// </summary>
    [ClientRpc]
    private void TrapEventClientRPC(int _trapIndex)
    {
        Debug.Log($"TrapEventClientRPC called for trap index {_trapIndex}");

        if (GameManager.Instance.Items[_trapIndex].GetComponent<Trap>() == null)
        {
            GameManager.Instance.Items[_trapIndex].transform.GetChild(0).GetComponent<Trap>().TriggerEvent();
        }
        else
        {
            GameManager.Instance.Items[_trapIndex].GetComponent<Trap>().TriggerEvent();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void MoveAnimationNetworkServerRpc(Vector2 _playerMove)
    {
        MoveAnimationNetworkClientRpc(_playerMove);
    }

    [ClientRpc]
    public void MoveAnimationNetworkClientRpc(Vector2 _playerMove)
    {
        _playerMain.playerInputs.AnimMovement(_playerMove);
    }

    [ServerRpc(RequireOwnership = false)]
    public void DisplayDeathServerRpc()
    {
        DisplayDeathClientRpc(Pseudo);
    }

    [ClientRpc]
    public void DisplayDeathClientRpc(string _pseudo)
    {
        Debug.Log(_pseudo);
    }
}