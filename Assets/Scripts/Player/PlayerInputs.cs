using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public PlayerInput playerInput;

    private event Action _eventShoot;

    private PlayerMain _playerMain;

    public bool _canShoot;

    private void Awake()
    {
        this.playerInput = this.GetComponent<PlayerInput>();
    }

    public void OnMove(InputValue _move)
    {
        if (this._playerMain.playerNetwork.ActionFromClient())
        {
            this._playerMain.playerMovement.direction = _move.Get<Vector3>();
        }
    }

    public void OnShooting()
    {
        if (this._eventShoot == null)
        {
            this._eventShoot += this._playerMain.shoot.Shooting;
            this._eventShoot += this._playerMain.playerNetwork.ChangeRoles;
        }

        if (this._canShoot)
        {
            _canShoot = false;
            this._eventShoot?.Invoke();
        }
    }

    public void InitPlayerMain(PlayerMain _PM)
    {
        this._playerMain = _PM;
        _PM.playerInputs = this;
    }

    public void SwitchToHunter()
    {
        this.FindMain();
        Debug.Log("switchs");
        _canShoot = true;
        this.playerInput.SwitchCurrentActionMap("Hunter");
    }

    public void SwitchToPrey()
    {
        this.FindMain();
        this.playerInput.SwitchCurrentActionMap("Prey");
    }

    private void FindMain()
    {
        if (this._playerMain == null)
        {
            this._playerMain = this.GetComponent<PlayerMain>();
            this._playerMain.playerNetwork = this.GetComponent<PlayerNetwork>();
        }
    }
}