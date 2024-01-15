using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode.Samples;
using Unity.Netcode;
public class PreyInput : PlayerInputs
{
    PreyManager manager;
    NetworkManager network;
    private void Start()
    {
        network = NetworkManager.Singleton;
        manager = GetComponent<PreyManager>();
        manager.playerInput = this;
    }
    public void OnMove(InputValue _move)
    {
        if (network.LocalClient != null)
        {
            if (network.LocalClient.PlayerObject.TryGetComponent(out PreyManager preyManager))
            {
                // Invoke a `ServerRpc` from client-side to teleport player to a random position on the server-side
                preyManager.preyMovement.direction = _move.Get<Vector3>(); 
            }
        }
        //manager.preyMovement.direction = _move.Get<Vector3>();
        
    }

}

