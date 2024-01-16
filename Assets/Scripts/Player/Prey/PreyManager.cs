using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Samples;
using Unity.Netcode;
using UnityEngine;
public class PreyManager : PlayerManager
{
    public PreyMovement preyMovement;
    public GameObject cam;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnNetworkSpawn()
    {
        GameManager.Instance.players.Add(gameObject);

        if (IsOwner)
        {
            cam.SetActive(true);
            GameManager.Instance.players.Add(gameObject);
        }
        Spawn();
    }
    void Spawn()
    {
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
    }
}

