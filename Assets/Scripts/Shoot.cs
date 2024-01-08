using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;

    [SerializeField]private Transform shoot;
    [SerializeField] private float power;
    private GameObject Fire;

    public void Shooting()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            Fire = Instantiate(bullet, shoot.position, Quaternion.identity);

            Fire.GetComponent<Rigidbody>().AddForce(shoot.forward * power);
            Destroy(Fire, 1.5f);
        }
    }

}
