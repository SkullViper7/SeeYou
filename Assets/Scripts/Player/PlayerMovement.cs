using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 direction;
    float speed;
    private void Start()
    {
        PlayerManager.Instance.playerMovement = this;
        speed = 5;
    }

    private void FixedUpdate()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
