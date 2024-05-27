using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    [Header("Character Input Values")]
	public Vector2 move;
	public Vector2 look;
	public bool sprint;

    [Header("Movement Settings")]
	public bool analogMovement;

	[Header("Mouse Cursor Settings")]
	public bool cursorLocked = true;
	public bool cursorInputForLook = true;


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


#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
#endif

	public void MoveInput(Vector2 newMoveDirection)
	{
		move = newMoveDirection;
	} 

	public void LookInput(Vector2 newLookDirection)
	{
		look = newLookDirection;
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

	public void SprintInput(bool newSprintState)
	{
		sprint = newSprintState;
	}
		
	private void OnApplicationFocus(bool hasFocus)
	{
		SetCursorState(cursorLocked);
	}

	private void SetCursorState(bool newState)
	{
		Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
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