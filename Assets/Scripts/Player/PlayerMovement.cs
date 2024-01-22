using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode.Samples;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    [Header("Move the camera")]
    private float x;
    private float y;
    public float sensitivity = -1f;
    private Vector3 rotate;

    [Header("Move the player")]
    public Vector3 direction;
    float speed;

    PlayerMain _playerMain;
    protected virtual void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        speed = 5;
    }

    protected virtual void FixedUpdate()
    {
        if(_playerMain != null)
        {
            if (_playerMain.playerNetwork.IsOwner)
            {
                MoveCamera();

                //si il est une proie
                Move();
            }
        }
        else
        {
            this.GetComponent<PlayerMain>().InitPlayer();
        }
        
    }

    void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void MoveCamera()
    {
        y = Input.GetAxis("Mouse X");
        x = Input.GetAxis("Mouse Y");
        rotate = new Vector3(x, y * sensitivity, 0);
        transform.eulerAngles = transform.eulerAngles - rotate;
    }

    public void InitPlayerMain(PlayerMain _PM)
    {
        _playerMain = _PM;
        _PM.playerMovement = this;
    }

    public void BecomeHunter()
    {
        direction = Vector3.zero;
    }
}

