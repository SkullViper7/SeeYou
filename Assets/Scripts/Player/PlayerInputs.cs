using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public PlayerInput playerInput;

    private event Action _eventShoot;

    private PlayerMain _playerMain;

    public bool _canShoot;

    Animator _animator;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        _animator = GetComponent<Animator>();
    }

    public void OnMove(InputValue _move)
    {
        if (_playerMain.playerNetwork.ActionFromClient())
        {
            _playerMain.playerMovement.direction = _move.Get<Vector3>();
        }

        if (_move.Get<Vector3>() != Vector3.zero)
        {
            _animator.Play("Run");
        }
        else
        {
            _animator.Play("Idle");
        }
    }

    public void OnShooting()
    {
        if (_eventShoot == null)
        {
            _eventShoot += _playerMain.shoot.SyncShoot;
            _eventShoot += _playerMain.playerNetwork.RolesChangesServerRpc;
        }

        if (_canShoot)
        {
            _canShoot = false;
            _eventShoot?.Invoke();
        }
    }

    public void InitPlayerMain(PlayerMain _PM)
    {
        _playerMain = _PM;
        _PM.playerInputs = this;
    }

    public void SwitchToHunter()
    {
        FindMain();
        Debug.Log("switchs");
        _canShoot = true;
        playerInput.SwitchCurrentActionMap("Hunter");
    }

    public void SwitchToPrey()
    {
        FindMain();
        playerInput.SwitchCurrentActionMap("Prey");
    }

    private void FindMain()
    {
        if (_playerMain == null)
        {
            _playerMain = GetComponent<PlayerMain>();
            _playerMain.playerNetwork = GetComponent<PlayerNetwork>();
        }
    }
}