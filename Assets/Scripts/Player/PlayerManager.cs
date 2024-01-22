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
            GameManager.Instance.players.Add(gameObject);

            if (IsOwner)
            {
                cam.SetActive(true);
            }
            Spawn();
        }
        else
        {
            //En faire un spectateur
        }
        
    }

    void Spawn()
    {
        transform.position = SpawnManager.Instance.GiveSpawnToAPlayer().transform.position;
        Debug.Log(transform.position);
    }

    void BecomeHunter()
    {

    }

    void BecomePrey()
    {

    }
}



