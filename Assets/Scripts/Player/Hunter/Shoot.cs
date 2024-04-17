using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
public class Shoot : NetworkBehaviour
{
    public GameObject bullet;

    [SerializeField]private Transform _shoot;
    [SerializeField] private float power;
    private GameObject Fire;

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Shooting();
        }
    }

    [ServerRpc]
    public void Shooting()
    {
        GameObject boule = Instantiate(bullet, _shoot.position, Quaternion.identity) as GameObject;
        boule.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * power);
        Destroy(boule, 2f);
        _shoot.GetComponent<NetworkObject>().Spawn(); 
    }

    public void InitPlayerMain(PlayerMain _PM)
    {
        _PM.shoot = this;
    }


    
}
