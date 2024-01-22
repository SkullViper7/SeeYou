using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public PlayerInput playerInput;

    private event Action _eventShoot;

    private PlayerMain _playerMain;

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
        if (this._playerMain.playerNetwork.ActionFromClient())
        {
            if (this._eventShoot == null)
            {
                this._eventShoot += this._playerMain.shoot.Shooting;
                this._eventShoot += GameManager.Instance.ChangeRoles;
            }

            this._eventShoot?.Invoke();
        }
    }

    public void InitPlayerMain(PlayerMain _PM)
    {
        this._playerMain = _PM;
        _PM.playerInputs = this;
    }

    public void BecomeHunter()
    {
        this.FindMain();
        if (this._playerMain.playerNetwork.IsOwnerOfTheGameObject())
        {
            this.playerInput.SwitchCurrentActionMap("Hunter");
        }
    }

    public void BecomePrey()
    {
        this.FindMain();
        if (this._playerMain.playerNetwork.IsOwnerOfTheGameObject())
        {
            this.playerInput.SwitchCurrentActionMap("Prey");
        }
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