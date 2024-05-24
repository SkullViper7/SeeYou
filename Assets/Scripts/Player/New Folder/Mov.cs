using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Mov : MonoBehaviour
{
    public float speed;

    public Transform Orientation;

    [SerializeField]
    private float maxVelocity;
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        MyInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void MovePlayer()
    {
        moveDirection = Orientation.forward * verticalInput + Orientation.right * horizontalInput;
        //transform.Translate(this.moveDirection.normalized * this.speed * Time.deltaTime);
        rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
        if (math.abs(rb.velocity.z) > maxVelocity)
        {
            if (rb.velocity.z > 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, maxVelocity);
            }
            else
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -maxVelocity);
            }
        }

        if (math.abs(rb.velocity.x) > maxVelocity)
        {
            if (rb.velocity.x > 0)
            {
                rb.velocity = new Vector3(maxVelocity, rb.velocity.y, rb.velocity.z);
            }
            else
            {
                rb.velocity = new Vector3(-maxVelocity, rb.velocity.y, rb.velocity.z);
            }
        }

        //transform.Translate(moveDirection.normalized * speed * Time.deltaTime);
    }
}
