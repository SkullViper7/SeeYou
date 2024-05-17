using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;

    [SerializeField]private Transform shoot;
    [SerializeField] private float power;
    private GameObject fire;
    private PlayerMain main;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Shooting();
        }
    }

    public void Shooting()
    {
        
        GameObject boule = Instantiate(bullet, shoot.position, Quaternion.identity);
        boule.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * power);
        boule.SendMessage("InitBullet", gameObject);
        Destroy(boule, 2f);
    }

    public void SyncShoot()
    {
        main.playerNetwork.SyncShootServerRpc();
    }

    public void InitPlayerMain(PlayerMain _PM)
    {
        _PM.shoot = this;
        main = _PM;
    }

}
