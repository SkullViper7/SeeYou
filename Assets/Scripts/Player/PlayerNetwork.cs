using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Samples;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    private GameObject itemsToSpawn;
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
        itemsToSpawn = NetworkManager.Singleton.GetComponent<NetworkLan>().ItemsToSpawn;
        //if (GameManager.Instance.players.Count <= NetworkManager.Singleton.GetComponent<NetworkLan>().NumberOfPlayer.Value)
        if (GameManager.Instance.players.Count <= 2)
        {
            GameManager.Instance.players.Add(gameObject);
            gameObject.name += GameManager.Instance.players.Count;
            if (IsOwner)
            {
                GetComponent<PlayerMain>().InitPlayer();
                GetComponent<PlayerMain>().playerCamera.ActiveCam();
            }

            GetComponent<SpawnPlayer>().Spawn();
            //if (GameManager.Instance.players.Count == NetworkManager.Singleton.GetComponent<NetworkLan>().NumberOfPlayer.Value)
            if (GameManager.Instance.players.Count == 2)
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

    /// <summary>
    /// Se lance que lorsque tout les joueurs sont connectés
    /// </summary>
    [ServerRpc]
    private void StartTheGame()
    {
        for (int i = 0; i < itemsToSpawn.GetComponent<SpawnZoneObjects>().Items.Length; i++)
        {
            SpawnItemsClientRPC(itemsToSpawn.GetComponent<SpawnZoneObjects>().SpawnItems(), i);
        }

        RolesChangesServerRpc();
    }

    /// <summary>
    /// Va appeler 
    /// </summary>
    [ServerRpc]
    public void RolesChangesServerRpc()
    {
        StartCoroutine(DelayChangeHunter(GameManager.Instance.teamManager.FindAHunterServ()));
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="newHunter"></param>
    [ClientRpc]
    private void ChangeHunterClientRpc(int newHunter)
    {
        GameManager.Instance.teamManager.SetHunterForAllClients(newHunter);
    }

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
        GameObject actualHunter = GameManager.Instance.teamManager._hunter;
        yield return new WaitForSeconds(2f);
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

    /*private async Task<GameObject> SpawnItems()
    {
        itemsToSpawn.GetComponent<SpawnZoneObjects>().SpawnItems();
        await Task.CompletedTask;
    }*/

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
        GameManager.Instance.teamManager._hunter.GetComponent<RaycastShoot>().Shooting();
    }

    private IEnumerator WaitPlayers()
    {
        yield return new WaitForSeconds(0.1f);
        //tester avec yield return null
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

    /// <summary>
    /// Quand un joueur est touché par un trap
    /// </summary>
    [ServerRpc(RequireOwnership = false)]
    public void TrapEventServerRPC()
    {
        WaitPlayersTraps();
    }

    /// <summary>
    /// On attend la sync avec tout les joueurs
    /// </summary>
    private async void WaitPlayersTraps()
    {
        await Task.CompletedTask;
        TrapEventClientRPC();
    }

    /// <summary>
    /// On va lancer l'event du trap
    /// </summary>
    [ClientRpc]
    private void TrapEventClientRPC()
    {
        _playerMain.playerCollider.LastTrap.TriggerEvent();
    }
}