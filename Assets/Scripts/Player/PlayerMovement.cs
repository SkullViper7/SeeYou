using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode.Samples;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{

    private float x;
    private float y;
    public float sensitivity = -1f;
    private Vector3 rotate;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    protected virtual void FixedUpdate()
    {
        if (IsOwner)
        {
            y = Input.GetAxis("Mouse X");
            x = Input.GetAxis("Mouse Y");
            rotate = new Vector3(x, y * sensitivity, 0);
            transform.eulerAngles = transform.eulerAngles - rotate;
        }

    }
}

