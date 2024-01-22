using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
    public GameObject cam;
    private bool isHunter;

    public bool IsHunter
    {
        get { return isHunter; }

        set { isHunter = value;
            if (isHunter)
            {
                BecomeHunter();
            }
            else
            {
                BecomePrey();
            }
        }
    }

    public override void OnNetworkSpawn()
    {
        if (GameManager.Instance.players.Count < 6)
        {
            GameManager.Instance.players.Add(this.gameObject);

            if (this.IsOwner)
            {
                this.cam.SetActive(true);
            }

            this.Spawn();
        }
        else
        {
            //En faire un spectateur
        }
    }

    private void Spawn()
    {
        this.transform.position = SpawnManager.Instance.GiveSpawnToAPlayer().transform.position;
    }

    void BecomeHunter()
    {

    }

    void BecomePrey()
    {

    }
}