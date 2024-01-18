using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public PlayerInput playerInput;

    private event Action _eventShoot;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    PreyManager manager;

    PlayerMain _playerMain;


    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    public void OnMove(InputValue _move)
    {
        if (_playerMain.playerNetwork.ActionFromClient())
        {
            _playerMain.playerMovement.direction = _move.Get<Vector3>();
        }
    }

    public void OnShooting()
    {
        if (_eventShoot == null)
        {
            _eventShoot += _playerMain.shoot.Shooting;
            _eventShoot += GameManager.Instance.ChangeRoles;
        }
        _eventShoot?.Invoke();
    }

    public void InitPlayerMain(PlayerMain _PM)
    {
        _playerMain = _PM;
        _PM.playerInputs = this;
    }
    public void BecomeHunter()
    {
        playerInput.SwitchCurrentActionMap("Hunter");
    }

    public void BecomePrey()
    {
        playerInput.SwitchCurrentActionMap("Prey");
    }

    
}

