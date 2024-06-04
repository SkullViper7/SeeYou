using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    Rigidbody[] _rigidbodies;

    PlayerMain _playerMain;

    void Start()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    public void EnableRagdoll()
    {
        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].isKinematic = false;
        }

        _playerMain.playerMovement.enabled = false;
    }

    public void InitPlayerMain(PlayerMain _PM)
   	{
       	_playerMain = _PM;
       	_PM.ragdoll = this;
   	}
}
