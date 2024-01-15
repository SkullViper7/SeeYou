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

    private void Update()
    {
        
    }

    public void Shooting()
    {        
        GameObject boule = Instantiate(bullet, shoot.position, Quaternion.identity) as GameObject;
        boule.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * power);
        Destroy(boule, 2f);
    }

}
