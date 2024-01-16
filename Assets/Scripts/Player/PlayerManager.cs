using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
    public class PlayerManager : NetworkBehaviour
    {
        public PlayerInputs playerInput;
    public override void OnNetworkSpawn()
    {
        GameManager.Instance.players.Add(gameObject);

        if (IsOwner)
        {
            if(GameManager.Instance.players.Count > 6)
            {

            }
        }
    }

    /*
    private static PlayerManager instance = null;
    public static PlayerManager Instance => instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }
    */
}

