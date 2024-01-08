using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;

    [SerializeField]private Transform shoot;
    [SerializeField] private float power;
    [SerializeField] private GameObject Fire;

    public void Shooting()
    {
        Fire = Instantiate(bullet, shoot.position, Quaternion.identity);

        Fire.GetComponent<Rigidbody>().AddForce(shoot.forward * power);
        Destroy(Fire, 1.5f);
    }
}
