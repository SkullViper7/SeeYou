using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PreyInput : PlayerInputs
{
    private void Start()
    {
        PlayerManager.Instance.playerInput = this;
    }
    public void OnMove(InputValue _move)
    {
        PlayerManager.Instance.playerMovement.direction = _move.Get<Vector3>();
        Debug.Log(_move.Get<Vector3>());
    }

}
