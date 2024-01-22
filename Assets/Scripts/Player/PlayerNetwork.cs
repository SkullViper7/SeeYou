using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Globalization;

public class PlayerNetwork : NetworkBehaviour
{
    private NetworkManager _network;

    private PlayerMain _playerMain;
    // Start is called before the first frame update
    void Start()
    {
        _network = NetworkManager.Singleton;
        _playerMain = GetComponent<PlayerMain>();
    }

    public void InitPlayerMain(PlayerMain _PM)
    {
        _playerMain = _PM;
        _PM.playerNetwork = this;
    }

    public bool ActionFromClient()
    {
        bool canDoTheAction = false;
        if (_network.LocalClient != null)
        {
            if (_network.LocalClient.PlayerObject.TryGetComponent(out PlayerMain _playerMain))
            {
                canDoTheAction = true;
            }
        }
        return canDoTheAction;
    }

    public override void OnNetworkSpawn()
    {
        if (GameManager.Instance.players.Count < 6)
        {
            Debug.Log("IsOwner");
            Debug.Log(this.IsOwner);
            GameManager.Instance.players.Add(gameObject);
            if(GameManager.Instance.players.Count == 6)
            {

            }

            if (this.IsOwner)
            {
                this.GetComponent<PlayerMain>().InitPlayer();
                this.GetComponent<PlayerMain>().ActiveCam();
            }
            this.GetComponent<SpawnPlayer>().Spawn();
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
