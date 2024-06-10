using UnityEngine;
using System;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class FreeInputs : MonoBehaviour
{
	[Header("Character Input Values")]
	public Vector2 move;
	public Vector2 look;
	public bool jump;
	public bool sprint;
	[Header("Movement Settings")]
	public bool analogMovement;

	[Header("Mouse Cursor Settings")]
	public bool cursorLocked = true;
	public bool cursorInputForLook = true;

	Animator _animator;

	private event Action _eventShoot;
	private PlayerMain _playerMain;
	public bool _canShoot;

	public PlayerInput playerInput;

	AnimationUpdater _animationUpdater;

	private void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
		_animator = GetComponent<Animator>();
		_animationUpdater = GetComponent<AnimationUpdater>();
	}

#if ENABLE_INPUT_SYSTEM
	public void OnMove(InputValue _move)
	{
		MoveInput(_move.Get<Vector2>());

		if (_move.Get<Vector2>() != Vector2.zero)
		{
			_animationUpdater.UpdatePreyAnimation(1);
		}
		else
		{
			_animationUpdater.UpdatePreyAnimation(0);
		}
	}

	public void OnLook(InputValue value)
	{
		if (cursorInputForLook)
		{
			LookInput(value.Get<Vector2>());
		}
	}

	public void OnSprint(InputValue value)
	{
		SprintInput(value.isPressed);
	}
#endif
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

	/// <summary>
	/// Event handler for the Throw action. Starts the prediction, then throws the object and updates the animation.
	/// </summary>
	/// <param name="value">The input action callback context</param>
	public void OnThrow(InputValue value)
	{
		// Check if the throw action has started
		if (value.isPressed)
		{
			// Start the prediction for the throw
			_playerMain.preyThrow.Predict();
		}

		// Check if the throw action has been performed
		if (!value.isPressed)
		{
			// Throw the object
			_playerMain.preyThrow.ThrowObject();

			// Update the animation to the throw animation
		}
	}

	public void AnimMovement(Vector2 _playerMove) 
	{
        if (_playerMove != Vector2.zero)
        {
            _animationUpdater.UpdatePreyAnimation(1);
        }
        else
        {
            _animationUpdater.UpdatePreyAnimation(0);
        }
    }

	public void MoveInput(Vector2 newMoveDirection)
	{
		move = newMoveDirection;
	} 

	public void LookInput(Vector2 newLookDirection)
	{
		look = newLookDirection;
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

    public void SwitchToHunter()
    {
		FindMain();
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
